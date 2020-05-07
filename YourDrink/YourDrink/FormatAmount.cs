using System;
using Xamarin.Forms;

namespace YourDrink
{
    public class FormatAmount : IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
           // return (float)value == 0 ? string.Empty : value;
        }




        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
            //return (string)value != "" ? System.Convert.ToDecimal(value) : value;
        }

    }
}

