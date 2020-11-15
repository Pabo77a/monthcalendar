using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Pabo.MonthCalendar.Model
{
  internal class CalendarDay : Day, INotifyPropertyChanged
  {


    private bool selected = false;
    private bool mouseOver = false;

    private bool disabled = false;
    private bool trailing = false;

    private Thickness thickness = new Thickness(1, 1, 1, 1);

    public CalendarDay(DateTime date) : base(date)
    {
    }
    


    public bool Selected 
    { 
      get => this.selected && !Disabled;
      set {
        if (value != this.selected)
        {
          this.selected = value;
          OnPropertyChanged(nameof(this.Selected));
        }
      }
         
    }

    public bool Trailing
    {
      get => this.trailing;
      set
      {
        if (value != this.trailing)
        {
          this.trailing = value;
          OnPropertyChanged(nameof(this.Trailing));
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
    
  }
}