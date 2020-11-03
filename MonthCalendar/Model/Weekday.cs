using System;
using System.Globalization;
using System.Threading;

namespace Pabo.MonthCalendar.Model
{
  internal class Weekday
  {

    public Weekday()
    {
    }

    public Weekday(DayOfWeek day) : this()
    {
      Day = day;
    }

 
    public DayOfWeek Day { get; set; }

    public string Name
    {
      get
      {
        CultureInfo ci = Thread.CurrentThread.CurrentCulture;
        return ci.DateTimeFormat.GetAbbreviatedDayName(Day);
      }
    }

  }
}
