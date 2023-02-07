using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    internal class ConsoleDataPrinter : IDataPrinter
    {
        private void PrintColumnInfo(DatabaseObject column)
        {
            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");

        }

        private void PrintDatabaseInfo(DatabaseObject database)
        {
            Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
        }


        private void PrintTableInfo(DatabaseObject table)
        {
            Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

        }

        public void PrintDatabaseObjects(IList<DatabaseObject> databaseObjects)
        {
            foreach (var database in databaseObjects)
            {
                if (database.Type == "DATABASE")
                {
                    PrintDatabaseInfo(database);

                    // print all database's tables
                    foreach (var table in databaseObjects)
                    {
                        if (table.ParentType.ToUpper() == database.Type && table.ParentName == database.Name)
                        {
                            PrintTableInfo(table);

                            // print all table's columns
                            foreach (var column in databaseObjects)
                            {
                                if (column.ParentType.ToUpper() == table.Type && column.ParentName == table.Name)
                                {
                                    PrintColumnInfo(column);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
