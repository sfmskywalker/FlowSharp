using Elsa.Expressions.Models;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Services;

namespace Elsa.Workflows.Core.Implementations;

/// <inheritdoc />
public class VariablePersistenceManager : IVariablePersistenceManager
{
    /// <inheritdoc />
    public IEnumerable<Variable> GetPersistentVariables(WorkflowExecutionContext context) => context.Workflow.Variables.Where(x => x.StorageDriverType != null).ToList();

    /// <inheritdoc />
    public IEnumerable<Variable> GetPersistentVariablesInScope(ActivityExecutionContext context)
    {
        // Get variables for the current activity's immediate composite container.
        var immediateCompositeVariables = ((Composite?)context.ActivityNode.Ancestors()
                .FirstOrDefault(x => x.Activity is Composite)?.Activity)?.Variables
            .Where(x => x.StorageDriverType != null) ?? Enumerable.Empty<Variable>();

        // Get variables for the current activity itself, if it's a container.
        var directVariables = (context.Activity is Composite composite
            ? composite.Variables.Where(x => x.StorageDriverType != null)
            : Enumerable.Empty<Variable>());

        // Return a concatenated list of variables.
        return immediateCompositeVariables.Concat(directVariables);
    }

    /// <inheritdoc />
    public async Task LoadVariablesAsync(WorkflowExecutionContext context, IEnumerable<Variable> variables)
    {
        var register = context.MemoryRegister;
        var variableList = variables as ICollection<Variable> ?? variables.ToList();

        EnsureVariablesAreDeclared(context, variableList);

        // Foreach variable memory block, load its value from their associated storage driver.
        var cancellationToken = context.CancellationToken;
        var storageDriverContext = new StorageDriverContext(context, cancellationToken);
        //var blocks = register.Blocks.Values.Where(x => x.Metadata is VariableBlockMetadata { IsInitialized: false, StorageDriverType: not null }).ToList();
        var storageDriverManager = context.GetRequiredService<IStorageDriverManager>();

        foreach (var variable in variableList)
        {
            var block = EnsureBlock(register, variable);
            var metadata = (VariableBlockMetadata)block.Metadata!;
            var driver = storageDriverManager.Get(metadata.StorageDriverType!);

            block.Metadata = metadata with { IsInitialized = true };

            if (driver == null)
                continue;
            
            var id = GetStateId(context, variable);
            var value = await driver.ReadAsync(id, storageDriverContext);
            if (value == null) continue;

            var parsedValue = variable.ParseValue(value);
            register.Declare(variable);
            variable.Set(register, parsedValue);
        }
    }

    /// <inheritdoc />
    public async Task SaveVariablesAsync(WorkflowExecutionContext context)
    {
        var register = context.MemoryRegister;
        
        // Foreach variable memory block, save its value using their associated storage driver.
        var cancellationToken = context.CancellationToken;
        var storageDriverContext = new StorageDriverContext(context, cancellationToken);
        var blocks = register.Blocks.Values.Where(x => x.Metadata is VariableBlockMetadata { StorageDriverType: not null }).ToList();
        var storageDriverManager = context.GetRequiredService<IStorageDriverManager>();

        foreach (var block in blocks)
        {
            var metadata = (VariableBlockMetadata)block.Metadata!;
            var driver = storageDriverManager.Get(metadata.StorageDriverType!);

            if (driver == null)
                continue;

            var variable = metadata.Variable;
            var id = GetStateId(context, variable);
            var value = block.Value;
            
            if (value == null)
                await driver.DeleteAsync(id, storageDriverContext);
            else
                await driver.WriteAsync(id, value, storageDriverContext);
        }
    }

    /// <inheritdoc />
    public void EnsureVariablesAreDeclared(WorkflowExecutionContext context, IEnumerable<Variable> variables)
    {
        var register = context.MemoryRegister;
        foreach (var variable in variables)
        {
            if (!register.IsDeclared(variable))
                register.Declare(variable);
        }
    }

    private MemoryBlock EnsureBlock(MemoryRegister register, Variable variable)
    {
        if (!register.TryGetBlock(variable.Id, out var block))
            block = register.Declare(variable);
        return block;
    }

    private string GetStateId(WorkflowExecutionContext context, Variable variable) => $"{context.Id}:{context.Workflow.Id}:{variable.Name}";
}