using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IDataPrinter
    {

        void PrintDatabaseObjects(IList<DatabaseObject> databaseObjects);
    }
}
