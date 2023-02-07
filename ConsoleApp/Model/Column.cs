namespace ConsoleApp.Model
{
    public class Column : DatabaseObjectBase
    {
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
    }
}
