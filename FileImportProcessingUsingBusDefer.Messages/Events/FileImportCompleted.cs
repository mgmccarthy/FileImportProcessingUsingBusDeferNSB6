using System;

namespace FileImportProcessingUsingBusDefer.Messages.Events
{
    public class FileImportCompleted
    {
        public Guid ImportId { get; set; }
    }
}
