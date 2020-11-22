using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pabo.MonthCalendar.EventArgs
{
  public class SelectionChangedEventArgs<T> : RoutedEventArgs
  {
    public List<T> Selected { get; }

    public SelectionChangedEventArgs(RoutedEvent routedEvent, List<T> selected) : base(routedEvent)
    {
      this.Selected = selected;
    }

  }
}


