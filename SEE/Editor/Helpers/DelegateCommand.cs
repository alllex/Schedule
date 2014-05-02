using System;
using System.Windows.Input;

namespace Editor.Helpers
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _commandp;
        private readonly Action _command;
        private readonly bool _hasParam;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<object> command, Func<bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute = canExecute;
            _commandp = command;
            _hasParam = true;
        }

        public DelegateCommand(Action command, Func<bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute = canExecute;
            _command = command;
            _hasParam = false;
        }

        public void Execute(object parameter)
        {
            if (_hasParam)
            {
                _commandp(parameter);
            }
            else
            {
                _command();
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

    }
}