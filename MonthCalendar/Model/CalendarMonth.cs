using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace Pabo.MonthCalendar.Model
{
  public class CalendarMonth : Month, INotifyPropertyChanged, IPanel
  {

    private bool selected = false;
    private bool mouseOver = false;

    private bool disabled = false;

    private Thickness thickness = new Thickness(1, 1, 1, 1);

    public CalendarMonth()
    {

    }

    public CalendarMonth(int year, int month) : base(year, month)
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

    public string Id => $"{this.Year}-{this.Number}";

    public override string Text 
    { 
      get => !string.IsNullOrEmpty(this.text) ? this.text : new DateTime(this.Year, this.Number, 1).ToString("MMM"); 
      set => base.Text = value; 
    }

  }
}
