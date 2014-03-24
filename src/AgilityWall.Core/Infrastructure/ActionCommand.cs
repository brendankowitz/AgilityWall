using System;
using System.Windows.Input;

namespace AgilityWall.Core.Infrastructure
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canExecute;

        public ActionCommand()
        {
            _action = _action ?? (obj => { });
            _canExecute = _canExecute ?? (obj => true);
        }

        public ActionCommand(Action<object> action)
            : this()
        {
            _action = action;
        }

        public ActionCommand(Action<object> action, Predicate<object> condition)
            : this(action)
        {
            _canExecute = condition;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
