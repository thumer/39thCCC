using ContestProject.Utils;

namespace ContestProject.ViewModel
{
    public partial class MainViewModel
    {
        public DelegateCommand RunCommand { get; }
        public DelegateCommand RunFileCommand { get; }

        public DelegateCommand ClearOutputCommand { get; }

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
