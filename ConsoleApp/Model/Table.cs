using System.Collections.Generic;

namespace ConsoleApp.Model
{
    public class Table : DatabaseObjectBase
    {
        public IList<Column> Columns;

        public Table()
        {
            Columns = new List<Column>();
        }
        public string Schema;
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public double NumberOfChildren { get => Columns.Count; }


    }
}
