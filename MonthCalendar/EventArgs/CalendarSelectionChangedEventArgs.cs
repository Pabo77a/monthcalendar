using System.Collections.Generic;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarSelectionChangedEventArgs
  {

    public CalendarSelectionChangedEventArgs(List<DayItem> current, List<DayItem> prev)
    {
      this.PreviousSelection = prev;
      this.CurrentSelection = current;
    }

    public List<DayItem> CurrentSelection { get; set; }
    public List<DayItem> PreviousSelection { get; set; }
  }
}
