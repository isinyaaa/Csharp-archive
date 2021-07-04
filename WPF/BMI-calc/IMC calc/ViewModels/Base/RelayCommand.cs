using System;
using System.Windows.Input;

namespace IMC_calc
{
    /// <summary>
    /// Base relay command
    /// </summary>
    internal class RelayCommand : ICommand
    {
        /// <value>Holds the method to be executed</value>
        private Action mAction { get; set; }

        /// <value>dont know</value>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="mAction">Anonymous function, must return nothing</param>
        public RelayCommand(Action action) => mAction = action;

        /// <summary>
        /// Executes the relay command stored action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => mAction();

        /// <summary>
        /// Tells whether can execute of not
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;
    }
}
