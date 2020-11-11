using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pabo.MonthCalendar.EventArgs
{
  public class WeekdayEventArgs : RoutedEventArgs
  {
    public Weekday Weekday { get; }
    public WeekdayEventArgs(RoutedEvent routedEvent, Weekday weekday) : base(routedEvent)
    {
      this.Weekday = weekday;
    }

  }
}