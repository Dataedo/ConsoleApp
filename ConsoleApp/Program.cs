using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader();
            var importedData = reader.ImportData("data.csv");

            var printer = new ConsoleDataPrinter();
            printer.PrintDatabaseObjects(importedData);

            Console.ReadKey();

        }
    }
}
