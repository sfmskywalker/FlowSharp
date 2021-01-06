using AutoMapper;

using Elsa.Indexing.Models;
using Elsa.Models;

namespace Elsa.Indexing.Profiles
{
    public class ElasticsearchProfile : Profile
    {
        public ElasticsearchProfile()
        {
            CreateMap<WorkflowInstance, ElasticWorkflowInstance>();
            CreateMap<WorkflowDefinition, ElasticWorkflowInstance>();

            CreateMap<ElasticWorkflowInstance, WorkflowInstanceIndexModel>();
            CreateMap<ElasticWorkflowInstance, WorkflowDefinitionIndexModel>();
        }
    }
}
