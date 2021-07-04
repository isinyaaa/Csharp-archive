using System.Windows;
using System.Windows.Controls;

namespace TicTacToeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GameLogic.StartNewGame();

            //MainGrid.DataContext = GameLogic.GameBoard;

            InitializeContainer();
            
        }

        private void InitializeContainer()
        {
            var ColDefs = MainGrid.ColumnDefinitions;
            var RowDefs = MainGrid.RowDefinitions;

            for (int i = 0; i < 3; i++)
            {
                RowDefs.Add(new RowDefinition());
                ColDefs.Add(new ColumnDefinition());

                for (int j = 0; j < 3; j++)
                {
                    var curCell = new CellViewModel(i, j, MarkType.Free);

                    var curBut = new Button()
                    {
                        DataContext = curCell,

                    };

                    //var curBut = (Button)FindName($"But{i}{j}");

                    //curBut.GetType().GetProperty("DataContext").SetValue(curBut, curCell);

                    GameLogic.GameBoard.Add(curCell);

                    Grid.SetRow(curBut, i);
                    Grid.SetColumn(curBut, j);

                    MainGrid.Children.Add(curBut);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}
