using System.Threading;
using System.Threading.Tasks;
using Elsa.WorkflowSettings.Abstractions.Providers;
using Microsoft.Extensions.Configuration;

namespace Elsa.WorkflowSettings.Providers
{
    public class ConfigurationWorkflowSettingsProvider : WorkflowSettingsProvider
    {
        private readonly IConfiguration _configuarion;

        public ConfigurationWorkflowSettingsProvider(IConfiguration configuarion)
        {
            _configuarion = configuarion;
        }

        public override ValueTask<object> GetWorkflowSettingAsync(string workflowBlueprintId, string key, CancellationToken cancellationToken)
        {
            var value = _configuarion[$"{workflowBlueprintId}:{key}"];
            return new ValueTask<object>(value);
        }
    }
}