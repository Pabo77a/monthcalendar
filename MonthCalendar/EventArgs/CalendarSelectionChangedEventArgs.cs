using System.Collections.Generic;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarSelectionChangedEventArgs
  {
    
    public CalendarSelectionChangedEventArgs(List<Day> current, List<Day> prev)
    {
      this.PreviousSelection = prev;
      this.CurrentSelection = current;
    }

    public List<Day> CurrentSelection { get; set; }
    public List<Day> PreviousSelection { get; set; }
  }
}
