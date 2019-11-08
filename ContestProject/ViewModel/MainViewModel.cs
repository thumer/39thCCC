using ContestProject.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ContestProject.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            RunCommand = new DelegateCommand(() => RunAsync());
            RunFileCommand = new DelegateCommand(() => RunFileAsync());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            ClearOutputCommand = new DelegateCommand(ClearOutput, CanClearOutput);
        }

        protected void InvokeInUI(Action action, DispatcherPriority priority = DispatcherPriority.Send)
            => Application.Current.Dispatcher.Invoke(action, priority);

        private async Task RunAsync()
            => CurResult = await RunCodeAsync(CurInput);

        private async Task RunFileAsync()
            => CurResult = await RunFileAsync(Filename);

        private void ClearOutput()
            => Output = string.Empty;

        private bool CanClearOutput()
            => !string.IsNullOrEmpty(Output);

        private void WriteToOutput(string str)
            => Output = Output + str;

        private Task<string> RunCodeAsync(string input)
            => Task.Run(() => RunCode(input));

        private Task<string> RunFileAsync(string file)
            => Task.Run(() => RunFile(file));

        private string RunCode(string input)
        {
            var algorithm = new Code
            {
                ViewModel = this
            };
            algorithm.ExecuteInput(input);
            return algorithm.Output;
        }

        private string RunFile(string input)
        {
            var algorithm = new Code
            {
                ViewModel = this
            };
            algorithm.ExecuteFile(input);
            return algorithm.Output;
        }
    }
}