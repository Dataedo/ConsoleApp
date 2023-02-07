namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader(new ConsoleDataPrinter());
            reader.ImportAndPrintData("data.csv");
        }
    }
}
