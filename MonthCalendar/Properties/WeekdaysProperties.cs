using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public class WeekdaysProperties : PanelProperties
  {

    private bool abbreviatedNames = true;

    public WeekdaysProperties() : base()
    {
      this.BackgroundColor = Colors.White;
      this.TextColor = Colors.Blue;

      this.TextHorizontalAlignment = HorizontalAlignment.Center;
      this.TextVerticalAlignment = VerticalAlignment.Center;
      this.TextMargin = new Thickness(10, 0, 10, 0);

      this.MouseOverBackgroundColor = Colors.Transparent;
      this.MouseOverBorderColor = Colors.Transparent;
      this.MouseOverOpacity = .25;
      
    }

    public bool AbbreviatedNames
    {
      get => this.abbreviatedNames;
      set
      {
        if (abbreviatedNames != value)
        {
          this.abbreviatedNames = value;
          OnPropertyChanged(nameof(this.AbbreviatedNames));
        }
      }
    }
  }
}
