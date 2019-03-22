using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;
using System.IO;
using System.Diagnostics;
using System.Collections;
using ContestProjectWPF.ViewModel;
using ContestProjectWPF.Utils;

namespace ContestProjectWPF
{
    public abstract class CodeBase
    {
        public MainViewModel ViewModel { get; set; }

        public string Output { get; set; }

        public abstract string Execute(string input);

        public void ExecuteInput(string line)
        {
            line = (line ?? string.Empty).Trim();
            Output = Execute(line);

            //else if (typeof(T).IsGenericType && ((typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>)) || (typeof(T).GetGenericTypeDefinition().GetInterface(typeof(IEnumerable<>).FullName) != null)))
            //{
            //    Type argumentType = typeof(T).GetGenericArguments()[0];

            //    if (argumentType == typeof(String))
            //    {
            //        List<String> list = Helper.StringToList(line); // TODO: Hier müsste mann den Separator ändern
            //        Output = Execute(list as T);
            //    }
            //    else
            //    {
            //        object list = Helper.StringToList(argumentType, line); // TODO: Hier müsste mann den Separator ändern
            //        Output = Execute(list as T);
            //    }
            //}
        }

        public void ExecuteFile(string fileName)
        {
            //StringBuilder sbOut = new StringBuilder();
            //IEnumerable rows = null;

            //foreach (object row in rows)
            //{
            //    if (typeof(T).IsGenericType && ((typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>)) || (typeof(T).GetGenericTypeDefinition().GetInterface(typeof(IEnumerable<>).FullName) != null)))
            //    {
            //        Type argumentType = typeof(T).GetGenericArguments()[0];
            //        String value = row as string;

            //        if (argumentType == typeof(String))
            //        {
            //            List<String> list = Helper.StringToList(value); // TODO: Hier müsste mann den Separator ändern
            //            sbOut.AppendLine(Execute(list as T));
            //        }
            //        else
            //        {
            //            object list = Helper.StringToList(argumentType, value); // TODO: Hier müsste mann den Separator ändern
            //            sbOut.AppendLine(Execute(list as T));
            //        }
            //    }
            //    else
            //    {
            //        sbOut.AppendLine(Execute(row as T));
            //    }
            //}

            string content = string.Empty;
            if (fileName != null && File.Exists(fileName))
                content = File.ReadAllText(fileName);
            ViewModel.CurInput = content;

            string output = Execute(content);

            // Write output in File
            string outputFileName = GetOutFileName(fileName);
            File.WriteAllText(outputFileName, output);
            Process.Start(outputFileName);
            Output = output;
        }

        protected virtual char GetSeparator()
        {
            return ';';
        }

        private string GetOutFileName(string fileName)
        {
            return System.IO.Path.GetFullPath(fileName ?? "input.txt") + ".out.txt";
        }
    }

    public static class ContentHelper
    {
        //public static IEnumerable<InputRowElement> ParseInputRowElements(string input)
        //{
        //    input.Replace("\r\n", "\n");
        //    string tempFile = System.IO.Path.GetTempFileName();
        //    File.WriteAllText(tempFile, input);

        //    IEnumerable<InputRowElement> rowElements = ReadInputRowElements(tempFile);
        //    return rowElements;
        //}



        private static IEnumerable<InputRowElement> ReadInputRowElements(string fileName)
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
    }
}
