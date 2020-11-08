using Pabo.MonthCalendar.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Model
{
  public class Day : NotificationHandler , INotifyPropertyChanged
  {

    private Color textColor = Colors.Black;
    private FontStyle textFontStyle = FontStyles.Normal;
    private FontWeight textFontWeight = FontWeights.Normal;
    private int textFontSize = 16;
    private string textTextDecoration = "";
    private VerticalAlignment  textVerticalAlignment = VerticalAlignment.Bottom;
    private HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left;
    private FontFamily textFontFamily = new FontFamily("");
    private string text = "";

    private Color backgroundColor;

    private Color dateColor = Colors.Black;
    private FontStyle dateFontStyle = FontStyles.Normal;
    private FontWeight dateFontWeight = FontWeights.Normal;
    private string dateTextDecoration = "";
    private int dateFontSize = 16;
    private FontFamily dateFontFamily = new FontFamily("");
    private DateTime date;
    private VerticalAlignment dateVerticalAlignment = VerticalAlignment.Top;
    private HorizontalAlignment dateHorizontalAlignment = HorizontalAlignment.Right;
    


    public Day()
    {
    }

    public Day(DateTime date) : this()
    {
      this.date = date;
    }

    public Color BackgroundColor
    {
      get => backgroundColor;
      set
      {
        if (value != backgroundColor)
        {
          this.backgroundColor = value;
          OnPropertyChanged(nameof(this.BackgroundColor));
        }
      }
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


    #region Text

    public Color TextColor
    {
      get => textColor;
      set
      {
        if (value != textColor)
        {
          this.textColor = value;
          OnPropertyChanged(nameof(this.TextColor));
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
          this.textFontSize = value;
          OnPropertyChanged(nameof(this.TextFontSize));
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
          this.textFontWeight = value;
          OnPropertyChanged(nameof(this.TextFontWeight));
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
          this.textFontStyle = value;
          OnPropertyChanged(nameof(this.TextFontStyle));
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
          this.textFontFamily = value;
          OnPropertyChanged(nameof(this.TextFontFamily));
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
          this.textTextDecoration = value;
          OnPropertyChanged(nameof(this.TextTextDecoration));
        }
      }
    }

    public VerticalAlignment TextVerticalAlignment
    {
      get => textVerticalAlignment;
      set
      {
        if (value != textVerticalAlignment)
        {
          this.textVerticalAlignment = value;
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
          this.textHorizontalAlignment = value;
          OnPropertyChanged(nameof(this.TextHorizontalAlignment));
        }
      }
    }

    public string Text
    {
      get => text;
      set
      {
        if (value != text)
        {
          this.text = value;
          OnPropertyChanged(nameof(this.Text));
        }
      }
    }

    #endregion

  }
}
