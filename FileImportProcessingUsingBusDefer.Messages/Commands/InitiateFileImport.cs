using System;

namespace FileImportProcessingUsingBusDefer.Messages.Commands
{
    public class InitiateFileImport
    {
        public Guid ImportId { get; set; }
        public int TotalNumberOfFilesInImport { get; set; }
    }
}
