using ConsoleApp.Model;
using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IDataPrinter
    {
        void PrintDatabaseObjects(IList<Database> databaseObjects);
    }
}
