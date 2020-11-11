using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.Properties;
using Pabo.MonthCalendar.Common;
using Pabo.MonthCalendar.EventArgs;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pabo.MonthCalendar
{
  [ToolboxItem(false)]
  internal class Weekdays : PanelControl
  {

    #region dependency properties

    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(List<CalendarWeekday>),
               typeof(Weekdays),
               new FrameworkPropertyMetadata(new List<CalendarWeekday>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(WeekdaysProperties),
               typeof(Weekdays),
               new FrameworkPropertyMetadata(new WeekdaysProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion


    public Weekdays() : base(7,1)
    { }

    private ItemsControl itemsControl;
    private CalendarWeekday activeWeekday = null;

    #region constructor

    static Weekdays()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Weekdays), new FrameworkPropertyMetadata(typeof(Weekdays)));

    }

    #endregion

    #region events

    internal event EventHandler<CalendarWeekdayEventArgs> WeekdayLeave;

    internal event EventHandler<CalendarWeekdayEventArgs> WeekdayEnter;

    #endregion


    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.itemsControl = GetTemplateChild("PART_Host") as ItemsControl;
      if (this.itemsControl != null)
      {
        this.itemsControl.MouseMove += ItemsControl_MouseMove;
        this.itemsControl.MouseEnter += ItemsControl_MouseEnter;
        this.itemsControl.MouseLeave += ItemsControl_MouseLeave;
      }


      Setup();
    }

   

    #endregion


    #region properties

    internal List<CalendarWeekday> Days
    {
      get
      {
        return (List<CalendarWeekday>)this.GetValue(DaysProperty);
      }
      set
      {
        this.SetValue(DaysProperty, value);
      }
    }

    internal WeekdaysProperties Properties
    {
      get
      {
        return (WeekdaysProperties)this.GetValue(PropertiesProperty);
      }
      set
      {
        this.SetValue(PropertiesProperty, value);
        Setup();
      }
    }

    #endregion

    internal void SetupDays(int year, int month, List<Weekday> items)
    {

      List<CalendarWeekday> days = new List<CalendarWeekday>();

      CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
      DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

      for (int i = (int)firstDayOfWeek; i <= (int)DayOfWeek.Saturday; i++)
      {
        days.Add(new CalendarWeekday((DayOfWeek)Enum.Parse(typeof(DayOfWeek), i.ToString())));
      }
      if (firstDayOfWeek == DayOfWeek.Monday)
      {
        days.Add(new CalendarWeekday(DayOfWeek.Sunday));
      }

      for (int i = 0; i<7;i++)
      {
        Utils.CopyProperties<WeekdaysProperties, CalendarWeekday>(Properties, days[i]);
        days[i].Year = year;
        days[i].Month = month;
        var item = items.FirstOrDefault(x => x.Year == year && x.Month == month && x.DayOfWeek == days[i].DayOfWeek);
        if (item != null)
        {
          Utils.CopyProperties<Weekday, CalendarWeekday>(item, days[i]);
        }
      }

      this.Days = days.ToList<CalendarWeekday>();
    }

    private void Setup()
    {
      this.Height = this.Properties.TextFontSize + 20;
    }

    private void OnWeekdayLeave(CalendarWeekdayEventArgs e)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayLeave;
      handler?.Invoke(this, e);
    }

    private void OnWeekdayEnter(CalendarWeekdayEventArgs e)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayEnter;
      handler?.Invoke(this, e);
    }


    #region event handlers

    private void ItemsControl_MouseLeave(object sender, MouseEventArgs e)
    {
      if (this.activeWeekday != null)
      {
        this.activeWeekday.MouseOver = false;
        this.OnWeekdayLeave(new CalendarWeekdayEventArgs(this.activeWeekday));
      }
      this.activeWeekday = null;
    }

    private void ItemsControl_MouseEnter(object sender, MouseEventArgs e)
    {
      var weekday = this.Days[GetPanel(e.GetPosition(this))];
      this.activeWeekday = weekday;
      this.activeWeekday.MouseOver = true;
      this.OnWeekdayEnter(new CalendarWeekdayEventArgs(weekday));

    }

    private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
    {
      var weekday = this.Days[GetPanel(e.GetPosition(this))];
      if (this.activeWeekday != weekday)
      {
        this.activeWeekday.MouseOver = false;
        this.OnWeekdayLeave(new CalendarWeekdayEventArgs(this.activeWeekday));
        this.activeWeekday = weekday;
        this.activeWeekday.MouseOver = true;
        this.OnWeekdayEnter(new CalendarWeekdayEventArgs(this.activeWeekday));
      }
    }

   

    #endregion
  }
}
