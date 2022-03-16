
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace Pabo.MonthCalendar.Model
{
  public class CalendarWeek : Week, INotifyPropertyChanged, IPanel
  {
    private bool mouseOver;
    private bool selected;
    private bool disabled;
    private Thickness thickness;

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
          OnPropertyChanged();
        }
      }

    }

    public bool Selected
    {
      get => this.selected && !Disabled;
      set
      {
        if (value != this.selected)
        {
          this.selected = value;
          OnPropertyChanged();
        }
      }

    }

    public bool Disabled
    {
      get => this.disabled;
      set
      {
        if (value != this.disabled)
        {
          this.disabled = value;
          OnPropertyChanged();
        }
      }

    }

    public Thickness BorderThickness
    {
      get => this.thickness;
      set
      {
        if (value != this.thickness)
        {
          this.thickness = value;
          OnPropertyChanged();
        }
      }
    }


    public DateTime FirstDateOWeek { get; set; }

    public string Id => $"{this.Year}-{this.Number}";

    public string Weeknumber
    {
      get
      {
        return !string.IsNullOrEmpty(Text) ? Text : ISOWeek.GetWeekOfYear(FirstDateOWeek).ToString();
      }
    }

  }
}
