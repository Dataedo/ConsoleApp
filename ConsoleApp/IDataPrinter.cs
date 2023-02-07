namespace ConsoleApp
{
    public interface IDataPrinter
    {
        void PrintDatabaseInfo(DatabaseObject database);
        void PrintTableInfo(DatabaseObject table);
        void PrintColumnInfo(DatabaseObject column);
    }
}
