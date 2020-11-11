using System.Collections.Generic;
using System.Data.Common;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarWeekEventArgs
  {

    public CalendarWeekEventArgs(Week week)
    {
      this.Week = week;
    }

    public Week Week { get; set; }

  }
}
