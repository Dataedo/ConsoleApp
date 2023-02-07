namespace ConsoleApp
{
    using ConsoleApp.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader : IDataReader
    {
        public IList<Database> ImportData(string fileToImport)
        {

            var importedLines = File.ReadAllLines(fileToImport).ToList();

            IList<Database> databases = new List<Database>();
            IList<Table> tables = new List<Table>();
            IList<Column> columns = new List<Column>();

            importedLines = importedLines.Where(importedLine => !String.IsNullOrEmpty(importedLine) && importedLine.Split(';').Length >= 7).ToList();

            databases = importedLines.Where(importedLine => importedLine.ToLower().StartsWith("database"))
                    .Select(dbLine => dbLine.Split(';'))
                    .Select(db => new Database() { Type = db[0], Name = db[1] })
                    .ToList();

            tables = importedLines.Where(importedLine => importedLine.ToLower().StartsWith("table"))
                    .Select(tblLine => tblLine.Split(';'))
                    .Select(tbl => new Table()
                    {
                        Type = tbl[0],
                        Name = tbl[1],
                        Schema = tbl[2],
                        ParentName = tbl[3],
                        ParentType = tbl[4]
                    })
                    .ToList();

            columns = importedLines.Where(importedLine => importedLine.ToLower().StartsWith("column"))
                    .Select(clnLine => clnLine.Split(';'))
                    .Select(cln => new Column()
                    {
                        Type = cln[0],
                        Name = cln[1],
                        Schema = cln[2],
                        ParentName = cln[3],
                        ParentType = cln[4],
                        DataType = cln[5],
                        IsNullable = cln[6]
                    })
                    .ToList();

            tables.ToList().ForEach(tbl => tbl.Columns = columns.Where(cl => cl.ParentName.ToLower() == tbl.Name.ToLower()).ToList());
            databases.ToList().ForEach(db => db.Tables = tables.Where(tbl => tbl.ParentName.ToLower() == db.Name.ToLower()).ToList());

            // clear and correct imported data
            ClearImportedData(databases);

            return databases;

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
