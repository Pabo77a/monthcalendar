using System.Collections.Generic;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar.EventArgs
{
  internal class CalendarSelectionChangedEventArgs<T>
  {
    
    public CalendarSelectionChangedEventArgs(List<T> selected)
    {
      this.Selected = selected;
    }

    public List<T> Selected { get; set; }
  }
}
