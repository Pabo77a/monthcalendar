using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public class HeaderProperties : PanelProperties
  {

    private string text = string.Empty;
    private string dateFormat = "MMMM yyyy";


    public HeaderProperties()
    {
      this.Month = 1;
      this.Year = 1990;
    }


    internal int Month { get; set; }

    internal int Year {  get; set; }

    public string DateFormat
    {
      get => dateFormat;
      set
      {
        if (value != dateFormat)
        {
          dateFormat = value;
          OnPropertyChanged(nameof(this.Text));
        }
      }
    }


    public string Text
    {
      get
      {
        return !string.IsNullOrEmpty(this.text) ? this.text : new DateTime(this.Year, this.Month, 1).ToString(this.DateFormat); 
      }
      set
      {
        if (value != text)
        {
          text = value;
          OnPropertyChanged(nameof(this.Text));
        }
      }
    }

    internal void SetDate(int year, int month)
    {
      this.Month = month;
      this.Year = year;
      OnPropertyChanged(nameof(this.Text));

    }
  }
}
