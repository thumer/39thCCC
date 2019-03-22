using System;
using System.Windows.Input;

namespace ContestProject.Utils
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action execute, Func<bool> canExecute = null, bool autoDetectCanExecuteChanged = true, bool allowMultipleExcecutes = false)
            : base(_ => execute(), canExecute != null ? _ => canExecute() : (Func<object, bool>)null, autoDetectCanExecuteChanged, allowMultipleExcecutes)
        { }
    }

    public class DelegateCommand<T> : ObservableObject, ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;
        private readonly bool _allowMultipleExcecutes;

        private bool _commandIsExecuting = false;
        private bool _raiseCanExecuteChangedAfterExecute = false;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null, bool autoDetectCanExecuteChanged = true, bool allowMultipleExcecutes = false)
        {
            _execute = execute;
            _canExecute = canExecute;
            _allowMultipleExcecutes = allowMultipleExcecutes;

            if (autoDetectCanExecuteChanged)
                CommandManager.RequerySuggested += (s, e) => RaiseCanExecuteChanged();
        }

        private Key _key;
        public Key Key
        {
            get => _key;
            set => this.Set(ref _key, value);
        }

        private ModifierKeys _modifiers;
        public ModifierKeys Modifiers
        {
            get => _modifiers;
            set => this.Set(ref _modifiers, value);
        }

        private string _tooltipText;
        public string TooltipText
        {
            get => _tooltipText;
            set => this.Set(ref _tooltipText, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => this.Set(ref _text, value);
        }

        public bool CanExecute(T parameter)
        {
            if (!_allowMultipleExcecutes && _commandIsExecuting)
            {
                _raiseCanExecuteChangedAfterExecute = true;
                return false;
            }

            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public void Execute(T parameter)
        {
            if (!CanExecute(parameter))
                return;

            if (!_allowMultipleExcecutes)
                _commandIsExecuting = true;

            _execute(parameter);

            if (!_allowMultipleExcecutes)
            {
                _commandIsExecuting = false;

                if (_raiseCanExecuteChangedAfterExecute)
                {
                    RaiseCanExecuteChanged();
                    _raiseCanExecuteChangedAfterExecute = false;
                }
            }
        }

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        bool ICommand.CanExecute(object parameter) => CanExecute((T)parameter);

        void ICommand.Execute(object parameter) => Execute((T)parameter);
    }
}