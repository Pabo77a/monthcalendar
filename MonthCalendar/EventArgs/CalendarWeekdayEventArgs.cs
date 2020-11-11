using System.Collections.Generic;
using System.Data.Common;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarWeekdayEventArgs
  {

    public CalendarWeekdayEventArgs(Weekday weekday)
    {
      this.Weekday = weekday;
    }

    public Weekday Weekday { get; set; }

  }
}