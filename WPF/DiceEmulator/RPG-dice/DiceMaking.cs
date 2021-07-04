using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPG_dice
{
    class DiceMaking
    {
        public string DiceGridName { get => "DiceGrid"; }

        public string BorderStyleName { get; set; } = "BorderStyle";

        private const int TotalDiceSpace = 7;

        public int DiceAmmout { get; set; } = 0;

        public GridLength DiceAmmoutGU { get => new GridLength(DiceAmmout, GridUnitType.Star); }

        public GridLength EmptySpaceGU { get => new GridLength(TotalDiceSpace - DiceAmmout, GridUnitType.Star); }

        public void MakeDice(MainWindow window, int number)
        {
            DiceAmmout++;

            //var diceGrid = window.GetType().GetMember(DiceGridName).GetType();

            var dg = window.FindName(DiceGridName) as Grid;

            dg.RowDefinitions.Add(new RowDefinition());

            //new Grid().RowDefinitions.Add(new RowDefinition());

            //var rowDefs = diceGrid.GetProperty("RowDefinitions").GetType();
            //rowDefs.GetMethod("Add").Invoke(rowDefs, new object[1] { new RowDefinition() });

            var border = new Border() { Style = window.FindResource(BorderStyleName) as Style };

            Grid.SetRow(border, DiceAmmout);

            dg.Children.Add(border);

            //var children = diceGrid.GetProperty("Children").GetType();
            //var childrenAddMethod = children.GetMethod("Add");

            //childrenAddMethod.Invoke(children, new object[1] { border });

            var imageSource = new BitmapImage(new Uri($"pack://application:,,,/RPG_Dice;component/Images/{number}die.png"));
            var image = new Image() { Source = imageSource };

            Grid.SetRow(image, DiceAmmout);

            dg.Children.Add(image);

            //childrenAddMethod.Invoke(children, new object[1] { image });

            var modifierGrid = new Grid();

            Grid.SetColumn(modifierGrid, 2);
            Grid.SetColumn(modifierGrid, DiceAmmout);

            modifierGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            modifierGrid.ColumnDefinitions.Add(new ColumnDefinition());

            var modifierButton = new Button() { Tag = number, Style = window.FindResource("InGridButStyle") as Style };
            var modifierButtonTBox = new TextBox() {  };

        }
    }
}
