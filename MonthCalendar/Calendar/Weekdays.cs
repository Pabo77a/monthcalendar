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
  internal class Weekdays : ItemsControl<CalendarWeekday>
  {

    #region dependency properties

    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(List<CalendarWeekday>),
               typeof(Weekdays),
               new FrameworkPropertyMetadata(new List<CalendarWeekday>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDaysChanged));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(WeekdaysProperties),
               typeof(Weekdays),
               new FrameworkPropertyMetadata(new WeekdaysProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion


    public Weekdays() : base(7, 1)
    {
      this.popup = CreatePopup(this.Properties);
      this.Click += (sender, e) =>
      {
        this.OnWeekdayClick(new CalendarWeekdayEventArgs(clickItem));
      };
    }

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


    #endregion

    private static void OnDaysChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      Weekdays weekdays = d as Weekdays;
      if (weekdays != null)
        weekdays.OnDaysChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnDaysChanged(object newValue, object oldValue)
    {
      base.Items = (List<CalendarWeekday>)newValue;
    }


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

    protected override void Setup()
    {
      this.Height = this.Properties.TextFontSize + 20;
      SetupDays(this.year, this.month, this.weekdayItems, this.template);
    }

    protected override void OnSelectionChanged(List<CalendarWeekday> selected)
    {

    }

    protected override void OnItemLeave(CalendarWeekday item)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayLeave;
      handler?.Invoke(this, new CalendarWeekdayEventArgs(item));

    }
    protected override void OnItemEnter(CalendarWeekday item)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayEnter;
      handler?.Invoke(this, new CalendarWeekdayEventArgs(item));
    }

    protected override void OnItemDoubleClick(CalendarWeekday item)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayDoubleClick;
      handler?.Invoke(this, new CalendarWeekdayEventArgs(item));
    }


    private void OnWeekdayClick(CalendarWeekdayEventArgs e)
    {
      EventHandler<CalendarWeekdayEventArgs> handler = WeekdayClick;
      handler?.Invoke(this, e);
    }

    #region event handlers

    private void PropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
    }

 

    #endregion
  }
}
