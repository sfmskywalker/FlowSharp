using System.Text.Json;
using System.Text.Json.Serialization;
using Elsa.Expressions.Contracts;
using Elsa.Expressions.Helpers;
using Elsa.Expressions.Models;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;
using Elsa.Workflows.Core.Helpers;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Services;
using Elsa.Workflows.Management.Contracts;
using Humanizer;

namespace Elsa.Workflows.Management.Serialization.Converters;

/// <summary>
/// (De)serializes objects of type <see cref="IActivity"/>.
/// </summary>
public class ActivityJsonConverter : JsonConverter<IActivity>
{
    private readonly IActivityRegistry _activityRegistry;
    private readonly IActivityFactory _activityFactory;
    private readonly IActivityDescriber _activityDescriber;
    private readonly IExpressionSyntaxRegistry _expressionSyntaxRegistry;
    private readonly IServiceProvider _serviceProvider;

    /// <inheritdoc />
    public ActivityJsonConverter(
        IActivityRegistry activityRegistry, 
        IActivityFactory activityFactory, 
        IActivityDescriber activityDescriber,
        IExpressionSyntaxRegistry expressionSyntaxRegistry,
        IServiceProvider serviceProvider)
    {
        _activityRegistry = activityRegistry;
        _activityFactory = activityFactory;
        _activityDescriber = activityDescriber;
        _expressionSyntaxRegistry = expressionSyntaxRegistry;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public override IActivity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
            throw new JsonException("Failed to parse JsonDocument");

        if (!doc.RootElement.TryGetProperty("type", out var activityTypeNameElement))
            throw new JsonException("Failed to extract activity type property");

        var activityTypeName = activityTypeNameElement.GetString()!;
        var activityTypeVersion = doc.RootElement.TryGetProperty("version", out var activityTypeVersionElement) ? activityTypeVersionElement.GetInt32() : 1;
        var activityDescriptor = _activityRegistry.Find(activityTypeName, activityTypeVersion);

        var newOptions = new JsonSerializerOptions(options);
        newOptions.Converters.Add(new InputJsonConverterFactory(_serviceProvider));
        newOptions.Converters.Add(new OutputJsonConverterFactory(_serviceProvider));

        if (activityDescriptor == null)
        {
            var notFoundContext = new ActivityConstructorContext(doc.RootElement, newOptions);
            var notFoundActivity = (NotFoundActivity)_activityFactory.Create(typeof(NotFoundActivity), notFoundContext);

            notFoundActivity.Type = ActivityTypeNameHelper.GenerateTypeName<NotFoundActivity>();
            notFoundActivity.Version = 1;
            notFoundActivity.MissingTypeName = activityTypeName;
            notFoundActivity.MissingTypeVersion = activityTypeVersion;
            return notFoundActivity;
        }

        var context = new ActivityConstructorContext(doc.RootElement, newOptions);
        var activity = activityDescriptor.Constructor(context);

        // Reconstruct synthetic inputs.
        
        foreach (var inputDescriptor in activityDescriptor.Inputs.Where(x => x.IsSynthetic))
        {
            var inputName = inputDescriptor.Name;
            var propertyName = inputName.Camelize();
            var nakedType = inputDescriptor.Type;
            var wrappedType = typeof(Input<>).MakeGenericType(nakedType);
                    
            if (doc.RootElement.TryGetProperty(propertyName, out var propertyElement) && propertyElement.ValueKind != JsonValueKind.Null && propertyElement.ValueKind != JsonValueKind.Undefined)
            {
                var isWrapped = propertyElement.ValueKind == JsonValueKind.Object && propertyElement.GetProperty("typeName").ValueKind != JsonValueKind.Undefined;
                
                if (isWrapped)
                {
                    var json = propertyElement.ToString();
                    var inputValue = JsonSerializer.Deserialize(json, wrappedType, newOptions);

                    activity.SyntheticProperties[inputName] = inputValue!;    
                }
                else
                {
                    activity.SyntheticProperties[inputName] = propertyElement.ConvertTo(inputDescriptor.Type)!;
                }
            }
        }
        
        // Reconstruct synthetic outputs.
        foreach (var outputDescriptor in activityDescriptor.Outputs.Where(x => x.IsSynthetic))
        {
            var outputName = outputDescriptor.Name;
            var propertyName = outputName.Camelize();
            var nakedType = outputDescriptor.Type;
            var wrappedType = typeof(Output<>).MakeGenericType(nakedType);

            if (!doc.RootElement.TryGetProperty(propertyName, out var propertyElement) || propertyElement.ValueKind == JsonValueKind.Null || propertyElement.ValueKind == JsonValueKind.Undefined) 
                continue;
            
            var memoryReferenceElement = propertyElement.GetProperty("memoryReference");

            if (!memoryReferenceElement.TryGetProperty("id", out var memoryReferenceIdElement)) 
                continue;
            
            var variable = new Variable(memoryReferenceIdElement.GetString()!);
            var output = Activator.CreateInstance(wrappedType, variable)!;

            activity.SyntheticProperties[outputName] = output!;
        }

        return activity;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, IActivity value, JsonSerializerOptions options)
    {
        var activityDescriptor = _activityRegistry.Find(value.Type, value.Version)!;
        var newOptions = new JsonSerializerOptions(options);
        
        newOptions.Converters.Add(new InputJsonConverterFactory(_serviceProvider));
        newOptions.Converters.Add(new OutputJsonConverterFactory(_serviceProvider));
        
        // Write to a JsonObject so that we can add additional information.
        var activityModel = JsonSerializer.SerializeToNode(value, value.GetType(), newOptions)!;
        var syntheticInputs = activityDescriptor.Inputs.Where(x => x.IsSynthetic).ToList();
        var syntheticOutputs = activityDescriptor.Outputs.Where(x => x.IsSynthetic).ToList();

        // Write synthetic inputs. 
        foreach (var inputDescriptor in syntheticInputs)
        {
            var inputName = inputDescriptor.Name;
            var propertyName = inputName.Camelize();
            
            if (value.SyntheticProperties.TryGetValue(inputName, out var inputValue))
            {
                var input = (Input?)inputValue;

                if (input == null)
                {
                    activityModel[propertyName] = null;
                    continue;
                }
                
                var expression = input.Expression;
                var expressionType = expression.GetType();
                var inputType = input.Type;
                var memoryReferenceId = input.MemoryBlockReference().Id;
                var expressionSyntaxDescriptor = _expressionSyntaxRegistry.Find(x => x.Type == expressionType);

                if (expressionSyntaxDescriptor == null)
                    throw new Exception($"Syntax descriptor with expression type {expressionType} not found in registry");

                var inputModel = new
                {
                    TypeName = inputType,
                    Expression = expressionSyntaxDescriptor.CreateSerializableObject(new SerializableObjectConstructorContext(expression)),
                    MemoryReference = new
                    {
                        Id = memoryReferenceId
                    }
                };

                activityModel[propertyName] = JsonSerializer.SerializeToNode(inputModel, inputModel.GetType(), newOptions);
            }
        }
        
        // Write synthetic outputs. 
        foreach (var outputDescriptor in syntheticOutputs)
        {
            var outputName = outputDescriptor.Name;
            var propertyName = outputName.Camelize();
            
            if (value.SyntheticProperties.TryGetValue(outputName, out var outputValue))
            {
                var output = (Output?)outputValue;

                if (output == null)
                {
                    activityModel[propertyName] = null;
                    continue;
                }
                
                var outputType = outputDescriptor.Type;
                var memoryReferenceId = output.MemoryBlockReference().Id;
                
                var outputModel = new
                {
                    TypeName = outputType,
                    MemoryReference = new
                    {
                        Id = memoryReferenceId
                    }
                };

                activityModel[propertyName] = JsonSerializer.SerializeToNode(outputModel, outputModel.GetType(), newOptions);
            }
        }
        
        // Send the model to the writer.
        JsonSerializer.Serialize(writer, activityModel, newOptions);
    }
}