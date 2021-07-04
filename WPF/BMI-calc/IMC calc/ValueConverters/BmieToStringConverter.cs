using System;
using System.Globalization;

namespace IMC_calc
{
    /// <summary>
    /// A class that takes in a BMIE enum and outputs its description
    /// </summary>
    class BmieToStringConverter : BaseValueConverter<BmieToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((BMIE)value).GetDescription();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
