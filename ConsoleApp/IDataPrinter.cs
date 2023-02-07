using ConsoleApp.Model;
using System.Collections.Generic;

namespace ConsoleApp
{
    internal interface IDataPrinter
    {
        void PrintDatabaseObjects(IList<Database> databaseObjects);
    }
}