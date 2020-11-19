using Pabo.MonthCalendar.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Pabo.MonthCalendar.Converters
{

  [ValueConversion(typeof(bool), typeof(Visibility))]
  internal class VisualModeToVisibilityConverter : IValueConverter
  {
    public enum Parameters
    {
      Normal, Inverted
    }

    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
      var current = (VisualMode)Enum.Parse(typeof(VisualMode), value.ToString());
      var mode = (VisualMode)Enum.Parse(typeof(VisualMode), parameter.ToString());
    
      return current == mode ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}