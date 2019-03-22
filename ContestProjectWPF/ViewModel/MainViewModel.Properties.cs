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
            get => _curInput;
            set => this.Set(ref _curInput, value);
        }

        private string _curResult;
        public string CurResult
        {
            get => _curResult;
            set => this.Set(ref _curResult, value);
        }

        private string _output;
        public string Output
        {
            get => _output;
            set => this.Set(ref _output, value);
        }

        private string _filename;
        public string Filename
        {
            get => _filename;
            set => this.Set(ref _filename, value);
        }
    }
}
