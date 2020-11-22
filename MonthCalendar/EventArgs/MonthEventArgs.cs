using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pabo.MonthCalendar.EventArgs
{
  public class MonthEventArgs : RoutedEventArgs
  {
    public Month Month { get; }

    public MonthEventArgs(RoutedEvent routedEvent, Month month) : base(routedEvent)
    {
      this.Month = month;
    }

  }
}
