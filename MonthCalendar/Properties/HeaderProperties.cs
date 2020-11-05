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

    private string dateText = string.Empty;
    private string text = string.Empty;
    private string dateFormat = "MMMM yyyy";
      

    public HeaderProperties()
    {
    }
 
    public string DateText
    {
      get => dateText;
      set
      {
        if (value != dateText)
        {
          dateText = value;
          OnPropertyChanged(nameof(this.Text));
        }
      }
    }

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
        return !string.IsNullOrEmpty(this.text)  ? this.text : this.dateText;
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
  }
}
