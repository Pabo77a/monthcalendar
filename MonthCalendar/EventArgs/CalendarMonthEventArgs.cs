using System.Collections.Generic;
using System.Data.Common;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarMonthEventArgs
  {

    public CalendarMonthEventArgs(Month month)
    {
      this.Month = month;
    }

    public Month Month { get; set; }

  }
}