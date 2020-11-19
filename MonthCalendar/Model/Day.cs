using Pabo.MonthCalendar.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pabo.MonthCalendar.Model
{
  public class Day : Common
  {
 
    private BitmapImage image = null;
    private VerticalAlignment imageVerticalAlignment = VerticalAlignment.Top;
    private HorizontalAlignment imageHorizontalAlignment = HorizontalAlignment.Left;
    private Stretch imageStretch = Stretch.None;
    private Thickness imageMargin = new Thickness(2,2,0,0);
  
    private Color dateColor = Colors.Black;
    private FontStyle dateFontStyle = FontStyles.Normal;
    private FontWeight dateFontWeight = FontWeights.Normal;
    private string dateTextDecoration = "";
    private int dateFontSize = 16;
    private FontFamily dateFontFamily = new FontFamily("");
    private DateTime date;
    private VerticalAlignment dateVerticalAlignment = VerticalAlignment.Top;
    private HorizontalAlignment dateHorizontalAlignment = HorizontalAlignment.Right;
    private Thickness dateMargin = new Thickness(0, 4, 7, 0);

    public Day()
    {
      TextVerticalAlignment = VerticalAlignment.Bottom;
      TextHorizontalAlignment = HorizontalAlignment.Left;
      TextMargin = new Thickness(2, 0, 0, 2);
    }

    public Day(DateTime date) : this()
    {
      this.date = date;
    }


    #region Date

    public DateTime Date
    {
      get => date;
      set
      {
        if (value != date)
        {
          this.date = value;
          OnPropertyChanged(nameof(this.Date));
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
          this.dateFontFamily = value;
          OnPropertyChanged(nameof(this.DateFontFamily));
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
          this.dateTextDecoration = value;
          OnPropertyChanged(nameof(this.DateTextDecoration));
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
          this.dateColor = value;
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
          this.dateFontSize = value;
          OnPropertyChanged(nameof(this.DateFontSize));
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
          this.dateFontWeight = value;
          OnPropertyChanged(nameof(this.DateFontWeight));
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
          this.dateFontStyle = value;
          OnPropertyChanged(nameof(this.DateFontStyle));
        }
      }
    }

    public VerticalAlignment DateVerticalAlignment
    {
      get => dateVerticalAlignment;
      set
      {
        if (value != dateVerticalAlignment)
        {
          this.dateVerticalAlignment = value;
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
          this.dateHorizontalAlignment = value;
          OnPropertyChanged(nameof(this.DateHorizontalAlignment));
        }
      }
    }

    #endregion
       

    #region Image

    public BitmapImage Image
    {
      get => image;
      set
      {
        if (value != image)
        {
          image = value;
          OnPropertyChanged(nameof(this.Image));
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

    public Stretch ImageStretch
    {
      get => imageStretch;
      set
      {
        if (value != imageStretch)
        {
          imageStretch = value;
          OnPropertyChanged(nameof(this.ImageStretch));
        }
      }
    }

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
  }
}
