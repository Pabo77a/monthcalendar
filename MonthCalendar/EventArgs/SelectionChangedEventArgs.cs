using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation.Peers;

namespace Pabo.MonthCalendar.EventArgs
{
  public class SelectionChangedEventArgs : RoutedEventArgs
  {
    public List<DayItem> Current { get; }

    public List<DayItem> Previous { get; }

    public SelectionChangedEventArgs(RoutedEvent routedEvent, List<DayItem> current, List<DayItem> previous) : base(routedEvent)
    {
      this.Current = current;
      this.Previous = previous;
    }

  }
}


