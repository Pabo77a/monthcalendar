using Pabo.MonthCalendar.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Model
{
  public class Weekday : NotificationHandler, INotifyPropertyChanged
  {

    private Color textColor = Colors.Black;
    private FontStyle fontStyle = FontStyles.Normal;
    private FontWeight fontWeight = FontWeights.Normal;
    private int fontSize = 16;
    private string textDecoration = "";
    private VerticalAlignment verticalAlignment = VerticalAlignment.Center;
    private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center;
    private FontFamily fontFamily = new FontFamily("");
    private string text = "";
    private int number;
    private int year;
    private int month;
    private DayOfWeek dayOfWeek;

    private Color backgroundColor;


    public DayOfWeek DayOfWeek
    {
      get => dayOfWeek;
      set
      {
        if (value != dayOfWeek)
        {
          this.dayOfWeek = value;
          OnPropertyChanged(nameof(this.DayOfWeek));
        }
      }
    }

    public int Year
    {
      get => year;
      set
      {
        if (value != year)
        {
          this.year = value;
          OnPropertyChanged(nameof(this.Year));
        }
      }
    }

    public int Month
    {
      get => month;
      set
      {
        if (value != month)
        {
          this.month = value;
          OnPropertyChanged(nameof(this.Month));
        }
      }
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

    public int FontSize
    {
      get => fontSize;
      set
      {
        if (value != fontSize)
        {
          this.fontSize = value;
          OnPropertyChanged(nameof(this.FontSize));
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
          this.fontWeight = value;
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
          this.fontStyle = value;
          OnPropertyChanged(nameof(this.FontStyle));
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
          this.fontFamily = value;
          OnPropertyChanged(nameof(this.FontFamily));
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
          this.textDecoration = value;
          OnPropertyChanged(nameof(this.textDecoration));
        }
      }
    }

    public VerticalAlignment VerticalAlignment
    {
      get => verticalAlignment;
      set
      {
        if (value != verticalAlignment)
        {
          this.verticalAlignment = value;
          OnPropertyChanged(nameof(this.verticalAlignment));
        }
      }
    }

    public HorizontalAlignment HorizontalAlignment
    {
      get => horizontalAlignment;
      set
      {
        if (value != horizontalAlignment)
        {
          this.horizontalAlignment = value;
          OnPropertyChanged(nameof(this.HorizontalAlignment));
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


  }
}
