using ConsoleApp.Model;
using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IDataReader
    {
        IList<Database> ImportData(string fileToImport);
    }
}