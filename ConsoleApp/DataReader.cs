namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader
    {

        IList<ImportedObject> ImportedObjects;
        private IDataPrinter _dataPrinter;


        public DataReader(IDataPrinter dataPrinter)
        {
            this._dataPrinter = dataPrinter;
        }

        public void ImportAndPrintData(string fileToImport, bool printData = true)
        {
            ImportedObjects = new List<ImportedObject>();

            var importedLines = File.ReadAllLines(fileToImport);

            for (int i = 0; i < importedLines.Count(); i++)
            {

                var importedLine = importedLines[i];
                if (String.IsNullOrEmpty(importedLine))
                    continue;


                var values = importedLine.Split(';');
                if (values.Length < 7)
                    continue;


                var importedObject = new ImportedObject
                {
                    Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6]
                };

                ImportedObjects.Add(importedObject);
            }

            // clear and correct imported data
            ClearImportedData(ImportedObjects);

            foreach (var importedObject in ImportedObjects)
            {
                importedObject.NumberOfChildren = ImportedObjects.Count(x => x.ParentType == importedObject.Type && x.ParentName == importedObject.Name);
            }

            foreach (var database in ImportedObjects)
            {
                if (database.Type == "DATABASE")
                {
                    _dataPrinter.PrintDatabaseInfo(database);

                    // print all database's tables
                    foreach (var table in ImportedObjects)
                    {
                        if (table.ParentType.ToUpper() == database.Type && table.ParentName == database.Name)
                        {
                            _dataPrinter.PrintTableInfo(table);

                            // print all table's columns
                            foreach (var column in ImportedObjects)
                            {
                                if (column.ParentType.ToUpper() == table.Type && column.ParentName == table.Name)
                                {
                                    _dataPrinter.PrintColumnInfo(column);
                                }
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }

        private void ClearImportedData(IList<ImportedObject> ImportedObjects)
        {
            foreach (var importedObject in ImportedObjects)
            {
                importedObject.Type = importedObject.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
                importedObject.Name = importedObject.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.Schema = importedObject.Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.ParentName = importedObject.ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.ParentType = importedObject.ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            }
        }
    }

    public class ImportedObject : ImportedObjectBaseClass
    {
        public string Name
        {
            get;
            set;
        }
        public string Schema;

        public string ParentName;
        public string ParentType
        {
            get; set;
        }

        public string DataType { get; set; }
        public string IsNullable { get; set; }

        public double NumberOfChildren;
    }

    public class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
