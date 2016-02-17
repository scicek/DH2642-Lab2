namespace Dinnerplanner.Models
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private Predicate<object> _canExecutePredicate;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> action, Predicate<object> canExecute = null)
        {
            ExecuteAction = action;
            CanExecutePredicate = canExecute;
        }

        public Action<object> ExecuteAction { get; set; }

        public Predicate<object> CanExecutePredicate
        {
            get
            {
                return _canExecutePredicate; 
                
            }

            set
            {
                _canExecutePredicate = value; 
                CanExecuteChanged.Raise(this);
            }
        }

        public bool CanExecute(object parameter = null)
        {
            if (CanExecutePredicate == null)
            {
                return true;
            }

            return CanExecutePredicate(parameter);
        }

        public void Execute(object parameter)
        {
            if (ExecuteAction != null && CanExecute())
            {
                ExecuteAction(parameter);
            }
        }
    }
}
