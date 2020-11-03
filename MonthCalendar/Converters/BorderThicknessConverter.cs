using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.Converters
{

  [ValueConversion(typeof(Thickness), typeof(Thickness))]
  internal class BorderThicknessConverter : IValueConverter
  {
    public enum Parameters
    {
      Normal, Inverted
    }

    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
      var thickness = (Thickness)value;
      var day = parameter as CalendarDay;

      return new Thickness(0.0, thickness.Top, 0.0, thickness.Bottom);
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}
