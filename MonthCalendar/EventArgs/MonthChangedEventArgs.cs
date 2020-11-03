using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation.Peers;

namespace Pabo.MonthCalendar.EventArgs
{
  public class MonthChangedEventArgs : RoutedEventArgs
  {
    public int Month { get; }

    public int Year { get; }

 
    public MonthChangedEventArgs(RoutedEvent routedEvent, int month, int year) : base(routedEvent)
    {
      this.Month = month;
      this.Year = year;
    }

  }
}

