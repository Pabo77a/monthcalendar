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
  internal class Calendar : BaseControl
  {

    #region private members

    ItemsControl itemsControl;
    MonthCalendarSelectionMode selectionMode = MonthCalendarSelectionMode.Single;

    #endregion

    #region constructor

    static Calendar()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));

    }

    #endregion

    #region events

    internal event EventHandler<CalendarSelectionChangedEventArgs> SelectionChanged;

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


      this.itemsControl = GetTemplateChild("PART_Host") as ItemsControl;
      if (this.itemsControl != null)
      {
        this.itemsControl.MouseUp += Calendar_MouseDown;
        this.itemsControl.MouseDoubleClick += ItemsControl_MouseDoubleClick;
      }
    }

    private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      var day = GetDay(e.GetPosition(this));
      Debug.WriteLine("DoubleClick : " + string.Format(day.Date.ToString()));
    }

    private void Calendar_MouseDown(object sender, MouseButtonEventArgs e)
    {
      var day = GetDay(e.GetPosition(this));

      if (e.ClickCount > 1)
      {
        Debug.WriteLine("DoubleClick : " + string.Format(day.Date.ToString()));
      }
      else
      {
        SelectDay(day);
        Debug.WriteLine("Click : " + string.Format(day.Date.ToString()));
      }
    }

    #endregion


    #region private methods


    private void SelectDay(CalendarDay day)
    {

      var ctrl = Keyboard.Modifiers == ModifierKeys.Control;

      var oldSelection = this.Days.Where(x => x.Selected).ToList<DayItem>();
      if (this.selectionMode > MonthCalendarSelectionMode.None)
      {
        if ((this.selectionMode == MonthCalendarSelectionMode.Extended && ctrl) ||
            (this.selectionMode < MonthCalendarSelectionMode.Extended))
        {
          day.Selected = !day.Selected;
        }
        if (day.Selected && this.selectionMode == MonthCalendarSelectionMode.Single)
        {
          ClearCalendar(false);
        }
        foreach (CalendarDay sel in this.Days.Where(x => x.Selected))
        {
          this.SetupSelectedBorders(sel);
        }
      }

      var newSelection = this.Days.Where(x => x.Selected).ToList<DayItem>();
      this.OnSelectionChanged(new CalendarSelectionChangedEventArgs(newSelection, oldSelection));

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

    private CalendarDay GetDay(Point pt)
    {

      var itemWidth = this.ActualWidth / 7;
      var itemHeight = this.ActualHeight / 6;

      int x = (int)(pt.X / itemWidth);
      int y = (int)(pt.Y / itemHeight);

      var day = x + (y * 6) + y;

      return this.Days[day];
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
    }

    private void OnSelectionChanged(CalendarSelectionChangedEventArgs e)
    {
      EventHandler<CalendarSelectionChangedEventArgs> handler = SelectionChanged;
      handler?.Invoke(this, e);
    }

    #endregion

    #region properties

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
      }
    }

    internal MonthCalendarSelectionMode SelectionMode
    {
      get => SelectionMode;
      set
      {
        if (value != this.selectionMode)
        {
          if ((value > MonthCalendarSelectionMode.Single || value == MonthCalendarSelectionMode.None) &&
              (this.Days.Where(x => x.Selected).ToList().Count > 1))
          {
            var oldSelection = this.Days.Where(x => x.Selected).ToList<DayItem>();
            ClearCalendar();
            this.OnSelectionChanged(new CalendarSelectionChangedEventArgs(new List<DayItem>(), oldSelection));

          }
          this.selectionMode = value;
        }
      }
    }

    #endregion


    #region methods

    internal void SetupDays(int year, int month, List<DayItem> items)
    {

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

        if (days[i].Date.Month == month)
        {
          days[i].DateColor = Properties.DateColor;
          days[i].BackgroundColor = Properties.BackgroundColor;
        }
        else
        {
          days[i].DateColor = Properties.TrailingDateColor;
          days[i].BackgroundColor = Properties.TrailingBackgroundColor;
        }

        days[i].DateFontFamily = Properties.DateFontFamily;
        days[i].DateFontSize = Properties.DateFontSize;
        days[i].DateFontStyle = Properties.DateFontStyle;
        days[i].DateFontWeight = Properties.DateFontWeight;
        days[i].DateTextDecoration = Properties.DateTextDecoration;

        days[i].TextFontFamily = Properties.TextFontFamily;
        days[i].TextFontSize = Properties.TextFontSize;
        days[i].TextFontStyle = Properties.TextFontStyle;
        days[i].TextFontWeight = Properties.TextFontWeight;
        days[i].TextTextDecoration = Properties.TextTextDecoration;

      }

      foreach (DayItem item in items)
      {
        var day = days.FirstOrDefault(x => x.Date == item.Date);
        if (day != null)
        {
          Utils.CopyProperties<CalendarProperties, CalendarDay>(Properties, day);
          Utils.CopyProperties<DayItem, CalendarDay>(item, day);
        }
      }

      this.Days = days.ToList<CalendarDay>();

    }

  }

  #endregion
}
