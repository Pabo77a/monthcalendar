using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public class MonthProperties : PanelProperties
  {

    // Selected
    private Color selectedBackgroundColor = Colors.LightBlue;
    private Color selectedBorderColor = Colors.Blue;
    private double selectedOpacity = .25;
    
    private bool abbreviatedNames = true;

    // Disabled
    private Color disabledBackgroundColor = Colors.LightGray;
    private Color disabledColor = Colors.Red;
    private double disabledOpacity = .90;

    public MonthProperties() : base()
    {
      this.TextColor = Colors.Blue;
      this.BackgroundColor = Colors.White;
      this.TextHorizontalAlignment = HorizontalAlignment.Center;
      this.TextVerticalAlignment = VerticalAlignment.Center;
      this.TextMargin = new Thickness(10, 0, 10, 0);

      this.MouseOverBackgroundColor = Colors.LightBlue;
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
          OnPropertyChanged();
        }
      }
    }

    #region Selected

    public Color SelectedBackgroundColor
    {
      get => selectedBackgroundColor;
      set
      {
        if (value != selectedBackgroundColor)
        {
          selectedBackgroundColor = value;
          OnPropertyChanged();
        }
      }
    }

    public Color SelectedBorderColor
    {
      get => selectedBorderColor;
      set
      {
        if (value != selectedBorderColor)
        {
          selectedBorderColor = value;
          OnPropertyChanged();
        }
      }
    }

    public double SelectedOpacity
    {
      get => selectedOpacity;
      set
      {
        if (value != selectedOpacity)
        {
          selectedOpacity = value;
          OnPropertyChanged();
        }
      }
    }

    #endregion

    #region Disabled

    public Color DisabledBackgroundColor
    {
      get => disabledBackgroundColor;
      set
      {
        if (value != disabledBackgroundColor)
        {
          disabledBackgroundColor = value;
          OnPropertyChanged();
        }
      }
    }

    public Color DisabledColor
    {
      get => disabledColor;
      set
      {
        if (value != disabledColor)
        {
          disabledColor = value;
          OnPropertyChanged();
        }
      }
    }

    public double DisabledOpacity
    {
      get => disabledOpacity;
      set
      {
        if (value != disabledOpacity)
        {
          disabledOpacity = value;
          OnPropertyChanged();
        }
      }
    }

    #endregion
  }
}