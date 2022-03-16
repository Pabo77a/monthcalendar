using System;

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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
        }
      }
    }

  }
}
