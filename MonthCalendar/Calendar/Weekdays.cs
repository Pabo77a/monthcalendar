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
using System.Threading;
using System.Windows.Media;

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Host", Type = typeof(System.Windows.Controls.ItemsControl))]
  [ToolboxItem(false)]
  internal class Weekdays : ItemsControl
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


    public Weekdays() : base(7, 1)
    {

      this.Click += (sender, e) =>
      {
        this.OnWeekdayClick(new CalendarWeekdayEventArgs(clickWeekday));
      };
    }

    private System.Windows.Controls.ItemsControl itemsControl;
    private CalendarWeekday activeWeekday = null;
    private CalendarWeekday clickWeekday;
    private bool suspendLayout = false;
    private int year;
    private int month;
    private List<Weekday> weekdayItems;
    private DataTemplate template;

    #region constructor

    static Weekdays()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Weekdays), new FrameworkPropertyMetadata(typeof(Weekdays)));

    }

    #endregion

    #region events

    internal event EventHandler<CalendarWeekdayEventArgs> WeekdayLeave;

    internal event EventHandler<CalendarWeekdayEventArgs> WeekdayEnter;

    internal event EventHandler<CalendarWeekdayEventArgs> WeekdayClick;

    internal event EventHandler<CalendarWeekdayEventArgs> WeekdayDoubleClick;

    #endregion


    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.itemsControl = GetTemplateChild("PART_Host") as System.Windows.Controls.ItemsControl;
      if (this.itemsControl != null)
      {
        this.itemsControl.MouseMove += ItemsControl_MouseMove;
        this.itemsControl.MouseEnter += ItemsControl_MouseEnter;
        this.itemsControl.MouseLeave += ItemsControl_MouseLeave;
        this.itemsControl.MouseDown += ItemsControl_MouseDown;
        this.itemsControl.MouseDoubleClick += ItemsControl_MouseDoubleClick;

        this.popup = CreatePopup(this.Properties);
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
        if (value != null)
        {
          value.PropertyChanged -= PropertiesChanged;
          value.PropertyChanged += PropertiesChanged;
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

    #endregion

    internal void SetupDays(int year, int month, List<Weekday> items, DataTemplate template)
    {
      this.year = year;
      this.month = month;
      this.weekdayItems = items;
      this.template = template;

      if (!SuspendLayout)
      {

        CultureInfo ci = Thread.CurrentThread.CurrentCulture;

        List<CalendarWeekday> days = new List<CalendarWeekday>();

        DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

        for (int i = (int)firstDayOfWeek; i <= (int)DayOfWeek.Saturday; i++)
        {
          days.Add(new CalendarWeekday((DayOfWeek)Enum.Parse(typeof(DayOfWeek), i.ToString())));
        }
        if (firstDayOfWeek == DayOfWeek.Monday)
        {
          days.Add(new CalendarWeekday(DayOfWeek.Sunday));
        }

        for (int i = 0; i < 7; i++)
        {
          days[i].Template = this.template;
          Utils.CopyProperties<WeekdaysProperties, CalendarWeekday>(Properties, days[i]);
          days[i].Year = year;
          days[i].Month = month;
          days[i].Name = Properties.AbbreviatedNames
            ? ci.DateTimeFormat.GetAbbreviatedDayName(days[i].DayOfWeek)
            : ci.DateTimeFormat.GetDayName(days[i].DayOfWeek);

          var item = items.FirstOrDefault(x => x.Year == year && x.Month == month && x.DayOfWeek == days[i].DayOfWeek);
          if (item != null)
          {
            Utils.CopyProperties<Weekday, CalendarWeekday>(item, days[i]);
          }
        }

        this.Days = days.ToList<CalendarWeekday>();
      }
    }

    private void Setup()
    {
      this.Height = this.Properties.TextFontSize + 20;
      //SetupDays(this.year, this.month, this.weekdayItems, this.template);
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

    private void OnWeekdayClick(CalendarWeekdayEventArgs e)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayClick;
      handler?.Invoke(this, e);
    }

    private void OnWeekdayDoubleClick(CalendarWeekdayEventArgs e)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayDoubleClick;
      handler?.Invoke(this, e);
    }


    #region event handlers

    private void PropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
    }

    private void ItemsControl_MouseLeave(object sender, MouseEventArgs e)
    {
      if (this.activeWeekday != null)
      {
        this.activeWeekday.MouseOver = false;
        this.OnWeekdayLeave(new CalendarWeekdayEventArgs(this.activeWeekday));
      }
      this.activeWeekday = null;
      this.popup.IsOpen = false;
    }

    private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      this.Button_DoubleClick(sender, e);
      var day = this.Days[GetItem(e.GetPosition(this))];
      this.OnWeekdayDoubleClick(new CalendarWeekdayEventArgs(day));
    }

    private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
      this.clickWeekday = this.Days[GetItem(e.GetPosition(this))];
      this.Button_Click(sender, e);
    }

    private void ItemsControl_MouseEnter(object sender, MouseEventArgs e)
    {
      var weekday = this.Days[GetItem(e.GetPosition(this))];
      this.activeWeekday = weekday;
      this.activeWeekday.MouseOver = true;
      this.OnWeekdayEnter(new CalendarWeekdayEventArgs(weekday));

    }

    private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
    {
      var weekday = this.Days[GetItem(e.GetPosition(this))];
      SetTooltip(weekday.Tooltip);
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
