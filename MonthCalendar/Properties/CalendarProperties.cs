using System.Windows;
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

    // Not current month
    private Color notCurrentMonthDateColor = Colors.White;
    private Color notCurrentMonthBackgroundColor = Colors.LightGray;
    private bool showNotCurrentMonth = true;

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
          OnPropertyChanged();
        }
      }
    }

    public Color NotCurrentMonthBackgroundColor
    {
      get => notCurrentMonthBackgroundColor;
      set
      {
        if (value != notCurrentMonthBackgroundColor)
        {
          notCurrentMonthBackgroundColor = value;
          OnPropertyChanged();
        }
      }
    }

    public Color NotCurrentMonthDateColor
    {
      get => notCurrentMonthDateColor;
      set
      {
        if (value != notCurrentMonthDateColor)
        {
          notCurrentMonthDateColor = value;
          OnPropertyChanged();
        }
      }
    }

    public bool ShowNotCurrentMonth
    {
      get => showNotCurrentMonth;
      set
      {
        if (value != showNotCurrentMonth)
        {
          showNotCurrentMonth = value;
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
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
          OnPropertyChanged();
        }
      }
    }

    #endregion


  }
}