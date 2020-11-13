﻿using Pabo.MonthCalendar.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace Pabo.MonthCalendar
{
  public class ItemsControl : BaseControl
  {
    public ItemsControl(int cols, int rows)
    {
      this.Cols = cols;
      this.Rows = rows;
    }

    public int Cols { get; private set; }

    public int Rows { get; private set; }

    protected Pos GetItemPos(Point pt)
    {
      var itemWidth = this.ActualWidth / this.Cols;
      var itemHeight = this.ActualHeight / this.Rows;

      var row = (pt.Y / itemHeight) +1;
      var col = (pt.X / itemWidth) + 1;

      return new Pos((int)col,(int)row);
    }


    protected int GetItem(Point pt)
    {

      var itemWidth = this.ActualWidth / this.Cols;
      var itemHeight = this.ActualHeight / this.Rows;

      int x = (int)(pt.X / itemWidth);
      int y = (int)(pt.Y / itemHeight);

      var panel = Cols > 1 ? 
        x + (y * this.Rows) + y : 
        x + y;
  
      return panel;
    }
  }
}
