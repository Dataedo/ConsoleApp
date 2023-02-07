namespace ConsoleApp
{
    public interface IDataPrinter
    {
        void PrintDatabaseInfo(ImportedObject database);
        void PrintTableInfo(ImportedObject table);
        void PrintColumnInfo(ImportedObject column);
    }
}
