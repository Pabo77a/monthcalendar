using Pabo.MonthCalendar.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Model
{
  public class Common : NotificationHandler, INotifyPropertyChanged
  {

    private Color textColor = Colors.Black;
    private Color textBackgroundColor = Colors.Transparent;
    private FontStyle textFontStyle = FontStyles.Normal;
    private FontWeight textFontWeight = FontWeights.Normal;
    private int textFontSize = 16;
    private string textTextDecoration = "";
    private VerticalAlignment textVerticalAlignment = VerticalAlignment.Center;
    private HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Center;
    private FontFamily textFontFamily = new FontFamily("");
    private Thickness textMargin = new Thickness(10, 0, 10, 0);
    protected string text = "";
 
    private Color backgroundColor;
    private DataTemplate template;

    private string toolTip;

    public Color BackgroundColor
    {
      get => backgroundColor;
      set
      {
        if (value != backgroundColor)
        {
          this.backgroundColor = value;
          OnPropertyChanged();
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
          OnPropertyChanged();
        }
      }
    }

    public Color TextBackgroundColor
    {
      get => textBackgroundColor;
      set
      {
        if (value != textBackgroundColor)
        {
          this.textBackgroundColor = value;
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
        }
      }
    }

    public Thickness TextMargin
    {
      get => textMargin;
      set
      {
        if (value != textMargin)
        {
          this.textMargin = value;
          OnPropertyChanged();
        }
      }
    }

    public virtual string Text
    {
      get => text;
      set
      {
        if (value != text)
        {
          this.text = value;
          OnPropertyChanged();
        }
      }
    }

    public DataTemplate Template
    {
      get => this.template;
      set
      {
        if (value != this.template)
        {
          this.template = value;
          OnPropertyChanged();
        }
      }

    }

    public string Tooltip
    {
      get => toolTip;
      set
      {
        if (value != toolTip)
        {
          this.toolTip = value;
          OnPropertyChanged();
        }
      }
    }

  }
}
