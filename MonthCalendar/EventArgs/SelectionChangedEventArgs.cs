using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pabo.MonthCalendar.EventArgs
{
  public class SelectionChangedEventArgs : RoutedEventArgs
  {
    public List<Day> Current { get; }

    public List<Day> Previous { get; }

    public SelectionChangedEventArgs(RoutedEvent routedEvent, List<Day> current, List<Day> previous) : base(routedEvent)
    {
      this.Current = current;
      this.Previous = previous;
    }

  }
}


