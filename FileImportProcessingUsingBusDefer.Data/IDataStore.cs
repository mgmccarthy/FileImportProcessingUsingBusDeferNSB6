namespace FileImportProcessingUsingBusDefer.Data
{
    public interface IDataStore
    {
        ISession OpenSession();
    }
}
