using Pabo.MonthCalendar.Common;
using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Pabo.MonthCalendar
{
  public abstract class ItemsControl<T> : BaseControl where T : IPanel
  {

    protected Popup popup;
    protected bool mouseDown = false;


    private ItemsControl itemsControl;
    private MonthCalendarSelectionMode selectionMode = MonthCalendarSelectionMode.None;

    protected T activeItem;
    protected T clickItem;

    protected bool suspendLayout = false;

    private Pos startPos = new Pos();
    private Pos endPos = new Pos();

    public ItemsControl(int cols, int rows)
    {
      this.Cols = cols;
      this.Rows = rows;
    
      this.MouseMove += ItemsControl_MouseMove;
    }
   
    public int Cols { get; private set; }

    public int Rows { get; private set; }

    public List<T> Items { get; set; }

    protected Pos StartPos
    {
      get => startPos;
      set
      {
        if (value != startPos)
        {
          startPos = value;
        }
      }
    }

    protected Pos EndPos
    {
      get => endPos;
      set
      {
        if (value.Col != endPos.Col || value.Row != endPos.Row)
        {
          endPos = value;

          for (int i = 0; i < this.Rows * this.Cols; i++)
          {
            this.Items[i].MouseOver = false;
          }

          for (int c = Math.Min(StartPos.Col, EndPos.Col); c <= Math.Max(EndPos.Col, StartPos.Col); c++)
          {
            for (int r = Math.Min(StartPos.Row, EndPos.Row); r <= Math.Max(EndPos.Row, StartPos.Row); r++)
            {
              var y = ((r - 1) * this.Cols) + c - 1;
              this.Items[y].MouseOver = true;
            }
          }
        }
      }
    }

    internal bool SuspendLayout
    {
      get => this.suspendLayout;
      set
      {
        if (value != this.suspendLayout)
        {
          this.suspendLayout = value;
          if (!this.suspendLayout)
          {
            this.Setup();
          }
        }
      }
    }

    internal MonthCalendarSelectionMode SelectionMode
    {
      get => selectionMode;
      set
      {
        if (value != this.selectionMode)
        {
          if ((value > MonthCalendarSelectionMode.Single || value == MonthCalendarSelectionMode.None) &&
              (this.Items.Where(x => x.Selected).ToList().Count > 1))
          {
            ClearItems();


          }
          this.selectionMode = value;
        }
      }
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.itemsControl = GetTemplateChild("PART_Host") as System.Windows.Controls.ItemsControl;
      if (this.itemsControl != null)
      {
        this.itemsControl.MouseDown += ItemsControl_MouseDown;
        this.itemsControl.MouseMove += ItemsControl_MouseMove;
        this.itemsControl.MouseUp += ItemsControl_MouseUp;
        this.itemsControl.MouseEnter += ItemsControl_MouseEnter;
        this.itemsControl.MouseLeave += ItemsControl_MouseLeave;
        this.itemsControl.MouseDoubleClick += ItemsControl_MouseDoubleClick;
      }
    }
  
    private void ItemsControl_MouseLeave(object sender, MouseEventArgs e)
    {
      if (this.activeItem != null)
      {
        this.activeItem.MouseOver = false;
        this.OnItemLeave(this.activeItem);
      }
      this.activeItem = default(T);
      this.popup.IsOpen = false;

    }

    private void ItemsControl_MouseEnter(object sender, MouseEventArgs e)
    {
      var item = this.Items[GetItem(e.GetPosition(this))];

      this.activeItem = item;
      this.activeItem.MouseOver = true;
      this.OnItemEnter(item);

    }

    private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
    {
      var item = this.Items[GetItem(e.GetPosition(this))];
      SetTooltip(item.Tooltip);

      if (this.mouseDown && this.SelectionMode > MonthCalendarSelectionMode.Single)
      {
        EndPos = this.GetItemPos(e.GetPosition(this));

      }
      if (this.activeItem != null)
      {
        if (this.activeItem.Id != item.Id)
        {
          if ((!this.mouseDown) || (this.SelectionMode == MonthCalendarSelectionMode.Single))
            this.activeItem.MouseOver = false;
          this.OnItemLeave(this.activeItem);
          this.activeItem = item;
          if ((!this.mouseDown) || (this.SelectionMode == MonthCalendarSelectionMode.Single))
            this.activeItem.MouseOver = true;
          this.OnItemEnter(this.activeItem);
        }
      }

      if (popup != null)
      {
        popup.PlacementTarget = sender as UIElement; ;
        popup.VerticalOffset = e.GetPosition(this).Y + 16;
        popup.HorizontalOffset = e.GetPosition(this).X + 16;
        popup.Placement = PlacementMode.Relative;
      }
    }

    private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      this.Button_DoubleClick(sender, e);
      var item = this.Items[GetItem(e.GetPosition(this))];
      this.OnItemDoubleClick(item);
    }

    private void ItemsControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
      if (this.mouseDown)
      {
        this.clickItem = this.Items[GetItem(e.GetPosition(this))];

        var selectedDays = this.Items.Where(x => x.Selected).ToList();
        SelectDays(this.Items.Where(x => x.MouseOver).ToList(), this.clickItem);
      }
      this.mouseDown = false;
    }

    private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
      StartPos = GetItemPos(e.GetPosition(this));

      this.mouseDown = true;
      this.Button_Click(sender, e);
    }

    private void SelectDays(List<T> items, T activeItem)
    {

      var selected = this.Items.Where(x => x.Selected).ToList();
      var mouseOver = this.Items.Where(x => x.MouseOver).ToList();
      if (SelectionMode > MonthCalendarSelectionMode.None)
      {
        if (SelectionMode == MonthCalendarSelectionMode.Single)
        {
          foreach (T item in selected)
          {
            if (item.Id != activeItem.Id)
            {
              item.Selected = false;
            }
          }
          activeItem.Selected = activeItem.Selected ? false : true;
        }
        if (SelectionMode > MonthCalendarSelectionMode.Single)
        {
          foreach (T item in items)
          {
            item.Selected = item.Id == activeItem.Id && activeItem.Selected && mouseOver.Count() == 1 ? false : true;
            item.MouseOver = false;
          }
        }
        activeItem.MouseOver = true;
        foreach (T sel in this.Items.Where(x => x.Selected))
        {
          this.SetupSelectedBorders(sel);
        }
        this.OnSelectionChanged();
      }
    }


    protected Pos GetItemPos(Point pt)
    {
      var itemWidth = this.ActualWidth / this.Cols;
      var itemHeight = this.ActualHeight / this.Rows;

      var row = (pt.Y / itemHeight) + 1;
      var col = (pt.X / itemWidth) + 1;

      return new Pos((int)col, (int)row);
    }


    protected int GetItem(Point pt)
    {

      var itemWidth = this.ActualWidth / this.Cols;
      var itemHeight = this.ActualHeight / this.Rows;

      int x = (int)(pt.X / itemWidth);
      int y = (int)(pt.Y / itemHeight);

      var panel = Cols > 1 ?
        y * this.Cols + x :
        x + y;


      return panel;
    }

    protected void SetupSelectedBorders(T item)
    {
      var index = this.Items.FindIndex(x => x.Id == item.Id);
      List<string> rightMost = new List<string>();
      var t = -1;
      for (int i =0;i< this.Rows; i++)
      {
        t += this.Cols;
        rightMost.Add(t.ToString());
      }
      if (item.Selected)
      {
        var left = (index == 0) || (index % this.Cols == 0) || !this.Items[index - 1].Selected ? 1 : 0;
        var right = rightMost.IndexOf(index.ToString()) == 0 || index == (this.Cols * this.Rows) - 1 || !this.Items[index + 1].Selected ? 1 : 0;
        var top = (index <= this.Cols-1) || !this.Items[index - this.Cols].Selected ? 1 : 0;
        var bottom = (index > this.Cols * (this.Rows-1)) || index + this.Cols > (this.Cols * this.Rows)-1 || !this.Items[index + this.Cols].Selected ? 1 : 0;

        item.BorderThickness = new Thickness(left, top, right, bottom);
      }
    }

    private void ClearItems(bool setupBorders = true)
    {
      foreach (T item in this.Items)
      {
        item.Selected = false;
        if (setupBorders)
        {
          this.SetupSelectedBorders(item);
        }
      }

      this.OnSelectionChanged();
    }

    protected abstract void Setup();

    protected abstract void OnSelectionChanged();

    protected abstract void OnItemLeave(T item);
    protected abstract void OnItemEnter(T item);

    protected abstract void OnItemDoubleClick(T item);

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
