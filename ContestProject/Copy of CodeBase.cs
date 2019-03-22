//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using LINQtoCSV;
//using System.IO;

//namespace ContestProject
//{
//    public abstract class CodeBase
//    {
//        public string Output { get; set; }

//        public abstract string Execute(InputRowElement row);

//        public void ExecuteLine(string line)
//        {
//            string tempFile = Path.GetTempFileName();
//            File.WriteAllText(tempFile, line);

//            var rowElements = ReadFile(tempFile);
//            File.Delete(tempFile);

//            Output = Execute(rowElements.First());
//        }

//        public void ExecuteFile(string fileName)
//        {
//            string outputFileName = GetOutFileName(fileName);
//            var rowElements = ReadFile(fileName);

//            StringBuilder sbOut = new StringBuilder();
//            foreach (var rowElement in rowElements)
//            {
//                sbOut.AppendLine(Execute(rowElement));
//            }
//            File.WriteAllText(outputFileName, sbOut.ToString());

//            System.Diagnostics.Process.Start(outputFileName);
//        }

//        private IEnumerable<InputRowElement> ReadFile(string fileName)
//        {
//            CsvContext cc = new CsvContext();
//            CsvFileDescription fileDescription_logs = new CsvFileDescription
//            {
//                SeparatorChar = ';', // default is ','
//                FirstLineHasColumnNames = true,
//                EnforceCsvColumnAttribute = false, // default is false
//                FileCultureName = "de-AT" // default is the current culture
//            };

//            return cc.Read<InputRowElement>(fileName, fileDescription_logs);
//        }

//        private string GetOutFileName(string fileName)
//        {
//            return Path.GetFullPath(fileName) + Path.GetFileName(fileName) + ".out.txt";
//        }
//    }
//}
