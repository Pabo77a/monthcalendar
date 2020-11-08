using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Pabo.MonthCalendar.Model
{
  internal class CalendarWeekday : Weekday, INotifyPropertyChanged
  {

    public CalendarWeekday()
    {
    }

    public CalendarWeekday(DayOfWeek day) : this()
    {
      Day = day;
    }

 
    public DayOfWeek Day { get; set; }

    public string Name
    {
      get
      {
        if (!string.IsNullOrEmpty(Text))
        {
          return Text;
        }
        else
        {
          CultureInfo ci = Thread.CurrentThread.CurrentCulture;
          return ci.DateTimeFormat.GetAbbreviatedDayName(Day);
        }
      }
    }

  }
}
