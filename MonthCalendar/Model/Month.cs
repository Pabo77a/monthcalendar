using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pabo.MonthCalendar.Model
{
  public class Month : Common
  {
    private int month;
    private int year;

    private BitmapImage image = null;
    private VerticalAlignment imageVerticalAlignment = VerticalAlignment.Top;
    private HorizontalAlignment imageHorizontalAlignment = HorizontalAlignment.Left;
    private Stretch imageStretch = Stretch.None;
    private Thickness imageMargin = new Thickness(2, 2, 0, 0);


    public Month()
    {
    }

    public Month(int year, int month)
    {
      this.month = month;
      this.year = year;
    }


    public int Number
    {
      get => this.month;
      set
      {
        if (value != this.month)
        {
          this.month = value;
          OnPropertyChanged();
        }
      }
    }

    public int Year
    {
      get => this.year;
      set
      {
        if (value != this.year)
        {
          this.year = value;
          OnPropertyChanged();
        }
      }
    }

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
