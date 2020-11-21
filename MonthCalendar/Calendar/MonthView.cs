using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using Pabo.MonthCalendar.Common;
using System.Diagnostics;
using System.Globalization;
using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.Controls;
using Point = System.Windows.Point;
using Pabo.MonthCalendar.EventArgs;
using Pabo.MonthCalendar.Properties;
using System.Windows.Controls.Primitives;

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Host", Type = typeof(System.Windows.Controls.ItemsControl))]
  [ToolboxItem(false)]
  public class MonthView : ItemsControl
  {

    public MonthView() : base(3,4)
    {
      this.Click += (sender, e) =>
      {
        this.OnMonthClick(new CalendarMonthEventArgs(clickMonth));
      };
    }

    #region private members

    private System.Windows.Controls.ItemsControl itemsControl;
    private MonthCalendarSelectionMode selectionMode = MonthCalendarSelectionMode.Single;

    private CalendarMonth activeMonth;
    private CalendarMonth clickMonth;

    private bool suspendLayout = false;
    private int year;

    private List<Month> monthItems;

    private Pos startPos = new Pos();
    private Pos endPos = new Pos();
    private List<Month> prevSelected = new List<Month>();

    private DataTemplate template;

    #endregion

    #region constructor

    static MonthView()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MonthView), new FrameworkPropertyMetadata(typeof(MonthView)));

    }

    #endregion

    #region events

    internal event EventHandler<CalendarSelectionChangedEventArgs> SelectionChanged;

    internal event EventHandler<CalendarMonthEventArgs> MonthLeave;

    internal event EventHandler<CalendarMonthEventArgs> MonthEnter;

    internal event EventHandler<CalendarMonthEventArgs> MonthClick;

    internal event EventHandler<CalendarMonthEventArgs> MonthDoubleClick;


    #endregion

    #region dependency properties

    public static readonly DependencyProperty MonthsProperty = DependencyProperty.Register("Months",
               typeof(List<CalendarMonth>),
               typeof(MonthView),
               new FrameworkPropertyMetadata(new List<CalendarMonth>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(MonthProperties),
               typeof(MonthView),
               new FrameworkPropertyMetadata(new MonthProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    #endregion


    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.itemsControl = GetTemplateChild("PART_Host") as System.Windows.Controls.ItemsControl;
      if (this.itemsControl != null)
      {
        this.itemsControl.MouseDown += Calendar_MouseDown;
        this.itemsControl.MouseMove += ItemsControl_MouseMove;
        this.itemsControl.MouseUp += ItemsControl_MouseUp;
        this.itemsControl.MouseEnter += ItemsControl_MouseEnter;
        this.itemsControl.MouseLeave += ItemsControl_MouseLeave;
        this.itemsControl.MouseDoubleClick += ItemsControl_MouseDoubleClick;

        this.popup = CreatePopup(this.Properties);
      }
    }

    #endregion

    #region event handlers

    private void ItemsControl_MouseLeave(object sender, MouseEventArgs e)
    {
      if (this.activeMonth != null)
      {
        this.activeMonth.MouseOver = false;
        this.OnMonthLeave(new CalendarMonthEventArgs(this.activeMonth));
      }
      this.activeMonth = null;
      this.popup.IsOpen = false;

    }

    private void ItemsControl_MouseEnter(object sender, MouseEventArgs e)
    {
      var month = this.Months[GetItem(e.GetPosition(this))];

      this.activeMonth = month;
      this.activeMonth.MouseOver = true;
      this.OnMonthEnter(new CalendarMonthEventArgs(month));

    }

    private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
    {
      var day = this.Months[GetItem(e.GetPosition(this))];
      SetTooltip(day.Tooltip);

      if (this.mouseDown && this.SelectionMode > MonthCalendarSelectionMode.Single)
      {
        EndPos = this.GetItemPos(e.GetPosition(this));

      }
      if (this.activeMonth != day)
      {
        if ((!this.mouseDown) || (this.SelectionMode == MonthCalendarSelectionMode.Single))
          this.activeMonth.MouseOver = false;
        this.OnMonthLeave(new CalendarMonthEventArgs(this.activeMonth));
        this.activeMonth = day;
        if ((!this.mouseDown) || (this.SelectionMode == MonthCalendarSelectionMode.Single))
          this.activeMonth.MouseOver = true;
        this.OnMonthEnter(new CalendarMonthEventArgs(this.activeMonth));
      }
    }

    private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      this.Button_DoubleClick(sender, e);
      var month = this.Months[GetItem(e.GetPosition(this))];
      this.OnMonthDoubleClick(new CalendarMonthEventArgs(month));
    }

    private void ItemsControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
      if (this.mouseDown)
      {
        this.clickMonth = this.Months[GetItem(e.GetPosition(this))];

        var selectedMonths = this.Months.Where(x => x.Selected).ToList();
        SelectMonths(this.Months.Where(x => x.MouseOver).ToList(), this.clickMonth);
      }
      this.mouseDown = false;
    }


    private void Calendar_MouseDown(object sender, MouseButtonEventArgs e)
    {
      StartPos = GetItemPos(e.GetPosition(this));

      this.mouseDown = true;
      this.Button_Click(sender, e);
    }

    private void PropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
    }


    #endregion


    #region private methods

    private void SelectMonths(List<CalendarMonth> months, CalendarMonth activeMonth)
    {
      var selected = this.Months.Where(x => x.Selected).ToList();
      var mouseOver = this.Months.Where(x => x.MouseOver).ToList();
      if (SelectionMode > MonthCalendarSelectionMode.None)
      {
        if (SelectionMode == MonthCalendarSelectionMode.Single)
        {
          foreach (CalendarMonth month in selected)
          {
            if (month != activeMonth)
            {
              month.Selected = false;
            }
          }
          activeMonth.Selected = activeMonth.Selected ? false : true;
        }
        if (SelectionMode > MonthCalendarSelectionMode.Single)
        {
          foreach (CalendarMonth month in months)
          {
            month.Selected = month == activeMonth && activeMonth.Selected && mouseOver.Count() == 1 ? false : true;
            month.MouseOver = false;
          }
        }
        activeMonth.MouseOver = true;
        foreach (CalendarMonth sel in this.Months.Where(x => x.Selected))
        {
          this.SetupSelectedBorders(sel);
        }
        this.OnSelectionChanged();
      }
    }


    private void SetupSelectedBorders(CalendarMonth month)
    {
        var index = this.Months.FindIndex(x => x.Number == month.Number);
        List<string> rightMost = new List<string> { "2", "5", "8", "11" };
        if (month.Selected)
        {
          var left = (index == 0) || (index % 3 == 0) || !this.Months[index - 1].Selected ? 1 : 0;
          var right = rightMost.IndexOf(index.ToString()) == 0 || index == 11 || !this.Months[index + 1].Selected ? 1 : 0;
          var top = (index <= 3) || !this.Months[index - 3].Selected ? 1 : 0;
          var bottom = (index >= 9) || index + 3 > 41 || !this.Months[index + 3].Selected ? 1 : 0;

          month.BorderThickness = new Thickness(left, top, right, bottom);
        }
    }

    private void ClearCalendar(bool setupBorders = true)
    {
      foreach (CalendarMonth month in this.Months)
      {
        month.Selected = false;
        if (setupBorders)
        {
          this.SetupSelectedBorders(month);
        }
      }

      this.OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
      var selectedMonths = this.Months.Where(x => x.Selected).ToList<Month>();

      var diff1 = selectedMonths.Except(this.prevSelected).ToList();
      var diff2 = this.prevSelected.Except(selectedMonths).ToList();
      if (diff1.Count() > 0 || diff2.Count > 0)
      {
        this.prevSelected = selectedMonths;
        foreach (CalendarMonth month in selectedMonths) { this.SetupSelectedBorders(month); };
        
        //EventHandler<CalendarSelectionChangedEventArgs> handler = SelectionChanged;
        //handler?.Invoke(this, new CalendarSelectionChangedEventArgs(selectedDays));
      }
    }

    private void OnMonthLeave(CalendarMonthEventArgs e)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthLeave;
      handler?.Invoke(this, e);
    }

    private void OnMonthEnter(CalendarMonthEventArgs e)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthEnter;
      handler?.Invoke(this, e);
    }

    private void OnMonthClick(CalendarMonthEventArgs e)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthClick;
      handler?.Invoke(this, e);

    }

    private void OnMonthDoubleClick(CalendarMonthEventArgs e)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthDoubleClick;
      handler?.Invoke(this, e);
    }


    #endregion

    #region properties

    private Pos StartPos
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

    private Pos EndPos
    {
      get => endPos;
      set
      {
        if (value.Col != endPos.Col || value.Row != endPos.Row)
        {
          endPos = value;

          for (int i = 0; i < 12; i++)
          {
            this.Months[i].MouseOver = false;
          }

          for (int c = Math.Min(StartPos.Col, EndPos.Col); c <= Math.Max(EndPos.Col, StartPos.Col); c++)
          {
            for (int r = Math.Min(StartPos.Row, EndPos.Row); r <= Math.Max(EndPos.Row, StartPos.Row); r++)
            {
              var y = ((r - 1) * 3) + c - 1;
              this.Months[y].MouseOver = true;
            }
          }
        }
      }
    }

    internal List<CalendarMonth> Months
    {
      get
      {
        return (List<CalendarMonth>)this.GetValue(MonthsProperty);
      }
      set
      {
        this.SetValue(MonthsProperty, value);
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

    [Description("")]
    [Category("Header")]
    [Browsable(true)]
    internal MonthProperties Properties
    {
      get
      {
        return (MonthProperties)this.GetValue(PropertiesProperty);
      }
      set
      {
        this.SetValue(PropertiesProperty, value);
        if (value != null)
        {
          value.PropertyChanged -= PropertiesChanged;
          value.PropertyChanged += PropertiesChanged;
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
              (this.Months.Where(x => x.Selected).ToList().Count > 1))
          {
            ClearCalendar();


          }
          this.selectionMode = value;
        }
      }
    }

    #endregion


    #region public methods

    #endregion

    #region private methods

    private void Setup()
    {
      SetupMonths(this.year, this.monthItems, this.template);
    }

    internal void SetupMonths(int year, List<Month> items, DataTemplate template)
    {

      this.year = year;
      this.monthItems = items;
      this.template = template;

      if (!SuspendLayout)
      {
        var months = new CalendarMonth[12];

        for (int i = 0;i <12;i++)
        {
          months[i] = new CalendarMonth(year, i+1);

          //var disabled = this.disabledMonths.FirstOrDefault(x => x == days[i].Date);
          //var selected = this.Days.FirstOrDefault(x => x.Selected && x.Date == days[i].Date);

          months[i].Template = this.template;

          months[i].Selected = this.Months.FirstOrDefault(x => x.Selected && x.Year == months[i].Year && x.Number == months[i].Number) != null;
          //months[i].Disabled = disabled; 

          if (months[i].Selected)
            this.SetupSelectedBorders(months[i]);

        }

        foreach (Month item in items)
        {
          var month = months.FirstOrDefault(x => x.Year == item.Year && x.Number == item.Number);
          if (month != null)
          {
            Utils.CopyProperties<MonthProperties, CalendarMonth>(Properties, month);
            Utils.CopyProperties<Month, CalendarMonth>(item, month);
          }
        }

        this.Months = months.ToList();
      }

    }

  }

  #endregion
}
