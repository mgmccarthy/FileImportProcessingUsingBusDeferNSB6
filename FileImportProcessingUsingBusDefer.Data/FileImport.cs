using System;

namespace FileImportProcessingUsingBusDefer.Data
{
    public class FileImport
    {
        public Guid Id { get; set; }
        public Guid ImportId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool Successful { get; set; }
    }
}
