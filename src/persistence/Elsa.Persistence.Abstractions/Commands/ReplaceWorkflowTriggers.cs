using Elsa.Mediator.Contracts;
using Elsa.Models;
using Elsa.Persistence.Entities;

namespace Elsa.Persistence.Commands;

public record ReplaceWorkflowTriggers(Workflow Workflow, ICollection<WorkflowTrigger> WorkflowTriggers) : ICommand;