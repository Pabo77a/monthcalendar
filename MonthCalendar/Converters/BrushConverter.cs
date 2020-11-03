using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Converters
{

  [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
  internal class ColorToSolidColorBrushConverter : IValueConverter
  {
    
    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
      var color = (Color)value;

      return new SolidColorBrush(color);
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}
