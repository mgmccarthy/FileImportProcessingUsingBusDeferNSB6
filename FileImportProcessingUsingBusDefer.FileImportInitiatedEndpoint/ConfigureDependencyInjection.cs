using FileImportProcessingUsingBusDefer.Data;
using NServiceBus;

namespace FileImportProcessingUsingBusDefer.FileImportInitiatedEndpoint
{
    public class ConfigureDependencyInjection : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.RegisterComponents(reg => reg.ConfigureComponent<DataStore>(DependencyLifecycle.InstancePerCall));
        }
    }
}
