﻿using System;
using System.Linq.Expressions;
using Elsa.Models;

namespace Elsa.Persistence.Specifications
{
    public class WorkflowInstanceIdSpecification : Specification<WorkflowInstance>
    {
        public string Id { get; set; }
        public WorkflowInstanceIdSpecification(string id) => Id = id;
        public override Expression<Func<WorkflowInstance, bool>> ToExpression() => x => x.EntityId == Id;
    }
}