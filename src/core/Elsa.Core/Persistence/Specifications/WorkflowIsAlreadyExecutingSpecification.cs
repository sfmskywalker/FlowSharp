﻿using System;
using System.Linq.Expressions;
using Elsa.Models;

namespace Elsa.Persistence.Specifications
{
    public class WorkflowIsAlreadyExecutingSpecification : Specification<WorkflowInstance>
    {
        public override Expression<Func<WorkflowInstance, bool>> ToExpression() => x => x.WorkflowStatus == WorkflowStatus.Running || x.WorkflowStatus == WorkflowStatus.Suspended;
    }
}