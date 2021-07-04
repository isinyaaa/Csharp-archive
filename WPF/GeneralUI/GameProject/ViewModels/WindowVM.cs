using GameProject.DM;
using System.Windows;
using System.Windows.Input;

namespace GameProject.VM
{
    public class WindowVM : BaseViewModel
    {
        #region private members

        ///<value>App Window</value>
        private Window mWindow { get; set; }

        ///<value>Outer Margin Size</value>
        private int mOuterMarginSize { get; set; } = 10;

        ///<value>Window Radius Size</value>
        private int mWindowRadius { get; set; } = 10;

        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        #endregion

        #region public properties

        public AppPage CurrentPage { get; set; } = AppPage.Login;

        public double WindowMinimumWidth { get; set; } = 400;

        public double WindowMinimumHeight { get; set; } = 400;

        public Thickness InnerContentsPadding { get; set; } = new Thickness(0);

        public bool Borderless { get => (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked); }

        ///<value>Resize Border Size</value>
        public int ResizeBorder { get => Borderless ? 0 : 6; set => ResizeBorder = value; }

        ///<value>Resize Border Size Thickness</value>
        public Thickness ResizeBorderThickness { get => new Thickness(ResizeBorder + OuterMarginSize); }

        ///<value>Outer Margin Size</value>
        public int OuterMarginSize { get => Borderless ? 0 : mOuterMarginSize; set => mOuterMarginSize = value; }

        ///<value>Outer Margin Size Thickness</value>
        public Thickness OuterMarginSizeThickness { get => new Thickness(OuterMarginSize); }

        ///<value>Window Corner Radius Size</value>
        public int WindowRadius { get => Borderless ? 0 : mWindowRadius; set => mWindowRadius = value; }

        ///<value>Window Corner Radius Size CR</value>
        public CornerRadius WindowsCornerRadius { get => new CornerRadius(WindowRadius); }

        ///<value>Title Height</value>
        public int TitleHeight { get; set; } = 38;

        ///<value>Title Height GL</value>
        public GridLength TitleHeightGridLength { get => new GridLength(TitleHeight + ResizeBorder); }

        #endregion

        #region commands

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowVM(Window window)
        {
            // Gets a local copy of the App Screen
            mWindow = window;

            //Sets the Method for the Window Size Changed
            mWindow.StateChanged += (sender, e) =>
            {
                WindowResized();
            };

            //Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(mWindow);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };

        }

        #region private helpers

        private void WindowResized()
        {
            //Calls those properties on the Windows Size Changed
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowsCornerRadius));
        }

        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindow);

            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion
    }
}
