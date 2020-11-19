using Pabo.MonthCalendar.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Model
{

  

  public class Week : Common
  {
    private int number;
    private int year;

    public int Number
    {
      get => number;
      set
      {
        if (value != number)
        {
          this.number = value;
          OnPropertyChanged(nameof(this.Number));
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
  }
}
