using System.Collections.Generic;

namespace ConsoleApp.Model
{
    public class Database : DatabaseObjectBase
    {
        public IList<Table> Tables { get; set; }

        public Database()
        {
            Tables = new List<Table>();
        }

        public double NumberOfChildren { get => Tables.Count; }
    }
}
