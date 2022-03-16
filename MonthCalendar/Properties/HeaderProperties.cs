using Pabo.MonthCalendar.Common;
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
 
    public HeaderProperties() : base()
    {
      this.Month = 1;
      this.Year = 1990;
      this.TextFontSize = 16;
      this.TextVerticalAlignment = VerticalAlignment.Center;
      this.TextHorizontalAlignment = HorizontalAlignment.Center;
    }


    internal int Month { get; set; }

    internal int Year { get; set; }

    internal VisualMode VisualMode {get; set;}

    public string DateFormat
    {
      get => dateFormat;
      set
      {
        if (value != dateFormat)
        {
          dateFormat = value;
          OnPropertyChanged(nameof(this.VisualMode));
        }
      }
    }


    public string Text
    {
      get
      {
        return !string.IsNullOrEmpty(this.text) ? this.text : 
          VisualMode == VisualMode.Days ? new DateTime(this.Year, this.Month, 1).ToString(this.DateFormat) : this.Year.ToString(); 
      }
      set
      {
        if (value != text)
        {
          text = value;
          OnPropertyChanged();
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
