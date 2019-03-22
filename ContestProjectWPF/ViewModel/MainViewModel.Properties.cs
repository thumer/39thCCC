using ContestProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestProject.ViewModel
{
    public partial class MainViewModel
    {
        //public DelegateCommand StartJavaConnectCommand { get; set; }
        //public DelegateCommand DisconnectAndCloseCommand { get; set; }
        //public DelegateCommand StartCommand { get; set; }
        //public DelegateCommand StopCommand { get; set; }

        //public DelegateCommand ReadAndRunAndSendCommand { get; set; }
        //public DelegateCommand ReceiveCommand { get; set; }
        public DelegateCommand RunCommand { get; set; }
        public DelegateCommand RunFileCommand { get; set; }

        //public DelegateCommand SendCommand { get; set; }

        public DelegateCommand ClearOutputCommand { get; set; }


        private string _javaArguments;
        public string JavaArguments
        {
            get
            {
                return _javaArguments;
            }
            set
            {
                _javaArguments = value;
                RaisePropertyChanged(() => JavaArguments);
            }
        }

        private string _host;
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
                RaisePropertyChanged(() => Host);
            }
        }

        private int _port;
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                RaisePropertyChanged(() => Port);
            }
        }

        private string _curInput;
        public string CurInput
        {
            get
            {
                return _curInput;
            }
            set
            {
                _curInput = value;
                RaisePropertyChanged(() => CurInput);
            }
        }

        private string _curResult;
        public string CurResult
        {
            get
            {
                return _curResult;
            }
            set
            {
                _curResult = value;
                RaisePropertyChanged(() => CurResult);
            }
        }


        private string _output;
        public string Output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
                RaisePropertyChanged(() => Output);
            }
        }

        private int _delay;
        public int Delay
        {
            get
            {
                return _delay;
            }
            set
            {
                _delay = value;
                RaisePropertyChanged(() => Delay);
            }
        }

        private bool _showOutput;
        public bool ShowOutput
        {
            get
            {
                return _showOutput;
            }
            set
            {
                _showOutput = value;
                RaisePropertyChanged(() => ShowOutput);
            }
        }
        
        private string _filename;
        public string Filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
                RaisePropertyChanged(() => Filename);
            }
        }


        private string _param1;
        public string Param1
        {
            get
            {
                return _param1;
            }
            set
            {
                _param1 = value;
                RaisePropertyChanged(() => Param1);
            }
        }
        private string _param2;
        public string Param2
        {
            get
            {
                return _param2;
            }
            set
            {
                _param2 = value;
                RaisePropertyChanged(() => Param2);
            }
        }
    }
}
