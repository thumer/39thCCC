using ContestProject.ViewModel;
using System.Diagnostics;
using System.IO;

namespace ContestProject
{
    public abstract class CodeBase
    {
        public MainViewModel ViewModel { get; set; }

        public string Output { get; set; }

        public abstract string Execute(string input);

        public void ExecuteInput(string line)
            => Output = Execute(line?.Trim() ?? string.Empty);

        public void ExecuteFile(string fileName)
        {
            var content = string.Empty;
            if (fileName != null && File.Exists(fileName))
                content = File.ReadAllText(fileName);
            ViewModel.CurInput = content;

            string output = Execute(content);

            // Write output in File
            string outputFileName = GetOutFileName(fileName);
            File.WriteAllText(outputFileName, output);
            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
            Output = output;
        }

        private string GetOutFileName(string fileName)
            => Path.GetFullPath(fileName ?? "input.txt") + ".out.txt";
    }
}
