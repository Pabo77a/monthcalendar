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
  public class CalendarProperties : PanelProperties
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
    private Thickness dateMargin = new Thickness(0, 4, 7, 0);

    //Image
    private Thickness imageMargin = new Thickness(2,2,0,0);
    private VerticalAlignment imageVerticalAlignment = VerticalAlignment.Top;
    private HorizontalAlignment imageHorizontalAlignment = HorizontalAlignment.Left;


    private BitmapImage backgroundImage = null;

    // Trailing
    private Color trailingDateColor = Colors.White;
    private Color trailingBackgroundColor = Colors.LightGray;

    // Selected
    private Color selectedBackgroundColor = Colors.LightBlue;
    private Color selectedBorderColor = Colors.Blue;
    private double selectedOpacity = .25;

    // Disabled
    private Color disabledBackgroundColor = Colors.LightGray;
    private Color disabledColor = Colors.Red;
    private double disabledOpacity = .90;

    public CalendarProperties() : base()
    {
      this.MouseOverBackgroundColor = Colors.LightBlue;
      this.MouseOverBorderColor = Colors.Transparent;
      this.MouseOverOpacity = .25;
      this.TextColor = Colors.Black;
      this.BackgroundColor = Colors.White;

      this.TextFontSize = 16;
      this.TextFontWeight = FontWeights.Normal;
      this.TextFontStyle = FontStyles.Normal;
      this.TextFontFamily = new FontFamily(string.Empty);
      this.TextTextDecoration = string.Empty;

      this.TextVerticalAlignment = VerticalAlignment.Bottom;
      this.TextHorizontalAlignment = HorizontalAlignment.Left;
      this.TextMargin = new Thickness(2, 0, 0, 2);
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

    public Thickness DateMargin
    {
      get => dateMargin;
      set
      {
        if (value != dateMargin)
        {
          dateMargin = value;
          OnPropertyChanged(nameof(this.DateMargin));
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

    #region Image

    public Thickness ImageMargin
    {
      get => imageMargin;
      set
      {
        if (value != imageMargin)
        {
          imageMargin = value;
          OnPropertyChanged(nameof(this.ImageMargin));
        }
      }
    }

    public VerticalAlignment ImageVerticalAlignment
    {
      get => imageVerticalAlignment;
      set
      {
        if (value != imageVerticalAlignment)
        {
          imageVerticalAlignment = value;
          OnPropertyChanged(nameof(this.ImageVerticalAlignment));
        }
      }
    }

    public HorizontalAlignment ImageHorizontalAlignment
    {
      get => imageHorizontalAlignment;
      set
      {
        if (value != imageHorizontalAlignment)
        {
          imageHorizontalAlignment = value;
          OnPropertyChanged(nameof(this.ImageHorizontalAlignment));
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

    #region Disabled

    public Color DisabledBackgroundColor
    {
      get => disabledBackgroundColor;
      set
      {
        if (value != disabledBackgroundColor)
        {
          disabledBackgroundColor = value;
          OnPropertyChanged(nameof(this.DisabledBackgroundColor));
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
          OnPropertyChanged(nameof(this.DisabledColor));
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
          OnPropertyChanged(nameof(this.DisabledOpacity));
        }
      }
    }

    #endregion


  }
}