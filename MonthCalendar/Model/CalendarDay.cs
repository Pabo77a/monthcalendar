using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Pabo.MonthCalendar.Model
{
  internal class CalendarDay : Day, INotifyPropertyChanged
  {


    private bool selected = false;
   
    private Thickness thickness = new Thickness(1, 1, 1, 1);

    public CalendarDay(DateTime date) : base(date)
    {
    }
    


    public bool Selected 
    { 
      get => this.selected;
      set {
        if (value != this.selected)
        {
          this.selected = value;
          OnPropertyChanged(nameof(this.Selected));
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