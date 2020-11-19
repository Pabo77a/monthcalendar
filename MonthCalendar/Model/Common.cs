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

    public Thickness TextMargin
    {
      get => textMargin;
      set
      {
        if (value != textMargin)
        {
          this.textMargin = value;
          OnPropertyChanged(nameof(this.TextMargin));
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
          OnPropertyChanged(nameof(this.Text));
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
          OnPropertyChanged(nameof(this.Template));
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
          OnPropertyChanged(nameof(this.Tooltip));
        }
      }
    }

  }
}
