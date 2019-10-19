using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using System.Windows.Input;
namespace CoreKit.XF.Infrastructure
{
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<object, Task> execute)
            : base(param => execute(param).ConfigureAwait(false))
        {
        }

        public AsyncCommand(Func<Task> execute)
            : base(() => execute().ConfigureAwait(false))
        {
        }

        public AsyncCommand(Action<object> execute, Func<object, bool> canExecute)
            : base(execute, canExecute)
        {
        }

        public AsyncCommand(Action execute, Func<bool> canExecute)
            : base(execute, canExecute)
        {
        }
    }


    public interface ICommand<T>
    {

        event EventHandler CanExecuteChanged;

        bool CanExecute(T parameter);

        void Execute(T parameter);
    }


    public class Command<T> : ICommand<T>
    {

        public event EventHandler CanExecuteChanged;
        readonly Func<T, bool> _canExecute;
        readonly Action<T> _execute;
        private volatile bool _isRunning;

        public Command(Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
        }

        public Command(Action execute) : this(o => execute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        public Command(Action<T> execute, Func<T, bool> canExecute) : this(execute)
        {
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _canExecute = canExecute;
        }

        public Command(Action execute, Func<bool> canExecute) : this(o => execute(), o => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(T parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);

            return true;

            //return this.canExecute == null || this.canExecute.Invoke();
        }

        public void Execute(T parameter)
        {
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;

            if (CanExecute(parameter))
            {
                _execute(parameter);
            }

            _isRunning = false;
        }

        public void RaiseCanExecuteChanged()
        {

            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }


    public sealed class AsyncCommand<T> : Command<T>
    {

        public AsyncCommand(Func<T, Task> execute)
            : base(param => execute(param).ConfigureAwait(false))
        {
        }

        public AsyncCommand(Func<Task> execute)
            : base(() => execute().ConfigureAwait(false))
        {
        }

        public AsyncCommand(Action<T> execute, Func<T, bool> canExecute)
            : base(execute, canExecute)
        {
        }

        public AsyncCommand(Action execute, Func<bool> canExecute)
            : base(execute, canExecute)
        {
        }

    }

}
