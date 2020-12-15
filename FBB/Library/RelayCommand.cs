using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FBB.Library
{
    public class RelayCommand<T> : ICommand
    {

        private Action<T> _execute = null;
        private Predicate<T> _canExecute = null;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }


        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute is null)
            {
                throw new ArgumentException("execute is null");
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            //if (!(_canExecute is null))
            //{
            //    return _canExecute((T)parameter);
            //}
            //else
            //{
            //    return _canExecute is null;
            //}

            return _canExecute is null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
