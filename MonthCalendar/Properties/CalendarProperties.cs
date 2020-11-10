using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pabo.MonthCalendar.Properties
{
  public class CalendarProperties : PropertiesBase
  {
    // Date
    private Color dateColor = Colors.Black;
    private int dateFontSize = 16;
    private FontWeight dateFontWeight = FontWeights.Normal;
    private FontStyle dateFontStyle = FontStyles.Normal;
    private FontFamily dateFontFamily = new FontFamily(string.Empty);
    private string dateTextDecoration = string.Empty;
    private VerticalAlignment dateVerticalAlignment = VerticalAlignment.Top;
    private HorizontalAlignment dateHorizontalAlignment = HorizontalAlignment.Right;


    // Text
    private Color textColor = Colors.Black;
    private int textFontSize = 16;
    private FontWeight textFontWeight = FontWeights.Normal;
    private FontStyle textFontStyle = FontStyles.Normal;
    private FontFamily textFontFamily = new FontFamily(string.Empty);
    private string textTextDecoration = string.Empty;
    private VerticalAlignment textVerticalAlignment = VerticalAlignment.Bottom;
    private HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left;


    private BitmapImage backgroundImage = null;
    private Color backgroundColor = Colors.White;

    // Trailing
    private Color trailingDateColor = Colors.White;
    private Color trailingBackgroundColor = Colors.LightGray;

    // Selected
    private Color selectedBackgroundColor = Colors.LightBlue;
    private Color selectedBorderColor = Colors.Blue;
    private double selectedOpacity = .25;


    // MouseOver
    private Color mouseOverBackgroundColor = Colors.LightBlue;
    private Color mouseOverBorderColor = Colors.Transparent;
    private double mouseOverOpacity = .25;


    public CalendarProperties()
    {
      
    }

    public Color BackgroundColor
    {
      get => backgroundColor;
      set
      {
        if (value != backgroundColor)
        {
          backgroundColor = value;
          OnPropertyChanged(nameof(this.BackgroundColor));
        }
      }
    }

    public BitmapImage BackgroundImage
    {
      get => backgroundImage;
      set
      {
        if (value != backgroundImage)
        {
          backgroundImage = value;
          OnPropertyChanged(nameof(this.BackgroundImage));
        }
      }
    }

    public Color TrailingBackgroundColor
    {
      get => trailingBackgroundColor;
      set
      {
        if (value != trailingBackgroundColor)
        {
          trailingBackgroundColor = value;
          OnPropertyChanged(nameof(this.TrailingBackgroundColor));
        }
      }
    }

    public Color TrailingDateColor
    {
      get => trailingDateColor;
      set
      {
        if (value != trailingDateColor)
        {
          trailingDateColor = value;
          OnPropertyChanged(nameof(this.TrailingDateColor));
        }
      }
    }


    #region Text

    public VerticalAlignment TextVerticalAlignment
    {
      get => textVerticalAlignment;
      set
      {
        if (value != textVerticalAlignment)
        {
          textVerticalAlignment = value;
          OnPropertyChanged(nameof(this.TextVerticalAlignment));
        }
      }
    }

    public HorizontalAlignment TextHorizontalAlignment
    {
      get => textHorizontalAlignment;
      set
      {
        if (value != textHorizontalAlignment)
        {
          textHorizontalAlignment = value;
          OnPropertyChanged(nameof(this.TextHorizontalAlignment));
        }
      }
    }


    public string TextTextDecoration
    {
      get => textTextDecoration;
      set
      {
        if (value != textTextDecoration)
        {
          textTextDecoration = value;
          OnPropertyChanged(nameof(this.TextTextDecoration));
        }
      }
    }

    public FontFamily TextFontFamily
    {
      get => textFontFamily;
      set
      {
        if (value != textFontFamily)
        {
          textFontFamily = value;
          OnPropertyChanged(nameof(this.TextFontFamily));
        }
      }
    }

    public FontWeight TextFontWeight
    {
      get => textFontWeight;
      set
      {
        if (value != textFontWeight)
        {
          textFontWeight = value;
          OnPropertyChanged(nameof(this.textFontWeight));
        }
      }
    }

    public FontStyle TextFontStyle
    {
      get => textFontStyle;
      set
      {
        if (value != textFontStyle)
        {
          textFontStyle = value;
          OnPropertyChanged(nameof(this.TextFontStyle));
        }
      }
    }

    public Color TextColor
    {
      get => textColor;
      set
      {
        if (value != textColor)
        {
          textColor = value;
          OnPropertyChanged(nameof(this.textColor));
        }
      }
    }

    public int TextFontSize
    {
      get => textFontSize;
      set
      {
        if (value != textFontSize)
        {
          textFontSize = value;
          OnPropertyChanged(nameof(this.TextFontSize));
        }
      }
    }


    #endregion

    #region Date

    public VerticalAlignment DateVerticalAlignment
    {
      get => dateVerticalAlignment;
      set
      {
        if (value != dateVerticalAlignment)
        {
          dateVerticalAlignment = value;
          OnPropertyChanged(nameof(this.DateVerticalAlignment));
        }
      }
    }

    public HorizontalAlignment DateHorizontalAlignment
    {
      get => dateHorizontalAlignment;
      set
      {
        if (value != dateHorizontalAlignment)
        {
          dateHorizontalAlignment = value;
          OnPropertyChanged(nameof(this.DateHorizontalAlignment));
        }
      }
    }

    public string DateTextDecoration
    {
      get => dateTextDecoration;
      set
      {
        if (value != dateTextDecoration)
        {
          dateTextDecoration = value;
          OnPropertyChanged(nameof(this.DateTextDecoration));
        }
      }
    }

    public FontFamily DateFontFamily
    {
      get => dateFontFamily;
      set
      {
        if (value != dateFontFamily)
        {
          dateFontFamily = value;
          OnPropertyChanged(nameof(this.DateFontFamily));
        }
      }
    }

    public FontWeight DateFontWeight
    {
      get => dateFontWeight;
      set
      {
        if (value != dateFontWeight)
        {
          dateFontWeight = value;
          OnPropertyChanged(nameof(this.dateFontWeight));
        }
      }
    }

    public FontStyle DateFontStyle
    {
      get => dateFontStyle;
      set
      {
        if (value != dateFontStyle)
        {
          dateFontStyle = value;
          OnPropertyChanged(nameof(this.DateFontStyle));
        }
      }
    }



    public Color DateColor
    {
      get => dateColor;
      set
      {
        if (value != dateColor)
        {
          dateColor = value;
          OnPropertyChanged(nameof(this.DateColor));
        }
      }
    }

    public int DateFontSize
    {
      get => dateFontSize;
      set
      {
        if (value != dateFontSize)
        {
          dateFontSize = value;
          OnPropertyChanged(nameof(this.DateFontSize));
        }
      }
    }

    #endregion

    #region Selected

    public Color SelectedBackgroundColor
    {
      get => selectedBackgroundColor;
      set
      {
        if (value != selectedBackgroundColor)
        {
          selectedBackgroundColor = value;
          OnPropertyChanged(nameof(this.SelectedBackgroundColor));
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
          OnPropertyChanged(nameof(this.SelectedBorderColor));
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
          OnPropertyChanged(nameof(this.SelectedOpacity));
        }
      }
    }

    #endregion

    #region Mouse over

    public Color MouseOverBackgroundColor
    {
      get => mouseOverBackgroundColor;
      set
      {
        if (value != mouseOverBackgroundColor)
        {
          mouseOverBackgroundColor = value;
          OnPropertyChanged(nameof(this.MouseOverBackgroundColor));
        }
      }
    }

    public Color MouseOverBorderColor
    {
      get => mouseOverBorderColor;
      set
      {
        if (value != mouseOverBorderColor)
        {
          mouseOverBorderColor = value;
          OnPropertyChanged(nameof(this.MouseOverBorderColor));
        }
      }
    }

    public double MouseOverOpacity
    {
      get => mouseOverOpacity;
      set
      {
        if (value != mouseOverOpacity)
        {
          mouseOverOpacity = value;
          OnPropertyChanged(nameof(this.MouseOverOpacity));
        }
      }
    }

    #endregion

  }
}