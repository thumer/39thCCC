using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace ContestProject
{
    public abstract class CodeBase<T> where T : class
    {
        public string Output { get; set; }

        public abstract string Execute(T row);

        public void ExecuteLine(string line)
        {
            if (typeof(T) == typeof(String))
            {
                Output = Execute(line as T);
            }
            else if (typeof(T) == typeof(InputRowElement))
            {
                string tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, line);

                IEnumerable<InputRowElement> rowElements = ReadInputRowElements(tempFile);

                Output = Execute(rowElements.First() as T);

                File.Delete(tempFile);
            }
            else if (typeof(T).IsGenericType && ((typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>)) || (typeof(T).GetGenericTypeDefinition().GetInterface(typeof(IEnumerable<>).FullName) != null)))
            {
                Type argumentType = typeof(T).GetGenericArguments()[0];

                if (argumentType == typeof(String))
                {
                    List<String> list = Helper.StringToList(line); // TODO: Hier müsste mann den Separator ändern
                    Output = Execute(list as T);
                }
                else
                {
                    object list = Helper.StringToList(argumentType, line); // TODO: Hier müsste mann den Separator ändern
                    Output = Execute(list as T);
                }
            }
        }

        public void ExecuteFile(string fileName)
        {
            StringBuilder sbOut = new StringBuilder();
            IEnumerable rows = null;

            if (typeof(T) == typeof(InputRowElement))
            {
                rows = ReadInputRowElements(fileName);
            }
            else 
            {
                rows = File.ReadLines(fileName);
            }
           
 
            foreach (object row in rows)
            {
                if (typeof(T).IsGenericType && ((typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>)) || (typeof(T).GetGenericTypeDefinition().GetInterface(typeof(IEnumerable<>).FullName) != null)))
                {
                    Type argumentType = typeof(T).GetGenericArguments()[0];
                    String value = row as string;

                    if (argumentType == typeof(String))
                    {
                        List<String> list = Helper.StringToList(value); // TODO: Hier müsste mann den Separator ändern
                        sbOut.AppendLine(Execute(list as T));
                    }
                    else
                    {
                        object list = Helper.StringToList(argumentType, value); // TODO: Hier müsste mann den Separator ändern
                        sbOut.AppendLine(Execute(list as T));
                    }
                }
                else
                {
                    sbOut.AppendLine(Execute(row as T));
                }
            }

            // Write output in File
            string outputFileName = GetOutFileName(fileName);
            File.WriteAllText(outputFileName, sbOut.ToString());
            Process.Start(outputFileName);
        }

        private IEnumerable<InputRowElement> ReadInputRowElements(string fileName)
        {
            CsvContext cc = new CsvContext();
            CsvFileDescription fileDescription_logs = new CsvFileDescription
            {
                SeparatorChar = ';', // default is ','
                FirstLineHasColumnNames = false,
                EnforceCsvColumnAttribute = true, // default is false
                FileCultureName = "de-AT" // default is the current culture
            };

            return cc.Read<InputRowElement>(fileName, fileDescription_logs);
        }

        private string GetOutFileName(string fileName)
        {
            return Path.GetFullPath(fileName) + Path.GetFileName(fileName) + ".out.txt";
        }
    }
}
