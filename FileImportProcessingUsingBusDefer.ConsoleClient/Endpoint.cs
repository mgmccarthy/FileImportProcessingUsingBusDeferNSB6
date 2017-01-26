using System;
using System.Threading.Tasks;
using NServiceBus;

namespace FileImportProcessingUsingBusDefer.ConsoleClient
{
    public static class Endpoint
    {
        public static IEndpointInstance Instance { get; private set; }

        public static async Task Init()
        {
            Console.Title = "FileImportProcessingSagaNSB6.ConsoleClient";

            var endpointConfiguration = new EndpointConfiguration("FileImportProcessingUsingBusDefer.ConsoleClient");
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendOnly();

            Instance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        }
    }
}
