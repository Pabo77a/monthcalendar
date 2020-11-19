using Pabo.MonthCalendar.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Model
{
  public class Weekday : Common
  {

    private int year;
    private int month;
    private DayOfWeek dayOfWeek;


    public DayOfWeek DayOfWeek
    {
      get => dayOfWeek;
      set
      {
        if (value != dayOfWeek)
        {
          this.dayOfWeek = value;
          OnPropertyChanged(nameof(this.DayOfWeek));
        }
      }
    }

    public int Year
    {
      get => year;
      set
      {
        if (value != year)
        {
          this.year = value;
          OnPropertyChanged(nameof(this.Year));
        }
      }
    }

    public int Month
    {
      get => month;
      set
      {
        if (value != month)
        {
          this.month = value;
          OnPropertyChanged(nameof(this.Month));
        }
      }
    }

  }
}
