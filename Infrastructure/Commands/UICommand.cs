using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Commands
{
    /// <summary>
    /// Реализует <see cref="ICommand"/>. 
    /// Является сущностью для комманд от UI
    /// </summary>
    public class UICommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public UICommand(Action<object> _execute, Func<object, bool> _canExecute = null)
        {
            this._execute = _execute;
            this._canExecute = _canExecute;
        }

        //-->region Реализация ICommand
        #region Реализация ICommand
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Определение возможности выполнения команды
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Возможность выполнения</returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        /// <summary>
        /// Выполняет комманду
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
        #endregion
    }
}
