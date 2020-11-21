using System;
using System.Collections.Generic;
using System.Text;

namespace Pabo.MonthCalendar.Model
{
  public class Month : Common
  {
    private int month;
    private int year;

    public Month()
    {
    }

    public Month(int year, int month)
    {
      this.month = month;
      this.year = year;
    }


    public int Number
    {
      get => this.month;
      set
      {
        if (value != this.month)
        {
          this.month = value;
          OnPropertyChanged();
        }
      }
    }

    public int Year
    {
      get => this.year;
      set
      {
        if (value != this.year)
        {
          this.year = value;
          OnPropertyChanged();
        }
      }
    }
  }
}
