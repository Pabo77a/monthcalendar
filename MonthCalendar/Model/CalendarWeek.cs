
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Pabo.MonthCalendar.Model
{
  internal class CalendarWeek : Week, INotifyPropertyChanged
  {
    private bool mouseOver;

    public CalendarWeek()
    {
    }

    public CalendarWeek(DateTime date) : this()
    {
      FirstDateOWeek = date;
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


    public DateTime FirstDateOWeek { get; set; }
  
    public string Weeknumber
    {
      get
      {
        return !string.IsNullOrEmpty(Text) ? Text : ISOWeek.GetWeekOfYear(FirstDateOWeek).ToString();
      }
    }

  }
}
