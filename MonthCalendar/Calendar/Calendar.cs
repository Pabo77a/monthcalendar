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

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Panel", Type = typeof(CalendarWrapPanel))]
  [ToolboxItem(false)]
  public class Calendar : ItemsControl
  {

    public Calendar() : base(7, 6)
    {
      this.Click += (sender, e) =>
      {
        this.OnDayClick(new CalendarDayEventArgs(clickDay));
      };
    }

    #region private members

    private System.Windows.Controls.ItemsControl itemsControl;
    private MonthCalendarSelectionMode selectionMode = MonthCalendarSelectionMode.Single;

    private CalendarDay activeDay;
    private CalendarDay clickDay;

    private bool suspendLayout = false;
    private int year;
    private int month;
    private List<Day> dayItems;
    private List<DateTime> disabledDays;
    private DateTime minDate;
    private DateTime maxDate;

    private bool mouseDown = false;
    private Pos startPos = new Pos();
    private Pos endPos = new Pos();
    private List<Day> prevSelected = new List<Day>();

    private DataTemplate template;

    #endregion



    #region constructor

    static Calendar()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));

    }

    #endregion

    #region events

    internal event EventHandler<CalendarSelectionChangedEventArgs> SelectionChanged;

    internal event EventHandler<CalendarDayEventArgs> DayLeave;

    internal event EventHandler<CalendarDayEventArgs> DayEnter;

    internal event EventHandler<CalendarDayEventArgs> DayClick;

    internal event EventHandler<CalendarDayEventArgs> DayDoubleClick;


    #endregion

    #region dependency properties

    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(List<CalendarDay>),
               typeof(Calendar),
               new FrameworkPropertyMetadata(new List<CalendarDay>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(CalendarProperties),
               typeof(Calendar),
               new FrameworkPropertyMetadata(new CalendarProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


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
      }
    }



    #endregion

    #region event handlers

    private void ItemsControl_MouseLeave(object sender, MouseEventArgs e)
    {
      if (this.activeDay != null)
      {
        this.activeDay.MouseOver = false;
        this.OnDayLeave(new CalendarDayEventArgs(this.activeDay));
      }
      this.activeDay = null;
    }

    private void ItemsControl_MouseEnter(object sender, MouseEventArgs e)
    {
      var day = this.Days[GetItem(e.GetPosition(this))];
      this.activeDay = day;
      this.activeDay.MouseOver = true;
      this.OnDayEnter(new CalendarDayEventArgs(day));

    }

    private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
    {

      var day = this.Days[GetItem(e.GetPosition(this))];

      if (this.mouseDown && this.SelectionMode > MonthCalendarSelectionMode.Single)
      {
        EndPos = this.GetItemPos(e.GetPosition(this));

      }
      if (this.activeDay != day)
      {
        if ((!this.mouseDown) || (this.SelectionMode == MonthCalendarSelectionMode.Single))
          this.activeDay.MouseOver = false;
        this.OnDayLeave(new CalendarDayEventArgs(this.activeDay));
        this.activeDay = day;
        if ((!this.mouseDown) || (this.SelectionMode == MonthCalendarSelectionMode.Single))
          this.activeDay.MouseOver = true;
        this.OnDayEnter(new CalendarDayEventArgs(this.activeDay));
      }
    }

    private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      this.Button_DoubleClick(sender, e);
      var day = this.Days[GetItem(e.GetPosition(this))];
      this.OnDayDoubleClick(new CalendarDayEventArgs(day));
    }

    private void ItemsControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
      if (this.mouseDown)
      {
        this.clickDay = this.Days[GetItem(e.GetPosition(this))];

        var selectedDays = this.Days.Where(x => x.Selected).ToList();
        SelectDays(this.Days.Where(x => x.MouseOver).ToList(), this.clickDay);
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

    private void SelectDays(List<CalendarDay> days, CalendarDay activeDay)
    {

      var selected = this.Days.Where(x => x.Selected).ToList();
      if (SelectionMode > MonthCalendarSelectionMode.None)
      {
        if (SelectionMode == MonthCalendarSelectionMode.Single)
        {
          foreach (CalendarDay day in selected)
          {
            if (day != activeDay)
            {
              day.Selected = false;
            }
          }
          activeDay.Selected = activeDay.Selected ? false : true;
        }
        if (SelectionMode > MonthCalendarSelectionMode.Single)
        {
          foreach (CalendarDay day in days)
          {
            day.Selected = day == activeDay && activeDay.Selected ? false : true;
            day.MouseOver = false;
          }
        }
        activeDay.MouseOver = true;
        foreach (CalendarDay sel in this.Days.Where(x => x.Selected))
        {
          this.SetupSelectedBorders(sel);
        }
        this.OnSelectionChanged();
      }
    }


    private void SetupSelectedBorders(CalendarDay day)
    {
      var index = this.Days.FindIndex(x => x.Date == day.Date);
      List<string> rightMost = new List<string> { "6", "13", "20", "27", "34", "41" };
      if (day.Selected)
      {
        var left = (index == 0) || (index % 7 == 0) || !this.Days[index - 1].Selected ? 1 : 0;
        var right = rightMost.IndexOf(index.ToString()) == 0 || index == 41 || !this.Days[index + 1].Selected ? 1 : 0;
        var top = (index <= 7) || !this.Days[index - 7].Selected ? 1 : 0;
        var bottom = (index >= 36) || index + 7 > 41 || !this.Days[index + 7].Selected ? 1 : 0;

        day.BorderThickness = new Thickness(left, top, right, bottom);
      }
    }

    private void ClearCalendar(bool setupBorders = true)
    {
      foreach (CalendarDay day in this.Days)
      {
        day.Selected = false;
        if (setupBorders)
        {
          this.SetupSelectedBorders(day);
        }
      }

      this.OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
      var selectedDays = this.Days.Where(x => x.Selected).ToList<Day>();

      var diff1 = selectedDays.Except(this.prevSelected).ToList();
      var diff2 = this.prevSelected.Except(selectedDays).ToList();
      if (diff1.Count() > 0 || diff2.Count > 0)
      {
        this.prevSelected = selectedDays;
        foreach (CalendarDay day in selectedDays) { this.SetupSelectedBorders(day); };
        EventHandler<CalendarSelectionChangedEventArgs> handler = SelectionChanged;
        handler?.Invoke(this, new CalendarSelectionChangedEventArgs(selectedDays));
      }
    }

    private void OnDayLeave(CalendarDayEventArgs e)
    {
      EventHandler<CalendarDayEventArgs> handler = DayLeave;
      handler?.Invoke(this, e);
    }

    private void OnDayEnter(CalendarDayEventArgs e)
    {
      EventHandler<CalendarDayEventArgs> handler = DayEnter;
      handler?.Invoke(this, e);
    }

    private void OnDayClick(CalendarDayEventArgs e)
    {
      EventHandler<CalendarDayEventArgs> handler = DayClick;
      handler?.Invoke(this, e);

    }

    private void OnDayDoubleClick(CalendarDayEventArgs e)
    {
      EventHandler<CalendarDayEventArgs> handler = DayDoubleClick;
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

          for (int i = 0; i < 42; i++)
          {
            this.Days[i].MouseOver = false;
          }

          for (int c = Math.Min(StartPos.Col, EndPos.Col); c <= Math.Max(EndPos.Col, StartPos.Col); c++)
          {
            for (int r = Math.Min(StartPos.Row, EndPos.Row); r <= Math.Max(EndPos.Row, StartPos.Row); r++)
            {
              var y = ((r - 1) * 7) + c - 1;
              this.Days[y].MouseOver = true;
            }
          }
        }
      }
    }

    internal List<CalendarDay> Days
    {
      get
      {
        return (List<CalendarDay>)this.GetValue(DaysProperty);
      }
      set
      {
        this.SetValue(DaysProperty, value);
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
    internal CalendarProperties Properties
    {
      get
      {
        return (CalendarProperties)this.GetValue(PropertiesProperty);
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
              (this.Days.Where(x => x.Selected).ToList().Count > 1))
          {
            ClearCalendar();


          }
          this.selectionMode = value;
        }
      }
    }

    #endregion


    #region public methods

    public void Select(List<DateTime> dates)
    {
      foreach (DateTime date in dates)
      {
        var day = this.Days.FirstOrDefault(x => x.Date == date);
        if (day != null)
        {
          day.Selected = true;
        }
      }

      this.OnSelectionChanged();
    }

    public void Deselect(List<DateTime> dates)
    {
      foreach (DateTime date in dates)
      {
        var day = this.Days.FirstOrDefault(x => x.Date == date);
        if (day != null)
        {
          day.Selected = false;
        }
      }

      this.OnSelectionChanged();
    }

    public void SelectWeek(int week)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (ISOWeek.GetWeekOfYear(day.Date) == week)
        {
          day.Selected = true;
        }
      }

      this.OnSelectionChanged();
    }

    public void DeselectWeek(int week)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (ISOWeek.GetWeekOfYear(day.Date) == week)
        {
          day.Selected = false;
        }
      }

      this.OnSelectionChanged();
    }

    public void SelectWeekday(DayOfWeek weekday)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (day.Date.DayOfWeek == weekday)
        {
          day.Selected = true;
        }
      }

      this.OnSelectionChanged();
    }

    public void DeselectWeekday(DayOfWeek weekday)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (day.Date.DayOfWeek == weekday)
        {
          day.Selected = false;
        }
      }

      this.OnSelectionChanged();
    }

    #endregion

    #region private methods

    private void Setup()
    {
      SetupDays(this.year, this.month, this.minDate, this.maxDate, this.dayItems, this.disabledDays, this.template);
    }

    internal void SetupDays(int year, int month, DateTime minDate, DateTime maxDate, List<Day> items, List<DateTime> disabledDays, DataTemplate template)
    {

      this.year = year;
      this.month = month;
      this.dayItems = items;
      this.minDate = minDate;
      this.maxDate = maxDate;
      this.disabledDays = disabledDays;
      this.template = template;

      if (!SuspendLayout)
      {
        var firstDay = new DateTime(year, month, 1);
        items = items.Where(x => x.Date > firstDay.AddDays(-15) && x.Date < firstDay.AddDays(45)).ToList();

        CalendarDay[] days = new CalendarDay[42];

        CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
        DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

        var firstDayInMonth = new DateTime(year, month, 1);
        DayOfWeek firstDayOfMonth = firstDayInMonth.DayOfWeek;

        var startPos = (int)firstDayOfMonth + (1 - (int)firstDayOfWeek) - 1;
        if (startPos == -1)
        {
          startPos = 6;
        }
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var endPos = startPos + daysInMonth;

        var date = firstDayInMonth;
        for (int i = startPos; i < endPos; i++)
        {
          days[i] = new CalendarDay(date);
          date = date.AddDays(1);
        }

        date = firstDayInMonth.AddDays(-1);
        for (int i = startPos - 1; i >= 0; i--)
        {
          days[i] = new CalendarDay(date);
          date = date.AddDays(-1);
        }

        date = firstDayInMonth.AddDays(daysInMonth);
        for (int i = endPos; i < 42; i++)
        {
          days[i] = new CalendarDay(date);
          date = date.AddDays(1);
        }

        for (int i = 0; i < 42; i++)
        {

          var disabled = this.disabledDays.FirstOrDefault(x => x == days[i].Date);
          var selected = this.Days.FirstOrDefault(x => x.Selected && x.Date == days[i].Date);

          days[i].Template = this.template;

          days[i].Selected = this.Days.FirstOrDefault(x => x.Selected && x.Date == days[i].Date) != null;
          days[i].Disabled = days[i].Date <= minDate || days[i].Date >= maxDate || disabled != default(DateTime);
          days[i].Trailing = days[i].Date.Month != month;
          if (!days[i].Disabled)
          {
     
            days[i].DateColor = days[i].Trailing ? Properties.TrailingDateColor : Properties.DateColor;
            days[i].BackgroundColor = Properties.BackgroundImage != null ?
                    Colors.Transparent :
                    days[i].Trailing ? Properties.TrailingBackgroundColor : Properties.BackgroundColor;
          }

          days[i].DateFontFamily = Properties.DateFontFamily;
          days[i].DateFontSize = Properties.DateFontSize;
          days[i].DateFontStyle = Properties.DateFontStyle;
          days[i].DateFontWeight = Properties.DateFontWeight;
          days[i].DateTextDecoration = Properties.DateTextDecoration;
          days[i].DateMargin = Properties.DateMargin;
          days[i].DateVerticalAlignment = Properties.DateVerticalAlignment;
          days[i].DateHorizontalAlignment = Properties.DateHorizontalAlignment;

          days[i].ImageMargin = Properties.ImageMargin;
          days[i].ImageVerticalAlignment = Properties.ImageVerticalAlignment;
          days[i].ImageHorizontalAlignment = Properties.ImageHorizontalAlignment;


          days[i].TextFontFamily = Properties.TextFontFamily;
          days[i].TextFontSize = Properties.TextFontSize;
          days[i].TextFontStyle = Properties.TextFontStyle;
          days[i].TextFontWeight = Properties.TextFontWeight;
          days[i].TextTextDecoration = Properties.TextTextDecoration;
          days[i].TextMargin = Properties.TextMargin;
          days[i].TextVerticalAlignment = Properties.TextVerticalAlignment;
          days[i].TextHorizontalAlignment = Properties.TextHorizontalAlignment;

          if (days[i].Selected)
            this.SetupSelectedBorders(days[i]);
        }

        foreach (Day item in items)
        {
          var day = days.FirstOrDefault(x => x.Date == item.Date);
          if (day != null)
          {
            Utils.CopyProperties<CalendarProperties, CalendarDay>(Properties, day);
            Utils.CopyProperties<Day, CalendarDay>(item, day);
          }
        }

        this.Days = days.ToList<CalendarDay>();
      }

    }

  }

  #endregion
}
