using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public class PanelProperties : PropertiesBase, ITooltipProperties
  {

    // Text
    private int textFontSize = 16;
    private FontWeight textFontWeight = FontWeights.Normal;
    private FontStyle textFontStyle = FontStyles.Normal;
    private FontFamily textFontFamily = new FontFamily(string.Empty);
    private String textTextDecoration = string.Empty;
    private Color textColor = Colors.White;
    private Color backgroundColor = Colors.Blue;
    private VerticalAlignment textVerticalAlignment = VerticalAlignment.Center;
    private HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Center;
    private Thickness textMargin = new Thickness(0, 0, 0, 0);

    // MouseOver
    private Color mouseOverBackgroundColor = Colors.LightBlue;
    private Color mouseOverBorderColor = Colors.Transparent;
    private double mouseOverOpacity = .25;

    //Tooltip
    private int tooltipFontSize = 12;
    private FontWeight tooltipFontWeight = FontWeights.Normal;
    private FontStyle tooltipFontStyle = FontStyles.Normal;
    private FontFamily tooltipFontFamily = new FontFamily(string.Empty);
    private Color tooltipTextColor = Colors.Black;
    private Color tooltipBackgroundColor = Colors.White;
    private Color tooltipBorderColor = Colors.Black;

    public PanelProperties()
    {
    }

    #region Text

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

    public Thickness TextMargin
    {
      get => textMargin;
      set
      {
        if (value != textMargin)
        {
          textMargin = value;
          OnPropertyChanged(nameof(this.TextMargin));
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
          backgroundColor = value;
          OnPropertyChanged(nameof(this.BackgroundColor));
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
          textFontStyle = value;
          OnPropertyChanged(nameof(this.TextFontStyle));
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

    #endregion

    #region MouseOver

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


    #region Tooltip

    public Color TooltipTextColor
    {
      get => tooltipTextColor;
      set
      {
        if (value != tooltipTextColor)
        {
          tooltipTextColor = value;
          OnPropertyChanged(nameof(this.TooltipTextColor));
        }
      }
    }

    public Color TooltipBackgroundColor
    {
      get => tooltipBackgroundColor;
      set
      {
        if (value != tooltipBackgroundColor)
        {
          tooltipBackgroundColor = value;
          OnPropertyChanged(nameof(this.TooltipBackgroundColor));
        }
      }
    }

    public Color TooltipBorderColor
    {
      get => tooltipBorderColor;
      set
      {
        if (value != tooltipBorderColor)
        {
          tooltipBorderColor = value;
          OnPropertyChanged(nameof(this.TooltipBorderColor));
        }
      }
    }

    public int TooltipFontSize
    {
      get => tooltipFontSize;
      set
      {
        if (value != tooltipFontSize)
        {
          tooltipFontSize = value;
          OnPropertyChanged(nameof(this.TooltipFontSize));
        }
      }
    }

    public FontFamily TooltipFontFamily
    {
      get => tooltipFontFamily;
      set
      {
        if (value != tooltipFontFamily)
        {
          tooltipFontFamily = value;
          OnPropertyChanged(nameof(this.TooltipFontFamily));
        }
      }
    }

    public FontWeight TooltipFontWeight
    {
      get => tooltipFontWeight;
      set
      {
        if (value != tooltipFontWeight)
        {
          tooltipFontWeight = value;
          OnPropertyChanged(nameof(this.TooltipFontWeight));
        }
      }
    }

    public FontStyle TooltipFontStyle
    {
      get => tooltipFontStyle;
      set
      {
        if (value != tooltipFontStyle)
        {
          tooltipFontStyle = value;
          OnPropertyChanged(nameof(this.TooltipFontStyle));
        }
      }
    }

    #endregion
  }
}
