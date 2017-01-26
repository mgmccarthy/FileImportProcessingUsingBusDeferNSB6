using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FileImportProcessingUsingBusDefer.Data;
using FileImportProcessingUsingBusDefer.Messages.Commands;
using FileImportProcessingUsingBusDefer.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace FileImportProcessingUsingBusDefer.FileImportInitiatedEndpoint.Handlers
{
    public class FileImportInitiatedHandler : IHandleMessages<InitiateFileImport>
    {
        private readonly IDataStore dataStore;
        private static readonly ILog Log = LogManager.GetLogger(typeof(FileImportInitiatedHandler));

        public FileImportInitiatedHandler(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public async Task Handle(InitiateFileImport message, IMessageHandlerContext context)
        {
            int rowsSucceeded;
            int rowsFailed;
            using (var session = dataStore.OpenSession())
            {
                rowsSucceeded = await session.Query<FileImport>().Where(x => x.ImportId == message.ImportId).CountAsync(x => x.Successful);
                rowsFailed = await session.Query<FileImport>().Where(x => x.ImportId == message.ImportId).CountAsync(x => !x.Successful);
            }

            var logMessage = $"RowsSucceeded: {rowsSucceeded}, RowsFailed: {rowsFailed}, TotalNumberOfFilesInImport: {message.TotalNumberOfFilesInImport}";

            if (rowsSucceeded + rowsFailed == message.TotalNumberOfFilesInImport)
            {
                Log.Warn(logMessage + " import completed");
                await context.Publish(new FileImportCompleted { ImportId = message.ImportId });
            }
            else
            {
                Log.Warn(logMessage + " import not completed. Checking again for complete in 5 seconds.\n\r");

                var options = new SendOptions();
                options.DelayDeliveryWith(new TimeSpan(0, 0, 5));
                options.RouteToThisEndpoint();
                await context.Send(message, options).ConfigureAwait(false);
            }
        }
    }
}
