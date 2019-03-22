using ContestProject.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ContestProject.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private Task _curTask;

        public MainViewModel()
        {
            RunCommand = new DelegateCommand(() => RunAsync());
            RunFileCommand = new DelegateCommand(() => RunFileAsync());

            ClearOutputCommand = new DelegateCommand(ClearOutput, CanClearOutput);
        }

        protected void InvokeInUI(Action action, DispatcherPriority priority = DispatcherPriority.Send)
        {
            Application.Current.Dispatcher.Invoke(action, priority);
        }

        private async Task RunAsync()
        {
            var task = RunCodeAsync(CurInput);
            _curTask = task;
            string result = await task;
            CurResult = result;
        }

        private async Task RunFileAsync()
        {
            var task = RunFileAsync(Filename);
            _curTask = task;
            string result = await task;
            CurResult = result;
        }

        private void ClearOutput()
        {
            Output = String.Empty;
        }

        private bool CanClearOutput()
        {
            return !String.IsNullOrEmpty(Output);
        }

        private void WriteToOutput(string str)
        {
            Output = Output + str;
        }

        private Task<string> RunCodeAsync(string input)
        {
            return Task.Run(() => RunCode(input));
        }

        private Task<string> RunFileAsync(string file)
        {
            return Task.Run(() => RunFile(file));
        }

        private string RunCode(string input)
        {
            Code algorithm = new Code();
            algorithm.ViewModel = this;
            algorithm.ExecuteInput(input);
            return algorithm.Output;
        }

        private string RunFile(string input)
        {
            Code algorithm = new Code();
            algorithm.ViewModel = this;
            algorithm.ExecuteFile(input);
            return algorithm.Output;
        }
    }
}