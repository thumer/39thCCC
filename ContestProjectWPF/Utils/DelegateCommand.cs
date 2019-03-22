using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

//------------------------------------------------------------------------
//	RZL KIS                                                    (c)2011 RZL
//
//
//	MS-Windows										    (w) Thomas Humer
//------------------------------------------------------------------------
//  DelegateCommand.cs
//------------------------------------------------------------------------
namespace ContestProject.Utils
{
    /// <summary>
    /// Das DelegateCommand ist eine Standardimplementierung eines Commands, das ein Execute- und ein CanExecute-Delegate für die Ausführung kapselt. 
    /// </summary>
    public class DelegateCommand : DelegateCommand<object>
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="execute">Execute-Delegate</param>
        /// <param name="canExecute">CanExecute-Delegate</param>
        /// <param name="autoDetectCanExecuteChanged">Automatisches ausführen das CanExecute-Delegates getriggert durch den CommandManager.</param>
        /// <param name="allowMultipleExcecutes">Liefert während der Ausführung des Commands bei CanExecute true, sodass mehrere Commands gleichzeitig gestartet werden können</param>
        public DelegateCommand(Action execute, Func<bool> canExecute = null, bool autoDetectCanExecuteChanged = true, bool allowMultipleExcecutes = false)
            : base(_ => execute(), canExecute != null ? _ => canExecute() : (Predicate<object>)null, autoDetectCanExecuteChanged, allowMultipleExcecutes)
        {
        }

        #endregion
    }

    /// <summary>
    /// Das generische DelegateCommand ist eine Standardimplementierung eines Commands, das ein Execute- und ein CanExecute-Delegate für die Ausführung kapselt. 
    /// </summary>
    /// <typeparam name="T">Typ der Command-Argumente. Kann entweder ein CommandParameter-Typ bei der Standard-WPF-Commandbindung oder EventCommandArgs bei dem Event-Trigger-Commanding sein.</typeparam>
    public class DelegateCommand<T> : ObservableObject, ICommand
    {
        #region Members

        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        private readonly bool _allowMultipleExcecutes;

        private bool _commandIsExecuting = false;
        private bool _raiseCanExecuteChangedAfterExecute = false;

        #endregion

        #region Events

        /// <summary>
        /// Gibt Bescheid, dass sich die CanExecute Eigenschaft geändert hat.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="execute">Execute-Delegate</param>
        /// <param name="canExecute">CanExecute-Delegate</param>
        /// <param name="autoDetectCanExecuteChanged">Automatisches ausführen das CanExecute-Delegates getriggert durch den CommandManager.</param>
        /// <param name="allowMultipleExcecutes">Liefert während der Ausführung des Commands bei CanExecute true, sodass mehrere Commands gleichzeitig gestartet werden können</param>
        public DelegateCommand(Action<T> execute,
                       Predicate<T> canExecute = null, bool autoDetectCanExecuteChanged = true, bool allowMultipleExcecutes = false)
        {
            _execute = execute;
            _canExecute = canExecute;
            _allowMultipleExcecutes = allowMultipleExcecutes;

            if (autoDetectCanExecuteChanged)
            {
                CommandManager.RequerySuggested += CommandManager_RequerySuggested;
            }
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

        #region ICommand<T> Members

        /// <summary>
        /// <see cref="ICommand"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ICommand"/>
        /// </summary>
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
    }
}