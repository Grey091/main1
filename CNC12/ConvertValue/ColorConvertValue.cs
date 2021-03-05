using CNC12.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using static CNC12.ViewModels.VMmainWindow;

namespace CNC12.ConvertValue
{
    public class ColorConvertValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is Colorss.Red)
                {
                    return new SolidColorBrush(Color.FromRgb(243, 112, 63));
                }
                else if (value is Colorss.Green)
                {
                    return new SolidColorBrush(Color.FromRgb(100, 243, 106));
                }
                else if (value is Colorss.Yellow)
                {
                    return new SolidColorBrush(Color.FromRgb(249, 241, 69));
                }
            }
            return null;           
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }
}
