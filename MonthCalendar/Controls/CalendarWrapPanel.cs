using System.Windows;
using System.Windows.Controls;


namespace Pabo.MonthCalendar.Controls
{
  public class CalendarWrapPanel : WrapPanel
  {

    #region dependency properties

    public static readonly DependencyProperty RowsProperty = DependencyProperty.Register("Rows",
               typeof(int),
               typeof(CalendarWrapPanel),
               new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty ColsProperty = DependencyProperty.Register("Cols",
               typeof(int),
               typeof(CalendarWrapPanel),
               new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    #endregion

    #region properties

    internal int Rows
    {
      get
      {
        return (int)this.GetValue(RowsProperty);
      }
      set
      {
        this.SetValue(RowsProperty, value);
      }
    }

    internal int Cols
    {
      get
      {
        return (int)this.GetValue(ColsProperty);
      }
      set
      {
        this.SetValue(ColsProperty, value);
      }
    }

    #endregion

    protected override Size MeasureOverride(Size availableSize)
    {
      // get tile size
      Size tileSize = GetTileSize((int)availableSize.Width, (int)availableSize.Height, Cols, Rows);
      foreach (UIElement child in this.InternalChildren)
      {
        // measure each child with a square it should occupy
        child.Measure(tileSize);
      }
   
      return availableSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      var tileSize = GetTileSize((int)finalSize.Width, (int)finalSize.Height, Cols, Rows);
      double x = 0, y = 0;
      int col = 1;
      int row = 1;

      double remainderX = (int)finalSize.Width - Cols * tileSize.Width;
      double remainderY = (int)finalSize.Height - Rows * tileSize.Height;

      foreach (UIElement child in this.InternalChildren)
      {

        if (col > Cols)
        {
          // if need to move on next row - do that
          col = 1;
          row++;
          x = 0;
          y += tileSize.Height;
        }
        // arrange in square
        var tmp = tileSize;
        
        // Add remainders to fill out space
        if (col == Cols)
          tmp.Width += remainderX;
        if (row == Rows)
          tmp.Height += remainderY;

        child.Arrange(new Rect(new Point(x, y), tmp));
        x += tileSize.Width;
        col++;
      }
      return finalSize;
    }

    Size GetTileSize(int width, int height, int cols, int rows)
    {

      double tileWidth = width / cols;
      double tileHeight = height / rows;
      if ((width < 0) || (height < 0))
      {
        return new Size(0, 0);
      }

      return new Size(tileWidth, tileHeight);
    }
  }
}
