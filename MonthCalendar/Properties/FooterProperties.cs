using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public class FooterProperties : PanelProperties
  {

    private string text;

    public FooterProperties()
    {
      this.TextFontWeight = FontWeights.Bold;
      this.TextColor = Colors.Black;
      this.BackgroundColor = Colors.White;
      this.Text = string.Empty;
    }

    public string Text
    {
      get
      {
        return !string.IsNullOrEmpty(this.text) ? this.text : DateTime.Now.ToShortDateString();
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
