namespace Elsa.Workflows.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ActivityAttribute : Attribute
{
    public ActivityAttribute(string @namespace, string? category, string? description = default)
    {
        Namespace = @namespace;
        Description = description;
        Category = category;
    }

    public ActivityAttribute(string @namespace, string? description = default)
    {
        Namespace = @namespace;
        Description = description;
        Category = @namespace;
    }

    public ActivityAttribute(string @namespace, string? type, int version = 1, string? description = default, string? category = default)
    {
        Namespace = @namespace;
        Type = type;
        Version = version;  
        Description = description;
        Category = category;
    }

    public string? Namespace { get; set; }
    public string? Type { get; set; }
    public int Version { get; set; } = 1;
    public string? Description { get; set; }
    public string? DisplayName { get; set; }
    public string? Category { get; set; }   
    
    /// <summary>
    /// A type that implements <see cref="ICustomPropertiesProvider"/> and provides a default value for custom properties.
    /// </summary>
    public Type CustomPropertiesTypeProvider { get; set; }
    public ActivityKind Kind { get; set; } = ActivityKind.Action;
}