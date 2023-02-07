namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader
    {

        public IList<DatabaseObject> ImportData(string fileToImport, bool printData = true)
        {
            var importedObjects = new List<DatabaseObject>();

            var importedLines = File.ReadAllLines(fileToImport);

            for (int i = 0; i < importedLines.Count(); i++)
            {

                var importedLine = importedLines[i];
                if (String.IsNullOrEmpty(importedLine))
                    continue;


                var values = importedLine.Split(';');
                if (values.Length < 7)
                    continue;


                var importedObject = new DatabaseObject
                {
                    Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6]
                };

                importedObjects.Add(importedObject);
            }

            // clear and correct imported data
            ClearImportedData(importedObjects);

            importedObjects.ToList().ForEach(obj => obj.NumberOfChildren = importedObjects.Count(x => x.ParentType == obj.Type && x.ParentName == obj.Name));

            return importedObjects;

        }



        private void ClearImportedData(IList<DatabaseObject> ImportedObjects)
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

}
