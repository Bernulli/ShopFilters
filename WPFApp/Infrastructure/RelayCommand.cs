using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFApp.Infrastructure
{
    public class RelayCommand : ICommand
    {
        //readonly Action<object> _execute;
        //readonly Predicate<object> _canExecute;

        //public RelayCommand(Action<object> execute)
        //    : this(execute, null)
        //{
        //}

        //public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        //{
        //    if (execute == null)
        //    {
        //        throw new ArgumentNullException("execute");
        //    }

        //    _execute = execute;
        //    _canExecute = canExecute;
        //}

        //public bool CanExecute(object parameter)
        //{
        //    //return _canExecute == null ? true : _canExecute.Invoke(parameter);
        //    return this._canExecute == null || this._canExecute(parameter);
        //}

        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        //public void Execute(object parameter)
        //{
        //    _execute.Invoke(parameter);
        //}

        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
