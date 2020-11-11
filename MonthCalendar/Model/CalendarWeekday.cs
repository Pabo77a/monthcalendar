using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Pabo.MonthCalendar.Model
{
  internal class CalendarWeekday : Weekday, INotifyPropertyChanged
  {

    private bool mouseOver = false;

    public CalendarWeekday()
    {
    }

    public CalendarWeekday(DayOfWeek day) : this()
    {
      DayOfWeek = day;
    }


    public bool MouseOver
    {
      get => this.mouseOver;
      set
      {
        if (value != this.mouseOver)
        {
          this.mouseOver = value;
          OnPropertyChanged(nameof(this.MouseOver));
        }
      }

    }


    public string Name
    {
      get
      {
        if (!string.IsNullOrEmpty(Text))
        {
          return Text;
        }
        else
        {
          CultureInfo ci = Thread.CurrentThread.CurrentCulture;
          return ci.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek);
        }
      }
    }

  }
}
