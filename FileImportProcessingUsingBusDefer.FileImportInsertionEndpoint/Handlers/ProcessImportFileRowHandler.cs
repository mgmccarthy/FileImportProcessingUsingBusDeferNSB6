using System;
using System.Threading.Tasks;
using FileImportProcessingUsingBusDefer.Data;
using FileImportProcessingUsingBusDefer.Messages.Commands;
using FileImportProcessingUsingBusDefer.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace FileImportProcessingUsingBusDefer.FileImportInsertionEndpoint.Handlers
{
    public class ProcessImportFileRowHandler : IHandleMessages<ProcessImportFileRow>
    {
        private readonly IDataStore dataStore;

        public ProcessImportFileRowHandler(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public async Task Handle(ProcessImportFileRow message, IMessageHandlerContext context)
        {
            if (message.FirstImportRowForThisImport)
                await context.Send(new InitiateFileImport { ImportId = message.ImportId, TotalNumberOfFilesInImport = message.TotalNumberOfFilesInImport });

            var success = new Random().Next(100) % 2 == 0;
            LogManager.GetLogger(typeof(ProcessImportFileRowHandler)).WarnFormat($"Handling ProcessImportFileRow for Customer: {message.CustomerId}");

            using (var session = dataStore.OpenSession())
            {
                session.Add(new FileImport { Id = Guid.NewGuid(), ImportId = message.ImportId, CustomerId = message.CustomerId, CustomerName = message.CustomerName, Successful = success });
                await session.SaveChangesAsync();
            }
        }
    }
}
