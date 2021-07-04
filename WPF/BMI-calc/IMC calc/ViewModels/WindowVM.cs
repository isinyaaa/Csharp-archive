using System.Windows.Controls;

namespace IMC_calc
{
    /// <summary>
    /// View Model for the <see cref="MainWindow"/> 
    /// </summary>
    class WindowVM : BaseViewModel
    {
        #region def values

        /// <value>Default 'weight' string to appear behind fields</value>
        public string DefWg { get; set; } = "Weight";

        /// <value>Default 'height' string to appear behind fields</value>
        public string DefHg { get; set; } = "Height";

        #endregion

        #region Imperial

        /// <value>User height in feet</value>
        public string UserHeightFeet { get; set; } = string.Empty;

        /// <value>User height in inches</value>
        public string UserHeightInches { get; set; } = string.Empty;

        /// <value>User weight in pounds</value>
        public string UserWeightLBS { get; set; } = string.Empty;

        #endregion

        #region Metric

        /// <value>User height in centimeters</value>
        public string UserHeightCm { get; set; } = string.Empty;

        /// <value>User weight in kilograms</value>
        public string UserWeightKG { get; set; } = string.Empty;

        #endregion

        #region BMI stuff

        /// <value>Stores the BMI value</value>
        public float BMIValue { get; set; } = new float();

        /// <value>Stores the BMI value as a formatted string</value>
        public string BMIString { get => BMIValue.ToString("G"); }

        /// <value>Tells whether the BMI value is available</value>
        public bool BMIAvailable { get => !BMIValue.Equals(new float()); }

        /// <value>Stores the Enum that describes the user BMI</value>
        public BMIE BMIInfo { get => Logic.BMIConvert(BMIValue); }

        #endregion

        #region radio buttons checks

        /// <value>Tells whether the metric <see cref="RadioButton"/> is checked</value>
        public bool? MetricChecked { get; set; } = true;

        /// <value>Tells whether the imperial <see cref="RadioButton"/> is checked</value>
        public bool? ImperialChecked { get; set; } = false;

        #endregion

        #region commands

        /// <value>The command for the Clear Button</value>
        public RelayCommand ClearCommand { get; set; }

        /// <value>The command for the Calculate Button</value>
        public RelayCommand CalculateCommand { get; set; }

        #endregion

        #region ctor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="textBoxes">All textboxes</param>
        /// <param name="radioButtons">All radiobuttons</param>
        public WindowVM(TextBox[] textBoxes, RadioButton[] radioButtons)
        {
            // Sets the clear command
            ClearCommand = new RelayCommand(() =>
            {
                // Checks what radiobutton is checked and clears the fiels related to it
                if ((bool)ImperialChecked)
                    UserHeightFeet = UserHeightInches = UserWeightLBS = string.Empty;
                else
                    UserHeightCm = UserWeightKG = string.Empty;

                // Resets the BMI value
                BMIValue = new float();

                // Updates properties
                UpdateBMIStuff();
                UpdateFields();
            });

            // Sets the calculate command
            CalculateCommand = new RelayCommand(() =>
            {
                // Calculates the BMI based on the radiobutton that's checked
                // and checkes whether that alternative has valid inputs
                if ((bool)ImperialChecked && ImperialInputsValid())
                    BMIValue = ImperialCalc();

                else if ((bool)MetricChecked && MetricInputsValid())
                    BMIValue = MetricCalc();
                // Else we can reset the BMI value
                else
                    BMIValue = new float();

                // Updates the BMI related properties
                UpdateBMIStuff();
            });

            // Sets up a caller inthe textchanged event in all textboxes so that we can check their inputs in real time
            foreach (var tBox in textBoxes)
            {
                tBox.TextChanged += (sender, e) =>
                {
                    var selectionStart = tBox.SelectionStart;

                    // Checkes whether the input is valid
                    tBox.Text = Logic.CorrectInput(((TextBox)sender).Text);

                    // And resets the selection start
                    tBox.SelectionStart = selectionStart;
                };
            }

            // Sets up a caller in the checked event in all radiobuttons so that we can update the visibility of the UI elements
            foreach (var rBut in radioButtons)
            {
                rBut.Checked += (sender, e) =>
                {
                    OnPropertyChanged(nameof(MetricChecked));
                    OnPropertyChanged(nameof(ImperialChecked));
                };
            }
        }

        #endregion

        #region private helpers

        #region update methods

        /// <summary>
        /// Updates every text input field property
        /// </summary>
        private void UpdateFields()
        {
            OnPropertyChanged(nameof(UserHeightFeet));
            OnPropertyChanged(nameof(UserHeightInches));
            OnPropertyChanged(nameof(UserWeightLBS));
            OnPropertyChanged(nameof(UserHeightCm));
            OnPropertyChanged(nameof(UserWeightKG));
        }

        /// <summary>
        /// Updates the BMI related properties
        /// </summary>
        private void UpdateBMIStuff()
        {
            OnPropertyChanged(nameof(BMIAvailable));
            OnPropertyChanged(nameof(BMIValue));
            OnPropertyChanged(nameof(BMIString));
            OnPropertyChanged(nameof(BMIInfo));
        }

        #endregion

        #region validity methods

        /// <summary>
        /// Checks whether the metric input properties are valid
        /// </summary>
        /// <returns></returns>
        private bool MetricInputsValid()
        {
            if (!UserHeightCm.CheckInput() || 
                !UserWeightKG.CheckInput())
                return false;
            
            return true;
        }

        /// <summary>
        /// Checks whether the imperial input properties are valid
        /// </summary>
        /// <returns></returns>
        private bool ImperialInputsValid()
        {
            if (!UserHeightFeet.CheckInput() ||
                !UserHeightInches.CheckInput() ||
                !UserWeightLBS.CheckInput())
                return false;
            
            return true;
        }

        #endregion

        #region calc methods

        /// <summary>
        /// Gets the BMI value from the imperial units menu
        /// </summary>
        /// <returns></returns>
        private float ImperialCalc()
        {
            var meterHeight = float.Parse(UserHeightFeet) * 0.3048f + float.Parse(UserHeightInches) * 0.0254f;
            var kgWeight = float.Parse(UserWeightLBS) * 0.453592f;

            return kgWeight / (meterHeight * meterHeight);
        }

        /// <summary>
        /// Gets the BMI value from the metric units menu
        /// </summary>
        /// <returns></returns>
        private float MetricCalc()
        {
            var meterHeight = float.Parse(UserHeightCm) / 100;
            var kgWeight = float.Parse(UserWeightKG);

            return kgWeight / (meterHeight * meterHeight);
        }

        #endregion

        #endregion
    }
}
