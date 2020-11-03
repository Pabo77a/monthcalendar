using System.Windows;

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

