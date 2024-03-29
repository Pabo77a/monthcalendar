﻿using System;
using System.ComponentModel;
using System.Windows;

namespace Pabo.MonthCalendar.Model
{
  public class CalendarWeekday : Weekday, INotifyPropertyChanged, IPanel
  {

    private bool mouseOver = false;
    private string name;
    private bool selected;
    private bool disabled;
    private Thickness thickness;

    public CalendarWeekday()
    {
    }

    public CalendarWeekday(DayOfWeek day) : this()
    {
      DayOfWeek = day;
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

    public string Id => $"{this.Year}-{this.Month}-{this.DayOfWeek}";

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
          OnPropertyChanged();
        }
      }

    }

  }
}
