using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.DataStructure;

namespace ConsoleApp
{
	public class DataReader
	{
		public List<ImportedObject> ImportedObjects;
		public string Path;

		public DataReader(string path)
		{
			Path = path;
			ImportedObjects = new List<ImportedObject>();
		}

		public void ImportData()
		{
			var fnName = nameof(ImportData);

			try
			{
				Console.WriteLine($"Starting: {fnName}");

				if (!File.Exists(Path))
					throw new FileNotFoundException($"Path: {Path}");

				var fileLines = ReadDataFromFile();
				ImportObjects(fileLines);
				ClearAndCorrectImportedData();
				AssignNumberOfChildren();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Something went wrong, details: {ex}");
			}
			finally
			{
				Console.WriteLine($"Finished: {fnName}");
			}
		}

		private void AssignNumberOfChildren()
		{
			var fnName = nameof(AssignNumberOfChildren);
			Console.WriteLine($"Starting: {fnName}");

			foreach (var importedObject in ImportedObjects)
			{
				foreach (var impObj in ImportedObjects.Where(a=>a.ParentType == importedObject.Type && a.ParentName == importedObject.Name))
				{
					importedObject.NumberOfChildren++;
				}
			}

			Console.WriteLine($"Finished: {fnName}");
		}

		private void ClearAndCorrectImportedData()
		{
			var fnName = nameof(ClearAndCorrectImportedData);
			Console.WriteLine($"Starting: {fnName}");
			ImportedObjects.ForEach(a =>
			{
				CorrectData(a.Type);
				a.Type = a.Type.ToUpper();
				CorrectData(a.Name);
				CorrectData(a.Schema);
				CorrectData(a.ParentName);
				CorrectData(a.ParentType);
			});
			Console.WriteLine($"Finished: {fnName}");
		}

		readonly Func<string, string> CorrectData = x => x.Trim().Replace(" ", "").Replace(Environment.NewLine, "");

		private void ImportObjects(List<string> fileLines)
		{
			var fnName = nameof(ImportObjects);
			Console.WriteLine($"Starting: {fnName}");

			for (int i = 0; i < fileLines.Count; i++)
			{
				var values = fileLines[i].Split(';');
				if (values.Length == 7)
				{
					var importedObject = new ImportedObject()
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
				else
				{
					Console.WriteLine($"Incorrect data in file: {Path} at line: {i}, data: {string.Join(";", values)}");
				}
			}
			Console.WriteLine($"Finished: {fnName}");
		}

		private List<string> ReadDataFromFile()
		{
			var fnName = nameof(ReadDataFromFile);
			Console.WriteLine($"Starting: {fnName}");

			var result = new List<string>();

			using (var streamReader = new StreamReader(Path))
			{
				var skipFirstLine = streamReader.ReadLine();
				while (!streamReader.EndOfStream)
				{
					result.Add(streamReader.ReadLine());
				}
			}

			Console.WriteLine($"Finished: {fnName}");
			return result;
		}

		public void PrintData()
		{
			foreach (var database in ImportedObjects.Where(a => a.Type.Equals("DATABASE")))
			{
				Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

				// print all database's tables
				foreach (var table in ImportedObjects.Where(a => a.ParentType == database.Type && a.ParentName == database.Name))
				{
					Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

					// print all table's columns
					foreach (var column in ImportedObjects.Where(a => a.ParentType == table.Type & a.ParentName == table.Name))
					{
						Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
					}
				}
			}
		}
	}
}

