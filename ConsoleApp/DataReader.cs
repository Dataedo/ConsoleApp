namespace ConsoleApp
{
    using ConsoleApp.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader
    {


        public IList<Database> ImportData(string fileToImport, bool printData = true)
        {

            var importedLines = File.ReadAllLines(fileToImport).ToList();
            IList<Database> databases = new List<Database>();
            IList<Table> tables = new List<Table>();
            IList<Column> columns = new List<Column>();

            importedLines = importedLines.Where(importedLine => !String.IsNullOrEmpty(importedLine) && importedLine.Split(';').Length >= 7).ToList();

            importedLines.Where(importedLine => importedLine.ToLower().StartsWith("database")).ToList().ForEach(databaseLine => databases.Add(MapLineToDatabaseObject(databaseLine)));
            importedLines.Where(importedLine => importedLine.ToLower().StartsWith("table")).ToList().ForEach(tableLine => tables.Add(MapLineToTableObject(tableLine)));
            importedLines.Where(importedLine => importedLine.ToLower().StartsWith("column")).ToList().ForEach(columnLine => columns.Add(MapLineToColumnObject(columnLine)));

            tables.ToList().ForEach(tbl => tbl.Columns = columns.Where(cl => cl.ParentName.ToLower() == tbl.Name.ToLower()).ToList());
            databases.ToList().ForEach(db => db.Tables = tables.Where(tbl => tbl.ParentName.ToLower() == db.Name.ToLower()).ToList());

            // clear and correct imported data
            ClearImportedData(databases);

            return databases;

        }

        private Column MapLineToColumnObject(string columnLine)
        {
            var splittedLine = columnLine.Split(';');

            return new Column()
            {
                Type = splittedLine[0],
                Name = splittedLine[1],
                Schema = splittedLine[2],
                ParentName = splittedLine[3],
                ParentType = splittedLine[4],
                DataType = splittedLine[5],
                IsNullable = splittedLine[6]
            };
        }
        private Table MapLineToTableObject(string tableLine)
        {
            var splittedLine = tableLine.Split(';');
            return new Table()
            {
                Type = splittedLine[0],
                Name = splittedLine[1],
                Schema = splittedLine[2],
                ParentName = splittedLine[3],
                ParentType = splittedLine[4]
            };
        }

        private Database MapLineToDatabaseObject(string databaseLine)
        {
            var splittedLine = databaseLine.Split(';');
            return new Database() { Type = splittedLine[0], Name = splittedLine[1] };
        }

        private void ClearImportedData(IList<Database> ImportedObjects)
        {
            ImportedObjects.ToList().ForEach(db =>
            {
                db.Type = db.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
                db.Name = db.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                db.Tables.ToList().ForEach(tbl =>
                {
                    tbl.Type = tbl.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
                    tbl.Name = tbl.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                    tbl.Schema = tbl.Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                    tbl.ParentName = tbl.ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                    tbl.ParentType = tbl.ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "");

                    tbl.Columns.ToList().ForEach(cln =>
                    {
                        cln.Type = cln.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
                        cln.Name = cln.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                        cln.Schema = cln.Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                        cln.ParentName = cln.ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                        cln.ParentType = cln.ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                        cln.DataType = cln.DataType.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                    });

                });
            });

        }
    }

}
