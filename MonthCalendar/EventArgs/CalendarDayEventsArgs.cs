using System.Collections.Generic;
using System.Data.Common;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarDayEventArgs
  {
   
    public CalendarDayEventArgs(Day day)
    {
      this.Day = day;
    }

    public Day Day { get; set; }
    
  }
}
