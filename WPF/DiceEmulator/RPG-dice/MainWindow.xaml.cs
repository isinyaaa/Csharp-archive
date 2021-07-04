using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RPG_dice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dice = new List<int>() { 4, 6, 8, 12 };

            foreach (var number in dice)
            {
                DiceMaking.MakeDice(this, number);
            }
        }

        public void example(int number)
        {
            
        }
    }
}
