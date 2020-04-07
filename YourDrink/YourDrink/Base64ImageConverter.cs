using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace YourDrink
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var base64 = (string)value;

            if (base64 == null)
                return null;
          
            return ImageSource.FromStream(() =>
            {
                return new MemoryStream(System.Convert.FromBase64String(base64));
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

