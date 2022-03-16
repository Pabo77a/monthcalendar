using System;
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
      this.TextVerticalAlignment = VerticalAlignment.Center;
      this.TextHorizontalAlignment = HorizontalAlignment.Left;
      this.TextMargin = new Thickness(1, 1, 0, 1);
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
          OnPropertyChanged();
        }
      }
    }

  }
}
