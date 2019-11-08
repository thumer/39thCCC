using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContestProjectWPF.Utils
{
    /// <summary>
    /// Das AsyncDelegateCommand ist eine Standardimplementierung eines Commands, das ein Execute- und ein CanExecute-Delegate für die Ausführung kapselt. 
    /// </summary>
    public class AsyncDelegateCommand : AsyncDelegateCommand<object>
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="asyncExecute">AsyncExecute-Delegate</param>
        /// <param name="canExecute">CanExecute-Delegate</param>
        /// <param name="allowRunMultipleTasks">Soll der Command ausgeführt werden können, wenn der Task noch nicht abgeschlossen wurde.</param>
        public AsyncDelegateCommand(Func<Task> asyncExecute,
                       Func<bool> canExecute = null, bool allowRunMultipleTasks = false)
            : base(_ => asyncExecute(), canExecute != null ? _ => canExecute() : (Func<object, bool>)null, allowRunMultipleTasks)
        {
        }

        #endregion
    }

    /// <summary>
    /// Das generische AsyncDelegateCommand ist eine Standardimplementierung eines Commands, das ein Execute- und ein CanExecute-Delegate für die Ausführung kapselt. 
    /// </summary>
    /// <typeparam name="T">Typ der Command-Argumente. Kann entweder ein CommandParameter-Typ bei der Standard-WPF-Commandbindung oder EventCommandArgs bei dem Event-Trigger-Commanding sein.</typeparam>
    public class AsyncDelegateCommand<T> : ObservableObject, ICommand
    {
        #region Membervariablen

        protected readonly Func<T, Task> _asyncExecute;
        protected readonly Func<T, bool> _canExecute;

        private readonly bool _allowRunMultipleTasks = false;
        private HashSet<Task> _activeTasks = new HashSet<Task>();
        private bool _raiseCanExecuteChangedAfterExecute = false;

        #endregion

        #region Events

        /// <summary>
        /// Gibt Bescheid, dass sich die CanExecute Eigenschaft geändert hat.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gibt Bescheid, dass sich die 
        /// </summary>
        public event EventHandler IsRunningChanged;

        #endregion

        #region Kosntruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="asyncExecute">AsyncExecute-Delegate</param>
        /// <param name="canExecute">CanExecute-Delegate</param>
        /// <param name="allowRunMultipleTasks">Soll der Command ausgeführt werden können, wenn der Task noch nicht abgeschlossen wurde.</param>
        public AsyncDelegateCommand(Func<T, Task> asyncExecute,
                       Func<T, bool> canExecute = null, bool allowRunMultipleTasks = false)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
            _allowRunMultipleTasks = allowRunMultipleTasks;

            CommandManager.RequerySuggested += CommandManager_RequerySuggested;
        }

        #endregion

        #region ICommand<T> Members

        public bool CanExecute(T parameter)
        {
            if (!_allowRunMultipleTasks && IsRunning)
            {
                _raiseCanExecuteChangedAfterExecute = true;
                return false;
            }

            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public async void Execute(T parameter)
        {
            await ExecuteAsync(parameter);
        }

        public bool IsRunning
        {
            get
            {
                return _activeTasks.Where(t => t.Status < TaskStatus.RanToCompletion).Any();
            }
        }

        public virtual async Task ExecuteAsync(T parameter)
        {
            if (!_allowRunMultipleTasks)
                _activeTasks.Clear();

            Task task = _asyncExecute(parameter);
            _activeTasks.Add(task);
            OnTaskStateChanged();
            await task;
            OnTaskStateChanged();

            if (_raiseCanExecuteChangedAfterExecute)
            {
                RaiseCanExecuteChanged();
                _raiseCanExecuteChangedAfterExecute = false;
            }
        }

        private void OnTaskStateChanged()
        {
            RaiseCanExecuteChanged();
            RaisePropertyChanged(() => IsRunning);
            RaiseIsRunningChanged();
        }

        #endregion

        #region Properties

        private Key _key;
        public Key Key
        {
            get { return _key; }
            set
            {
                _key = value;
                RaisePropertyChanged(() => Key);
            }
        }

        private ModifierKeys _modifiers;
        public ModifierKeys Modifiers
        {
            get { return _modifiers; }
            set
            {
                _modifiers = value;
                RaisePropertyChanged(() => Modifiers);
            }
        }

        private string _tooltipText;
        public string TooltipText
        {
            get { return _tooltipText; }
            set
            {
                _tooltipText = value;
                RaisePropertyChanged(() => TooltipText);
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        #endregion

        #region IDelegateCommand Members

        /// <summary>
        /// Wirft das CanExecuteChanged-Event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Wirft das IsRunningChanged-Event.
        /// </summary>
        private void RaiseIsRunningChanged()
        {
            if (IsRunningChanged != null)
            {
                IsRunningChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// <see cref="ICommand"/>
        /// </summary>
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        /// <summary>
        /// <see cref="ICommand"/>
        /// </summary>
        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// Damit der Aufruf von "CommandManager.InvalidateRequerySuggested();" auch hier von
        /// DelegateCommand (ICommand) abgeleiteten/erstellen Commands betrifft - muss der CommandManager davon wissen!
        /// Siehe auch: http://joshsmithonwpf.wordpress.com/2008/06/17/allowing-commandmanager-to-query-your-icommand-objects/
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void CommandManager_RequerySuggested(object sender, EventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        #endregion
    }
}