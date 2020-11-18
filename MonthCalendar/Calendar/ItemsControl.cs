using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Pabo.MonthCalendar
{
  public class ItemsControl : BaseControl
  {

    protected Popup popup;
    protected bool mouseDown = false;

    public ItemsControl(int cols, int rows)
    {
      this.Cols = cols;
      this.Rows = rows;

      this.MouseMove += ItemsControl_MouseMove;
    }

    public int Cols { get; private set; }

    public int Rows { get; private set; }

    private void ItemsControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
      if (popup != null)
      {
        popup.PlacementTarget = sender as UIElement; ;
        popup.VerticalOffset = e.GetPosition(this).Y + 16;
        popup.HorizontalOffset = e.GetPosition(this).X + 16;
        popup.Placement = PlacementMode.Relative;
      }
    }

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

    protected void SetTooltip(string text)
    {
      var textBlock = ((Border)popup.Child).Child as TextBlock;
      textBlock.Text = text;
      popup.IsOpen = !string.IsNullOrEmpty(text) && !this.mouseDown;
    }

    protected Popup CreatePopup(ITooltipProperties properties)
    {

      Popup popup = new Popup();

      Border border = new Border();
      border.BorderThickness = new Thickness(1, 1, 1, 1);
      border.BorderBrush = new SolidColorBrush(properties.TooltipBorderColor);
      border.Background = new SolidColorBrush(properties.TooltipBackgroundColor);

      TextBlock text = new TextBlock();
      text.FontSize = properties.TooltipFontSize;
      text.FontWeight = properties.TooltipFontWeight;
      text.FontStyle = properties.TooltipFontStyle;
      text.FontFamily = properties.TooltipFontFamily;
      text.Background = new SolidColorBrush(properties.TooltipBackgroundColor);
      text.Foreground = new SolidColorBrush(properties.TooltipTextColor);
      text.Margin = new Thickness(2, 2, 2, 2);

      border.Child = text;
      popup.Child = border;

      return popup;
    }
  }
}
