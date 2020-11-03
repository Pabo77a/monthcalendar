using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Pabo.MonthCalendar.Common
{
  public class CalendarWrapPanel : WrapPanel
  {
    protected override Size MeasureOverride(Size availableSize)
    {
      // get tile size
      Size tileSize = GetTileSize((int)availableSize.Width, (int)availableSize.Height, 7, 6);
      foreach (UIElement child in this.InternalChildren)
      {
        // measure each child with a square it should occupy
        child.Measure(tileSize);
      }
      return availableSize;
    }

  /*  protected override Size MeasureOverride(System.Windows.Size availableSize)
    {
      double x = 0.0;
      double y = 0.0;
      double largestY = 0.0;
      double largestX = 0.0;
      foreach (FrameworkElement child in this.InternalChildren)
      {
        child.Measure(availableSize);
      }

      foreach (FrameworkElement child in this.InternalChildren)
      {
        if (x + child.DesiredSize.Width > availableSize.Width)
        {
          if (x > 0)
          {
            x = 0;
            y = largestY;
            x += child.DesiredSize.Width;
          }
          else
          {
            x += child.DesiredSize.Width;
          }
        }
        else
        {
          x += child.DesiredSize.Width;
        }
        largestY = Math.Max(largestY, y + child.DesiredSize.Height);
        largestX = Math.Max(largestX, x + child.DesiredSize.Width);
      }

      return new Size(largestX, largestY);
    }*/


    protected override Size ArrangeOverride(Size finalSize)
    {
      var tileSize = GetTileSize((int)finalSize.Width, (int)finalSize.Height, 7, 6);
      double x = 0, y = 0;
      int col = 1;
      foreach (UIElement child in this.InternalChildren)
      {

        if (col > 7)
        {
          // if need to move on next row - do that
          col = 1;
          x = 0;
          y += tileSize.Height;
        }
        // arrange in square
        child.Arrange(new Rect(new Point(x, y), tileSize));
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
