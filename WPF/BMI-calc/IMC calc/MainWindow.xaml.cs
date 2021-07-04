using System.Windows;
using System.Windows.Controls;

namespace IMC_calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Runs after the window loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new WindowVM(new TextBox[5] { FeetTextBox, InchTextBox, PoundTextBox, CmTextBox, KgTextBox }, new RadioButton[2] { ImperialCheck, MetricCheck });
        }
    }
}