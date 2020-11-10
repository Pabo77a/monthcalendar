using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pabo.MonthCalendar.EventArgs
{
  public class DayEventArgs : RoutedEventArgs
  {
    public Day Day { get; }
    public DayEventArgs(RoutedEvent routedEvent, Day day) : base(routedEvent)
    {
      this.Day = day;
    }

  }
}
