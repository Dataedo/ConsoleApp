using System;

namespace ConsoleApp
{
    internal class ConsoleDataPrinter : IDataPrinter
    {
        public void PrintColumnInfo(DatabaseObject column)
        {
            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");

        }

        public void PrintDatabaseInfo(DatabaseObject database)
        {
            Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
        }

        public void PrintTableInfo(DatabaseObject table)
        {
            Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

        }
    }
}
