using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Converters
{

  [ValueConversion(typeof(DateTime), typeof(string))]
  internal class DateTimeToDayConverter : IValueConverter
  {

    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
      var date= (DateTime)value;

      return date.Day.ToString();
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}
