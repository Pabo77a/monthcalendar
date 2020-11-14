using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pabo.MonthCalendar.EventArgs
{
  public class SelectionChangedEventArgs : RoutedEventArgs
  {
    public List<Day> Selected { get; }

    public SelectionChangedEventArgs(RoutedEvent routedEvent, List<Day> selected) : base(routedEvent)
    {
      this.Selected = selected;
    }

  }
}


