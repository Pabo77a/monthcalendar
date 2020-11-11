using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pabo.MonthCalendar.EventArgs
{
  public class WeekEventArgs : RoutedEventArgs
  {
    public Week Week { get; }
    public WeekEventArgs(RoutedEvent routedEvent, Week week) : base(routedEvent)
    {
      this.Week = week;
    }

  }
}