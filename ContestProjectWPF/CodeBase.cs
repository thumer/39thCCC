using ContestProjectWPF.ViewModel;
using LINQtoCSV;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
        }

        public void ExecuteFile(string fileName)
        {
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
            => ';';

        private string GetOutFileName(string fileName)
            => Path.GetFullPath(fileName ?? "input.txt") + ".out.txt";
    }

    public static class ContentHelper
    {
        private static IEnumerable<InputRowElement> ReadInputRowElements(string fileName)
        {
            var cc = new CsvContext();
            var fileDescription_logs = new CsvFileDescription
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
