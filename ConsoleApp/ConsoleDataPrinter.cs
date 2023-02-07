using ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    internal class ConsoleDataPrinter : IDataPrinter
    {
        public void PrintDatabaseObjects(IList<Database> databaseObjects)
        {
            databaseObjects.ToList().ForEach(database =>
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                database.Tables.ToList().ForEach(table =>
                {
                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                    table.Columns.ToList().ForEach(column =>
                    {
                        Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                    });
                });
            });

        }
    }
}
