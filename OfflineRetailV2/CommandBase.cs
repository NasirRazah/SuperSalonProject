// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System;
using System.Windows.Input;

namespace OfflineRetailV2
{
    public class CommandBase : ICommand
    {
        public CommandBase(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            if (executeMethod != null)
            {
                ExecuteMethod = executeMethod;
            }

            CanExecuteMethod = canExecuteMethod;
        }

        public CommandBase(Action<object> executeMethod)
        {
            if (executeMethod != null)
            {
                ExecuteMethod = executeMethod;
            }

            CanExecuteMethod = null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Predicate<object> CanExecuteMethod { get; private set; }
        public Action<object> ExecuteMethod { get; private set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod == null || CanExecuteMethod.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteMethod.Invoke(parameter);
        }
    }
}