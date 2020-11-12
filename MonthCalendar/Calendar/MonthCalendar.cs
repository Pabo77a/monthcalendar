using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using Pabo.MonthCalendar.Common;

using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.Properties;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Pabo.MonthCalendar.Controls;
using System.Collections.Specialized;

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Header", Type = typeof(Header))]
  [TemplatePart(Name = "PART_Footer", Type = typeof(Footer))]
  [TemplatePart(Name = "PART_Calendar", Type = typeof(Calendar))]
  [TemplatePart(Name = "PART_Weekdays", Type = typeof(Weekdays))]
  [TemplatePart(Name = "PART_Weeknumbers", Type = typeof(Weeknumbers))]
  [ToolboxItem(true)]
  public class MonthCalendar : BaseControl
  {

    #region private members

    private Header header;
    private Footer footer;
    private Calendar calendar;
    private Weekdays weekdays;
    private Weeknumbers weeknumbers;

    private SelectionMode selectionMode;

    #endregion


    #region dependency properties

    public static readonly DependencyProperty MonthProperty = DependencyProperty.Register("Month",
               typeof(int),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnMonthChanged,
                   OnCoerceMonthChanged, false, UpdateSourceTrigger.PropertyChanged));

    public static readonly DependencyProperty YearProperty = DependencyProperty.Register("Year",
               typeof(int),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnYearChanged,
                   OnCoerceYearChanged, false, UpdateSourceTrigger.PropertyChanged));

    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(TrulyObservableCollection<Day>),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new TrulyObservableCollection<Day>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDaysChanged));

    public static readonly DependencyProperty WeeksProperty = DependencyProperty.Register("Weeks",
               typeof(TrulyObservableCollection<Week>),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new TrulyObservableCollection<Week>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnWeeksChanged));

    public static readonly DependencyProperty DayOfWeekProperty = DependencyProperty.Register("DayOfWeek",
               typeof(TrulyObservableCollection<Weekday>),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new TrulyObservableCollection<Weekday>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDayOfWeekChanged));



    public static readonly DependencyProperty HeaderVisibleProperty = DependencyProperty.Register("Header",
               typeof(bool),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty FooterVisibleProperty = DependencyProperty.Register("Footer",
               typeof(bool),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty WeekdaysVisibleProperty = DependencyProperty.Register("Weekdays",
               typeof(bool),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty WeeknumbersVisibleProperty = DependencyProperty.Register("Weeknumbers",
               typeof(bool),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register("SelectionMode",
               typeof(MonthCalendarSelectionMode),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(MonthCalendarSelectionMode.Single, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectionModeChanged));


    public static readonly DependencyProperty HeaderPropertiesProperty = DependencyProperty.Register("HeaderProperties",
               typeof(HeaderProperties),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new HeaderProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHeaderPropertiesChanged));

    public static readonly DependencyProperty FooterPropertiesProperty = DependencyProperty.Register("FooterProperties",
               typeof(FooterProperties),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new FooterProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFooterPropertiesChanged));

    public static readonly DependencyProperty WeeknumberPropertiesProperty = DependencyProperty.Register("WeeknumberProperties",
               typeof(WeeknumberProperties),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new WeeknumberProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnWeeknumberPropertiesChanged));

    public static readonly DependencyProperty WeekdaysPropertiesProperty = DependencyProperty.Register("WeekdaysProperties",
               typeof(WeekdaysProperties),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new WeekdaysProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnWeekdaysPropertiesChanged));

    public static readonly DependencyProperty CalendarPropertiesProperty = DependencyProperty.Register("CalendarProperties",
               typeof(CalendarProperties),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new CalendarProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCalendarPropertiesChanged));




    #endregion


    #region routed events

    public delegate void SelectionChangedEventHandler(object sender, EventArgs.SelectionChangedEventArgs e);

    public delegate void DayEventHandler(object sender, EventArgs.DayEventArgs e);
    public delegate void WeekEventHandler(object sender, EventArgs.WeekEventArgs e);
    public delegate void WeekdayEventHandler(object sender, EventArgs.WeekdayEventArgs e);

    public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
       "SelectionChanged", RoutingStrategy.Direct, typeof(SelectionChangedEventHandler), typeof(MonthCalendar));


    public static readonly RoutedEvent WeekClickEvent = EventManager.RegisterRoutedEvent(
       "WeekClick", RoutingStrategy.Direct, typeof(WeekEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent WeekDoubleClickEvent = EventManager.RegisterRoutedEvent(
       "WeekDoubleClick", RoutingStrategy.Direct, typeof(WeekEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent WeekdayClickEvent = EventManager.RegisterRoutedEvent(
       "WeekdayClick", RoutingStrategy.Direct, typeof(WeekdayEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent WeekdayDoubleClickEvent = EventManager.RegisterRoutedEvent(
       "WeekdayDoubleClick", RoutingStrategy.Direct, typeof(WeekdayEventHandler), typeof(MonthCalendar));


    public static readonly RoutedEvent DayClickEvent = EventManager.RegisterRoutedEvent(
       "DayClick", RoutingStrategy.Direct, typeof(DayEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent DayDoubleClickEvent = EventManager.RegisterRoutedEvent(
       "DayDoubleClick", RoutingStrategy.Direct, typeof(DayEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent DayLeaveEvent = EventManager.RegisterRoutedEvent(
       "DayLeave", RoutingStrategy.Direct, typeof(DayEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent DayEnterEvent = EventManager.RegisterRoutedEvent(
       "DayEnter", RoutingStrategy.Direct, typeof(DayEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent WeekLeaveEvent = EventManager.RegisterRoutedEvent(
       "WeekLeave", RoutingStrategy.Direct, typeof(WeekEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent WeekEnterEvent = EventManager.RegisterRoutedEvent(
       "WeekEnter", RoutingStrategy.Direct, typeof(WeekEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent WeekdayLeaveEvent = EventManager.RegisterRoutedEvent(
       "WeekdayLeave", RoutingStrategy.Direct, typeof(WeekdayEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent WeekdayEnterEvent = EventManager.RegisterRoutedEvent(
       "WeekdayEnter", RoutingStrategy.Direct, typeof(WeekdayEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent FooterEnterEvent = EventManager.RegisterRoutedEvent(
       "FooterEnter", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent FooterLeaveEvent = EventManager.RegisterRoutedEvent(
       "FooterLeave", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MonthCalendar));


    public delegate void MonthChangedEventHandler(object sender, EventArgs.MonthChangedEventArgs e);

    public static readonly RoutedEvent MonthChangedEvent = EventManager.RegisterRoutedEvent(
       "MonthChanged", RoutingStrategy.Direct, typeof(MonthChangedEventHandler), typeof(MonthCalendar));


    #endregion

    #region constructor

    static MonthCalendar()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MonthCalendar), new FrameworkPropertyMetadata(typeof(MonthCalendar)));

    }

    #endregion


    #region events

    [Category("Calendar")]
    [Browsable(true)]
    public event SelectionChangedEventHandler SelectionChanged
    {
      add { AddHandler(SelectionChangedEvent, value); }
      remove { RemoveHandler(SelectionChangedEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event MonthChangedEventHandler MonthChanged
    {
      add { AddHandler(MonthChangedEvent, value); }
      remove { RemoveHandler(MonthChangedEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event RoutedEventHandler FooterLeave
    {
      add { AddHandler(FooterLeaveEvent, value); }
      remove { RemoveHandler(FooterLeaveEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event RoutedEventHandler FooterEnter
    {
      add { AddHandler(FooterEnterEvent, value); }
      remove { RemoveHandler(FooterEnterEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event WeekdayEventHandler WeekdayClick
    {
      add { AddHandler(WeekdayClickEvent, value); }
      remove { RemoveHandler(WeekdayClickEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event WeekdayEventHandler WeekdayDoubleClick
    {
      add { AddHandler(WeekdayDoubleClickEvent, value); }
      remove { RemoveHandler(WeekdayDoubleClickEvent, value); }
    }


    [Category("Calendar")]
    [Browsable(true)]
    public event WeekEventHandler WeekClick
    {
      add { AddHandler(WeekClickEvent, value); }
      remove { RemoveHandler(WeekClickEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event WeekEventHandler WeekDoubleClick
    {
      add { AddHandler(WeekDoubleClickEvent, value); }
      remove { RemoveHandler(WeekDoubleClickEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event DayEventHandler DayClick
    {
      add { AddHandler(DayClickEvent, value); }
      remove { RemoveHandler(DayClickEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event DayEventHandler DayDoubleClick
    {
      add { AddHandler(DayDoubleClickEvent, value); }
      remove { RemoveHandler(DayDoubleClickEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event DayEventHandler DayLeave
    {
      add { AddHandler(DayLeaveEvent, value); }
      remove { RemoveHandler(DayLeaveEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event DayEventHandler DayEnter
    {
      add { AddHandler(DayEnterEvent, value); }
      remove { RemoveHandler(DayEnterEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event WeekdayEventHandler WeekdayLeave
    {
      add { AddHandler(WeekdayLeaveEvent, value); }
      remove { RemoveHandler(WeekdayLeaveEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event WeekdayEventHandler WeekdayEnter
    {
      add { AddHandler(WeekdayEnterEvent, value); }
      remove { RemoveHandler(WeekdayEnterEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event WeekEventHandler WeekLeave
    {
      add { AddHandler(WeekLeaveEvent, value); }
      remove { RemoveHandler(WeekLeaveEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event WeekEventHandler WeekEnter
    {
      add { AddHandler(WeekEnterEvent, value); }
      remove { RemoveHandler(WeekEnterEvent, value); }
    }

    #endregion

    #region private properties



    #endregion


    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.header = GetTemplateChild("PART_Header") as Header;
      if (this.header != null)
      {
        this.header.Decrease += Header_Decrease;
        this.header.Increase += Header_Increase;
      }
      this.footer = GetTemplateChild("PART_Footer") as Footer;
      if (this.footer != null)
      {
        this.footer.FooterEnter += Footer_FooterEnter;
        this.footer.FooterLeave += Footer_FooterLeave;
      }
      this.calendar = GetTemplateChild("PART_Calendar") as Calendar;
      if (this.calendar != null)
      {
        this.calendar.SelectionChanged += Calendar_SelectionChanged;
        this.calendar.DayEnter += Calendar_DayEnter;
        this.calendar.DayLeave += Calendar_DayLeave;
        this.calendar.DayClick += Calendar_DayClick;
        this.calendar.DayDoubleClick += Calendar_DayDoubleClick;
      }
      this.weekdays = GetTemplateChild("PART_Weekdays") as Weekdays;
      if (this.weekdays != null)
      {
        this.weekdays.WeekdayEnter += Weekdays_WeekdayEnter;
        this.weekdays.WeekdayLeave += Weekdays_WeekdayLeave;
        this.weekdays.WeekdayClick += Weekdays_WeekdayClick;
        this.weekdays.WeekdayDoubleClick += Weekdays_WeekdayDoubleClick;
      }

      this.weeknumbers = GetTemplateChild("PART_Weeknumbers") as Weeknumbers;
      if (this.weeknumbers != null)
      {
        this.weeknumbers.WeekEnter += Weeknumbers_WeekEnter;
        this.weeknumbers.WeekLeave += Weeknumbers_WeekLeave;
        this.weeknumbers.WeekClick += Weeknumbers_WeekClick;
        this.weeknumbers.WeekDoubleClick += Weeknumbers_WeekDoubleClick;
      }

      this.Setup();

    }


    #endregion


    #region private methods

    private void Setup()
    {
      SetupHeader();
      SetupFooter();
      SetupCalendar();
      SetupWeekdays();
      SetupWeeknumbers();
    }

    private void SetupCalendar()
    {
      if (this.calendar != null)
      {
        this.calendar.SetupDays(this.Year, this.Month, this.Days.ToList());
        this.calendar.SelectionMode = this.SelectionMode;
      }
    }

    private void SetupWeekdays()
    {
      if (this.weekdays != null)
      {
        this.weekdays.SetupDays(this.Year, this.Month, this.DayOfWeek.ToList());
      }
    }

    private void SetupWeeknumbers()
    {
      if (this.weeknumbers != null)
      {
        this.weeknumbers.SetupWeeks(this.calendar.Days.First().Date, this.Weeks.ToList());
      }
    }

    private void SetupHeader()
    {
      if (this.header != null)
      {
        this.header.Properties.SetDate(this.Year, this.Month);
      }
    }

    private void SetupFooter()
    {
    }


    internal void GetWeekDays()
    {

      Day[] days = new Day[42];

      System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
      DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

      var date = new DateTime(this.Year, this.Month, 1);
      DayOfWeek firstDayOfMonth = date.DayOfWeek;

      var startPos = (int)firstDayOfMonth + (1 - (int)firstDayOfMonth);
      var daysInMonth = DateTime.DaysInMonth(this.Year, this.Month);

      for (int i = startPos; i <= startPos + daysInMonth; i++)
      {
        days[i] = new Day();
        days[i].Date = date;
        date.AddDays(1);
      }
    }

    #endregion

    #region public properties

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public TrulyObservableCollection<Day> Days
    {
      get
      {
        return (TrulyObservableCollection<Day>)this.GetValue(DaysProperty);
      }
      set
      {
        this.SetValue(DaysProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public TrulyObservableCollection<Week> Weeks
    {
      get
      {
        return (TrulyObservableCollection<Week>)this.GetValue(WeeksProperty);
      }
      set
      {
        this.SetValue(WeeksProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public TrulyObservableCollection<Weekday> DayOfWeek
    {
      get
      {
        return (TrulyObservableCollection<Weekday>)this.GetValue(DayOfWeekProperty);
      }
      set
      {
        this.SetValue(DayOfWeekProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public bool Header
    {
      get
      {
        return (bool)this.GetValue(HeaderVisibleProperty);
      }
      set
      {
        this.SetValue(HeaderVisibleProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public HeaderProperties HeaderProperties
    {
      get
      {
        return (HeaderProperties)this.GetValue(HeaderPropertiesProperty);
      }
      set
      {
        this.SetValue(HeaderPropertiesProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public FooterProperties FooterProperties
    {
      get
      {
        return (FooterProperties)this.GetValue(FooterPropertiesProperty);
      }
      set
      {
        this.SetValue(FooterPropertiesProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public WeeknumberProperties WeeknumberProperties
    {
      get
      {
        return (WeeknumberProperties)this.GetValue(WeeknumberPropertiesProperty);
      }
      set
      {
        this.SetValue(WeeknumberPropertiesProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public WeekdaysProperties WeekdaysProperties
    {
      get
      {
        return (WeekdaysProperties)this.GetValue(WeekdaysPropertiesProperty);
      }
      set
      {
        this.SetValue(WeekdaysPropertiesProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public bool Footer
    {
      get
      {
        return (bool)this.GetValue(FooterVisibleProperty);
      }
      set
      {
        this.SetValue(FooterVisibleProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public bool Weekdays
    {
      get
      {
        return (bool)this.GetValue(WeekdaysVisibleProperty);
      }
      set
      {
        this.SetValue(WeekdaysVisibleProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public bool Weeknumbers
    {
      get
      {
        return (bool)this.GetValue(WeeknumbersVisibleProperty);
      }
      set
      {
        this.SetValue(WeeknumbersVisibleProperty, value);
      }
    }


    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public int Month
    {
      get
      {
        return (int)this.GetValue(MonthProperty);
      }
      set
      {
        this.SetValue(MonthProperty, value);
        EventArgs.MonthChangedEventArgs args = new EventArgs.MonthChangedEventArgs(MonthChangedEvent, this.Month, this.Year);
        RaiseEvent(args);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public int Year
    {
      get
      {
        return (int)this.GetValue(YearProperty);
      }
      set
      {
        this.SetValue(YearProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public MonthCalendarSelectionMode SelectionMode
    {
      get
      {
        return (MonthCalendarSelectionMode)this.GetValue(SelectionModeProperty);
      }
      set
      {
        this.SetValue(SelectionModeProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public CalendarProperties CalendarProperties
    {
      get
      {
        return (CalendarProperties)this.GetValue(CalendarPropertiesProperty);
      }
      set
      {
        this.SetValue(CalendarPropertiesProperty, value);
      }
    }

    #endregion


    #region eventhandlers

    private void Footer_FooterLeave(object sender, System.EventArgs e)
    {
      RoutedEventArgs args = new RoutedEventArgs(FooterLeaveEvent);
      RaiseEvent(args);

    }

    private void Footer_FooterEnter(object sender, System.EventArgs e)
    {
      RoutedEventArgs args = new RoutedEventArgs(FooterEnterEvent);
      RaiseEvent(args);
    }


    private void Calendar_SelectionChanged(object sender, EventArgs.CalendarSelectionChangedEventArgs e)
    {
      EventArgs.SelectionChangedEventArgs args = new EventArgs.SelectionChangedEventArgs(SelectionChangedEvent, e.CurrentSelection, e.PreviousSelection);
      RaiseEvent(args);
    }

    private void Calendar_DayLeave(object sender, EventArgs.CalendarDayEventArgs e)
    {
      EventArgs.DayEventArgs args = new EventArgs.DayEventArgs(DayLeaveEvent, e.Day);
      RaiseEvent(args);
    }

    private void Calendar_DayEnter(object sender, EventArgs.CalendarDayEventArgs e)
    {
      EventArgs.DayEventArgs args = new EventArgs.DayEventArgs(DayEnterEvent, e.Day);
      RaiseEvent(args);
    }

    private void Weeknumbers_WeekDoubleClick(object sender, EventArgs.CalendarWeekEventArgs e)
    {
      EventArgs.WeekEventArgs args = new EventArgs.WeekEventArgs(WeekDoubleClickEvent, e.Week);
      RaiseEvent(args);
    }

    private void Weeknumbers_WeekClick(object sender, EventArgs.CalendarWeekEventArgs e)
    {
      EventArgs.WeekEventArgs args = new EventArgs.WeekEventArgs(WeekClickEvent, e.Week);
      RaiseEvent(args);
    }


    private void Weekdays_WeekdayDoubleClick(object sender, EventArgs.CalendarWeekdayEventArgs e)
    {
      EventArgs.WeekdayEventArgs args = new EventArgs.WeekdayEventArgs(WeekdayDoubleClickEvent, e.Weekday);
      RaiseEvent(args);
    }

    private void Weekdays_WeekdayClick(object sender, EventArgs.CalendarWeekdayEventArgs e)
    {
      EventArgs.WeekdayEventArgs args = new EventArgs.WeekdayEventArgs(WeekdayClickEvent, e.Weekday);
      RaiseEvent(args);
    }

    private void Calendar_DayDoubleClick(object sender, EventArgs.CalendarDayEventArgs e)
    {
      EventArgs.DayEventArgs args = new EventArgs.DayEventArgs(DayDoubleClickEvent, e.Day);
      RaiseEvent(args);
    }

    private void Calendar_DayClick(object sender, EventArgs.CalendarDayEventArgs e)
    {
      EventArgs.DayEventArgs args = new EventArgs.DayEventArgs(DayClickEvent, e.Day);
      RaiseEvent(args);

    }


    private void Weeknumbers_WeekLeave(object sender, EventArgs.CalendarWeekEventArgs e)
    {
      EventArgs.WeekEventArgs args = new EventArgs.WeekEventArgs(WeekLeaveEvent, e.Week);
      RaiseEvent(args);

    }

    private void Weeknumbers_WeekEnter(object sender, EventArgs.CalendarWeekEventArgs e)
    {
      EventArgs.WeekEventArgs args = new EventArgs.WeekEventArgs(WeekEnterEvent, e.Week);
      RaiseEvent(args);

    }

    private void Weekdays_WeekdayLeave(object sender, EventArgs.CalendarWeekdayEventArgs e)
    {
      EventArgs.WeekdayEventArgs args = new EventArgs.WeekdayEventArgs(WeekdayLeaveEvent, e.Weekday);
      RaiseEvent(args);

    }

    private void Weekdays_WeekdayEnter(object sender, EventArgs.CalendarWeekdayEventArgs e)
    {
      EventArgs.WeekdayEventArgs args = new EventArgs.WeekdayEventArgs(WeekdayEnterEvent, e.Weekday);
      RaiseEvent(args);

    }


    private void Header_Decrease(object sender, RoutedEventArgs e)
    {
      this.Month--;
    }

    private void Header_Increase(object sender, RoutedEventArgs e)
    {
      this.Month++;
    }

    protected virtual void OnHeaderPropertiesChanged(object newValue, object oldValue)
    {
      if (this.header != null)
      {
        this.header.Properties = (HeaderProperties)newValue;
      }
    }

    private static void OnFooterPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnFooterPropertiesChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnFooterPropertiesChanged(object newValue, object oldValue)
    {
      if (this.footer != null)
      {
        this.footer.Properties = (FooterProperties)newValue;
        
      }
    }

    private static void OnCalendarPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnCalendarPropertiesChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnCalendarPropertiesChanged(object newValue, object oldValue)
    {
      if (this.calendar != null)
      {
        this.calendar.Properties = (CalendarProperties)newValue;
        this.calendar.Properties.PropertyChanged -= CalendarPropertiesChanged;
        this.calendar.Properties.PropertyChanged += CalendarPropertiesChanged;
        this.calendar.SetupDays(this.Year, this.Month, this.Days.ToList());
      }
    }

    private void CalendarPropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      SetupCalendar();
    }

    private static void OnDaysChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnDaysChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnDaysChanged(object newValue, object oldValue)
    {
      if (this.calendar != null)
      {
        this.calendar.SetupDays(this.Year, this.Month, this.Days.ToList());
        this.Days.CollectionChanged += (s, e) =>
        {
          this.calendar.SetupDays(this.Year, this.Month, this.Days.ToList());
        };
      }
    }

    private static void OnWeeksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnWeeksChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnWeeksChanged(object newValue, object oldValue)
    {
      if (this.weeknumbers != null)
      {

        this.weeknumbers.SetupWeeks(this.calendar.Days.First().Date, this.Weeks.ToList());
        this.Weeks.CollectionChanged += (s, e) =>
        {
          if (this.calendar != null && this.weeknumbers != null)
          {
            this.weeknumbers.SetupWeeks(this.calendar.Days.First().Date, this.Weeks.ToList());
          }
        };
      }
    }

    private static void OnDayOfWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnDayOfWeekChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnDayOfWeekChanged(object newValue, object oldValue)
    {
      if (this.weekdays != null)
      {

        this.weekdays.SetupDays(this.Year, this.Month, this.DayOfWeek.ToList());
        this.Weeks.CollectionChanged += (s, e) =>
        {
          if (this.calendar != null && this.weekdays != null)
          {
            this.weekdays.SetupDays(this.Year, this.Month, this.DayOfWeek.ToList());
          }
        };
      }
    }

    private static void OnWeeknumberPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnWeeknumberPropertiesChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnWeeknumberPropertiesChanged(object newValue, object oldValue)
    {
      if (this.weeknumbers != null)
      {
        this.weeknumbers.Properties = (WeeknumberProperties)newValue;
      }
    }

    private static void OnWeekdaysPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnWeekdaysPropertiesChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnWeekdaysPropertiesChanged(object newValue, object oldValue)
    {
      if (this.weekdays != null)
      {
        this.weekdays.Properties = (WeekdaysProperties)newValue;
      }
    }
  
    protected virtual void OHeaderPropertiesChanged(object newValue, object oldValue)
    {
      if (this.header != null)
      {
        this.header.Properties = (HeaderProperties)newValue;

      }
    }

    private static void OnHeaderPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
      {
        calendar.OnHeaderPropertiesChanged(e.NewValue, e.OldValue);
        calendar.SetupHeader();
      }
    }


    protected virtual void OnSelectionModeChanged(object newValue, object oldValue)
    {
      if (this.calendar != null)
      {
        this.calendar.SelectionMode = (MonthCalendarSelectionMode)newValue;
      }
    }

    private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnSelectionModeChanged(e.NewValue, e.OldValue);
    }


    private static void OnYearChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnYearChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnYearChanged(object newValue, object oldValue)
    {
    }

    private static void OnMonthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnMonthChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnMonthChanged(object newValue, object oldValue)
    {
      SetupHeader();
      SetupCalendar();
      SetupWeeknumbers();
      SetupWeekdays();
    }

    private static object OnCoerceMonthChanged(DependencyObject d, object value)
    {
      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        return calendar.OnCoerceMonthChanged(value);
      else
        return value;
    }

    protected virtual object OnCoerceMonthChanged(object value)
    {
      int month = (int)value;

      if (month < 1)
      {
        this.Year--;
        return 12;
      }
      else if (month > 12)
      {
        this.Year++;
        return 1;
      }



      return value;
    }

    private static object OnCoerceYearChanged(DependencyObject d, object value)
    {
      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        return calendar.OnCoerceYearChanged(value);
      else
        return value;
    }

    protected virtual object OnCoerceYearChanged(object value)
    {
      int year = (int)value;
      return value;
    }

    #endregion

    #region public methods


    public void Refresh()
    {
      this.Setup();
    }

    public void SuspendLayout()
    {
      this.calendar.SuspendLayout = true;
      //this.Header.suspendLayout = true;
      //this.Footer.suspendLayout = true;
      this.weekdays.SuspendLayout = true;
      this.weeknumbers.SuspendLayout = true;
    }

    public void ResumeLayout()
    {
      this.calendar.SuspendLayout = false;
      //this.Header.suspendLayout = true;
      //this.Footer.suspendLayout = true;
      this.weekdays.SuspendLayout = false;
      this.weeknumbers.SuspendLayout = false;
    }

    #endregion
  }
}
