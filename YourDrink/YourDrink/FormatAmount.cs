using System;
using Xamarin.Forms;

namespace YourDrink
{
    public class FormatAmount : IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return (float)value == 0 ? string.Empty : value;
        }




        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

