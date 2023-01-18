using System;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        public static string FileName = "data.csv";
        public static string PathToTheCsvFile = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);

        static void Main(string[] args)
        {
            var reader = new DataReader(PathToTheCsvFile);
            reader.ImportData();
            reader.PrintData();
            Console.ReadLine();
        }
    }
}
