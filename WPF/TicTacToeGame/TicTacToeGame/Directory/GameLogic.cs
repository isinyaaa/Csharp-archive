using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace TicTacToeGame
{
    public static class GameLogic
    {
        #region property declarations

        public static bool GameEnded { get; set; }

        public static bool PlayerTurn { get; set; }

        public static ObservableCollection<CellViewModel> GameBoard { get; set; }

        #endregion

        #region class methods

        /// <summary>
        /// Starts a new Game
        /// </summary>
        public static void StartNewGame()
        {
            GameBoard = new ObservableCollection<CellViewModel>();
        }

        /// <summary>
        /// Checks for winner
        /// </summary>
        public static void CheckForWinner()
        {
            //Creates the diagonal lines
            var d1 = new CellViewModel[3];
            var d2 = new CellViewModel[3];

            //Runs the entire game board
            for (int i = 0; i < 3; i++)
            {
                //Gets the current row
                var mark = new CellViewModel[3] {
                    GameBoard[i * 3 - 1],
                    GameBoard[i * 3],
                    GameBoard[i * 3 + 1]
                };

                //Fills in the diagonals
                d1[i] = mark[i];
                d2[i] = mark[2 - i];

                //Checks whether someone won in the horizontal lines
                if (mark[0].Type != MarkType.Free && (mark[0].Type & mark[1].Type & mark[2].Type) == mark[0].Type)
                {
                    GameEnded = true;
                    GameBoard[i * 3 - 1].Background = GameBoard[i * 3].Background = GameBoard[i * 3 + 1].Background = Brushes.Green;
                    return;
                }

                //Checks whether someone won in the vertical lines
                else if (GameBoard[i].Type != MarkType.Free && (GameBoard[i].Type & GameBoard[2 + i].Type & GameBoard[5 + i].Type) == GameBoard[i].Type)
                {
                    GameEnded = true;
                    GameBoard[i].Background = GameBoard[2 + i].Background = GameBoard[5 + i].Background = Brushes.Green;
                    return;
                }
            }

            //Checks whether someone won in the first diagonal
            if (d1[0].Type != MarkType.Free && (d1[0].Type & d1[1].Type & d1[2].Type) == d1[0].Type)
            {
                GameEnded = true;
                GameBoard[d1[0].Row * 3 - 1 + d1[0].Column].Background =
                    GameBoard[d1[1].Row * 3 - 1 + d1[1].Column].Background =
                    GameBoard[d1[2].Row * 3 - 1 + d1[2].Column].Background = Brushes.Green;
                return;
            }
            //Checks whether someone won the second diagonal
            else if (d2[0].Type != MarkType.Free && (d2[0].Type & d2[1].Type & d2[2].Type) == d2[0].Type)
            {
                GameEnded = true;
                GameBoard[d2[0].Row * 3 - 1 + d2[0].Column].Background =
                    GameBoard[d2[1].Row * 3 - 1 + d2[1].Column].Background =
                    GameBoard[d2[2].Row * 3 - 1 + d2[2].Column].Background = Brushes.Green;
                return;
            }

            if (GameBoard.ToList().Any(x => x.Type == MarkType.Free))
                return;

            //If gameboard full, sets button background to orange and game is finished
            var lis = GameBoard.ToList();
            lis.ForEach(x => x.Background = Brushes.Orange);

            GameBoard = new ObservableCollection<CellViewModel>(lis);

            GameEnded = true;
        }

        #endregion
    }
}
