using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Pabo.MonthCalendar.Model
{
  public class CalendarDay : Day, INotifyPropertyChanged, IPanel
  {


    private bool selected = false;
    private bool mouseOver = false;

    private bool disabled = false;
    private bool notCurrentMonth = false;
    private bool visible = true;

    private Thickness thickness = new Thickness(1, 1, 1, 1);

    public CalendarDay(DateTime date) : base(date)
    {
    }



    public bool Selected
    {
      get => this.selected && !Disabled;
      set
      {
        if (value != this.selected)
        {
          this.selected = value;
          OnPropertyChanged(nameof(this.Selected));
        }
      }

    }


    public bool NotCurrentMonth
    {
      get => this.notCurrentMonth;
      set
      {
        if (value != this.notCurrentMonth)
        {
          this.notCurrentMonth = value;
          OnPropertyChanged(nameof(this.NotCurrentMonth));
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
          OnPropertyChanged(nameof(this.Disabled));
        }
      }

    }

    public bool Visible
    {
      get => this.visible;
      set
      {
        if (value != this.visible)
        {
          this.visible = value;
          OnPropertyChanged(nameof(this.Visible));
        }
      }

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

    public string Id => this.Date.ToString("yyyy-MM-dd");

    public Thickness BorderThickness
    {
      get => this.thickness;
      set
      {
        if (value != this.thickness)
        {
          this.thickness = value;
          OnPropertyChanged(nameof(this.BorderThickness));
        }
      }
    }

  }
}