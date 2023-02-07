using System;

namespace ConsoleApp
{
    internal class ConsoleDataPrinter : IDataPrinter
    {
        public void PrintColumnInfo(ImportedObject column)
        {
            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");

        }

        public void PrintDatabaseInfo(ImportedObject database)
        {
            Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
        }

        public void PrintTableInfo(ImportedObject table)
        {
            Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

        }
    }
}
