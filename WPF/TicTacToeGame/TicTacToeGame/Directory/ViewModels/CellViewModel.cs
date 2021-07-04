using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace TicTacToeGame
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region property declarations

        public MarkType Type { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public ICommand ClickCommand { get; set; }

        public string Content { get; set; }

        public Brush Background { get; set; }

        public Brush Foreground { get; set; }

        #endregion

        #region ctor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="type"></param>
        public CellViewModel(int row, int col, MarkType type)
        {
            Row = row;

            Column = col;

            Type = type;

            ClickCommand = new RelayCommand(Click);
        }

        #endregion

        #region class methods

        /// <summary>
        /// Default Button Click Command
        /// </summary>
        public void Click()
        {
            if (GameLogic.GameEnded)
            {
                GameLogic.StartNewGame();
                return;
            }

            //Checks whether it is free or not
            if (Type != MarkType.Free)
                return;

            FillCell();
            
            //Toggles player turn status
            GameLogic.PlayerTurn = !GameLogic.PlayerTurn;

            //Checks for winner
            GameLogic.CheckForWinner();
        }

        /// <summary>
        /// Properly fills the Cell Content
        /// </summary>
        private void FillCell()
        {
            var curCell = GameLogic.GameBoard[Row * 3 - 1 + Column];

            curCell.Type = GameLogic.PlayerTurn ? MarkType.Cross : MarkType.Nought;

            curCell.Foreground = GameLogic.PlayerTurn ? Brushes.Black : Brushes.Red;
        }

        #endregion
    }
}
