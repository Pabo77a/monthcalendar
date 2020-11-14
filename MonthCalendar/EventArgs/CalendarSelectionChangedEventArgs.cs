using System.Collections.Generic;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarSelectionChangedEventArgs
  {
    
    public CalendarSelectionChangedEventArgs(List<Day> selected)
    {
      this.Selected = selected;
    }

    public List<Day> Selected { get; set; }
  }
}
