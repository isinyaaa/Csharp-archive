using System;
using System.Windows.Input;

namespace TicTacToeGame
{
    class RelayCommand : ICommand
    {
        private Action mAction;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter) => true;

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}