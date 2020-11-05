﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

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

    // Text
    private Color textColor = Colors.Black;
    private int textFontSize = 16;
    private FontWeight textFontWeight = FontWeights.Normal;
    private FontStyle textFontStyle = FontStyles.Normal;
    private FontFamily textFontFamily = new FontFamily(string.Empty);
    private string textTextDecoration = string.Empty;


    private Color backGroundColor = Colors.White;

    // Trailing
    private Color trailingDateColor = Colors.White;
    private Color trailingBackgroundColor = Colors.LightGray;

    public CalendarProperties()
    {
      
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

  }
}