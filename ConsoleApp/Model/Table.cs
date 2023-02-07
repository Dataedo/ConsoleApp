using System.Collections.Generic;

namespace ConsoleApp.Model
{
    public class Table : DatabaseObjectBase
    {

        public Table()
        {
            Columns = new List<Column>();
        }
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public double NumberOfChildren { get => Columns.Count; }

        public IList<Column> Columns;


    }
}
