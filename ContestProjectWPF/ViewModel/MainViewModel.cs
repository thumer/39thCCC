using ContestProject.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ContestProject.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        //private TCPHelper _tcpHelper = new TCPHelper();
        //private bool _autoRun = false;
        private Task _curTask;
        //private Process _javaProcess;

        public MainViewModel()
        {
            //Host = "localhost";
            //Port = 7000;
            //JavaArguments = "-jar simulator.jar --level=1 --tcp=7000";
            //Delay = 0;
            //ShowOutput = true;

            //KillProcessOnStart();

            //StartJavaConnectCommand = new DelegateCommand(StartJavaConnect, CanStartJavaConnect);
            //DisconnectAndCloseCommand = new DelegateCommand(DisconnectAndClose, CanDisconnectAndClose);
            //StartCommand = new DelegateCommand(Start, CanStart);
            //StopCommand = new DelegateCommand(Stop, CanStop);

            //ReadAndRunAndSendCommand = new DelegateCommand(() => ReadAndRunAndSend(), CanReadAndRunAndSend);
            //ReceiveCommand = new DelegateCommand(() => Receive(), CanRead);
            RunCommand = new DelegateCommand(() => RunAsync()/*, CanRun*/);
            RunFileCommand = new DelegateCommand(() => RunFileAsync());
            //SendCommand = new DelegateCommand(() => Send(), CanSend);

            ClearOutputCommand = new DelegateCommand(ClearOutput, CanClearOutput);
        }

        protected void InvokeInUI(Action action, DispatcherPriority priority = DispatcherPriority.Send)
        {
            Application.Current.Dispatcher.Invoke(action, priority);
        }
        //private void KillProcessOnStart()
        //{
        //    foreach (Process p in Process.GetProcessesByName("java"))
        //        p.Kill();
        //}

        //private bool CanStartJavaConnect()
        //{
        //    return !_tcpHelper.IsConnected;
        //}

        //private void StartJavaConnect()
        //{
        //    try
        //    {
        //        if (_javaProcess != null && !_javaProcess.HasExited)
        //            _javaProcess.Kill();

        //        CurInput = String.Empty;
        //        CurResult = String.Empty;
        //        Output = String.Empty;

        //        ProcessStartInfo startInfo = new ProcessStartInfo("java.exe", JavaArguments);
        //        startInfo.UseShellExecute = false;
        //        startInfo.CreateNoWindow = false;

        //        _javaProcess = Process.Start(startInfo);
        //        Thread.Sleep(100);
        //        _tcpHelper.Connect(Host, Port);
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("{0}: {1}", e.GetType().Name, e.Message));
        //    }
        //}

        //private bool CanDisconnectAndClose()
        //{
        //    return _tcpHelper.IsConnected;
        //}

        //private void DisconnectAndClose()
        //{
        //    try
        //    {
        //        _tcpHelper.Disconnect();
        //        Thread.Sleep(100);

        //        if (_javaProcess != null && !_javaProcess.HasExited)
        //            _javaProcess.Kill();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("{0}: {1}", e.GetType().Name, e.Message));
        //    }
        //}

        //private async void Start()
        //{
        //    _autoRun = true;

        //    while (_autoRun)
        //    {
        //        await RunCodeAutoAsync();
        //        await Task.Delay(Delay);
        //    }
        //}

        //private bool CanStart()
        //{
        //    return _tcpHelper.IsConnected && !_autoRun && (_curTask == null || _curTask.Status != TaskStatus.Running);
        //}

        //private void Stop()
        //{
        //    _autoRun = false;
        //}

        //private bool CanStop()
        //{
        //    return _tcpHelper.IsConnected && _autoRun;
        //}

        //private async Task ReadAndRunAndSend()
        //{
        //    await Receive();
        //    await Run();
        //    await Send();
        //}

        //private bool CanReadAndRunAndSend()
        //{
        //    return _tcpHelper.IsConnected && !_autoRun && (_curTask == null || _curTask.Status != TaskStatus.Running);
        //}

        //private async Task Receive()
        //{
        //    var task = _tcpHelper.ReceiveMessageAsync();
        //    _curTask = task;
        //    CurInput = await task;
        //    WriteToOutput("Received:\n" + CurInput + "\n");
        //}

        //private bool CanRead()
        //{
        //    return _tcpHelper.IsConnected && !_autoRun && (_curTask == null || _curTask.Status != TaskStatus.Running);
        //}
        
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

        //private bool CanRun()
        //{
        //    return _tcpHelper.IsConnected && !String.IsNullOrEmpty(CurInput) && !_autoRun && (_curTask == null || _curTask.Status != TaskStatus.Running);
        //}

        //private async Task Send()
        //{
        //    WriteToOutput("Sent:\n" + CurResult + "\n");
        //    await (_curTask = _tcpHelper.SendMessageAsync(CurResult));
        //}

        //private bool CanSend()
        //{
        //    return _tcpHelper.IsConnected && !String.IsNullOrEmpty(CurResult) && !_autoRun && (_curTask == null || _curTask.Status != TaskStatus.Running);
        //}

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

        //private Task RunCodeAutoAsync()
        //{
        //    _curTask = Task.Run(() => RunCodeAuto());
        //    return _curTask;
        //}

        //private void RunCodeAuto()
        //{
        //    string input = _tcpHelper.ReceiveMessage();
        //    if (ShowOutput)
        //    {
        //        InvokeInUI(() =>
        //        {
        //            CurInput = input;
        //            WriteToOutput("Received:\n" + input + "\n");
        //        });
        //    }
        //    string result = RunCode(input);
        //    if (ShowOutput)
        //    {
        //        InvokeInUI(() =>
        //        {
        //            CurResult = result;
        //            WriteToOutput("Sent:\n" + result + "\n");
        //        });
        //    }
        //    _tcpHelper.SendMessage(result);
        //}

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