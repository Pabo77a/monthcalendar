using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public class PanelProperties : PropertiesBase
  {
    private int fontSize = 16;
    private FontWeight fontWeight = FontWeights.Normal;
    private FontStyle fontStyle = FontStyles.Normal;
    private FontFamily fontFamily;
    private String textDecoration;
    private Color textColor = Colors.White;
    private Color backGroundColor = Colors.Blue;

    public PanelProperties()
    {
    }

    public Color TextColor
    {
      get => textColor;
      set
      {
        if (value != textColor)
        {
          textColor = value;
          OnPropertyChanged(nameof(this.TextColor));
        }
      }
    }

    public Color BackgroundColor
    {
      get => backGroundColor;
      set
      {
        if (value != backGroundColor)
        {
          backGroundColor = value;
          OnPropertyChanged(nameof(this.BackgroundColor));
        }
      }
    }

    public int FontSize
    {
      get => fontSize;
      set
      {
        if (value != fontSize)
        {
          fontSize = value;
          OnPropertyChanged(nameof(this.FontSize));
        }
      }
    }

    public string TextDecoration
    {
      get => textDecoration;
      set
      {
        if (value != textDecoration)
        {
          textDecoration = value;
          OnPropertyChanged(nameof(this.TextDecoration));
        }
      }
    }

    public FontFamily FontFamily
    {
      get => fontFamily;
      set
      {
        if (value != fontFamily)
        {
          fontFamily = value;
          OnPropertyChanged(nameof(this.FontFamily));
        }
      }
    }

    public FontWeight FontWeight
    {
      get => fontWeight;
      set
      {
        if (value != fontWeight)
        {
          fontWeight = value;
          OnPropertyChanged(nameof(this.FontWeight));
        }
      }
    }

    public FontStyle FontStyle
    {
      get => fontStyle;
      set
      {
        if (value != fontStyle)
        {
          fontStyle = value;
          OnPropertyChanged(nameof(this.FontStyle));
        }
      }
    }
  }
}
