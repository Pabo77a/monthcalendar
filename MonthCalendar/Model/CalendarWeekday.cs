using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Pabo.MonthCalendar.Model
{
  internal class CalendarWeekday : Weekday, INotifyPropertyChanged
  {

    private bool mouseOver = false;
    private string name;

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
      get => name;
      set
      {
        if (value != name)
        {
          this.name = value;
          OnPropertyChanged();
        }
      }
    }

    public override string Text
    {
      get
      {
          return !string.IsNullOrEmpty(this.text) ? this.text: Name;
      }
      set
      {
        if (this.text != value)
        {
          this.text = value;
          OnPropertyChanged(nameof(this.text));
        }
      }

    }



    /*     


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
     }*/

  }
}
