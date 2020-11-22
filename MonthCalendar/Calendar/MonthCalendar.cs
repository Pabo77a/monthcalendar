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
  [TemplatePart(Name = "PART_Months", Type = typeof(MonthView))]
  [TemplatePart(Name = "PART_Weekdays", Type = typeof(Weekdays))]
  [TemplatePart(Name = "PART_Weeknumbers", Type = typeof(Weeknumbers))]
  [ToolboxItem(true)]
  public class MonthCalendar : BaseControl
  {

    #region private members

    private Header header;
    private Footer footer;
    private Calendar calendar;
    private MonthView monthView;
    private Weekdays weekdays;
    private Weeknumbers weeknumbers;

    #endregion


    #region dependency properties


    public static readonly DependencyProperty MonthTemplateProperty = DependencyProperty.Register("MonthTemplate",
           typeof(DataTemplate),
           typeof(MonthCalendar),
           new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
               OnMonthTemplateChanged));


    public static readonly DependencyProperty DayTemplateProperty = DependencyProperty.Register("DayTemplate",
               typeof(DataTemplate),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnDayTemplateChanged));

    public static readonly DependencyProperty VisualModeProperty = DependencyProperty.Register("VisualMode",
               typeof(VisualMode),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(VisualMode.Days, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnVisualModeChanged));

    public static readonly DependencyProperty WeekTemplateProperty = DependencyProperty.Register("WeekTemplate",
               typeof(DataTemplate),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnWeekTemplateChanged));

    public static readonly DependencyProperty WeekdayTemplateProperty = DependencyProperty.Register("WeekdayTemplate",
               typeof(DataTemplate),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnWeekdayTemplateChanged));


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

    public static readonly DependencyProperty MinDateProperty = DependencyProperty.Register("MinDate",
               typeof(DateTime),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(DateTime.MinValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnMinDateChanged,
                   OnCoerceMinDateChanged, false, UpdateSourceTrigger.PropertyChanged));

    public static readonly DependencyProperty MaxDateProperty = DependencyProperty.Register("MaxDate",
               typeof(DateTime),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(DateTime.MaxValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnMaxDateChanged,
                   OnCoerceMaxDateChanged, false, UpdateSourceTrigger.PropertyChanged));

    public static readonly DependencyProperty MonthsProperty = DependencyProperty.Register("Months",
               typeof(TrulyObservableCollection<Month>),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new TrulyObservableCollection<Month>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMonthsChanged));


    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(TrulyObservableCollection<Day>),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new TrulyObservableCollection<Day>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDaysChanged));

    public static readonly DependencyProperty DisabledDaysProperty = DependencyProperty.Register("DisabledDays",
               typeof(ObservableCollection<DateTime>),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new ObservableCollection<DateTime>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDisabledDaysChanged));

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
               new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                 OnWeekdaysVisibleChanged,
                 OnCoerceWeekdaysVisibleChanged, false, UpdateSourceTrigger.PropertyChanged));

    public static readonly DependencyProperty WeeknumbersVisibleProperty = DependencyProperty.Register("Weeknumbers",
               typeof(bool),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                 OnWeeknumbersVisibleChanged,
                 OnCoerceWeeknumbersVisibleChanged, false, UpdateSourceTrigger.PropertyChanged));


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

    public static readonly DependencyProperty MonthPropertiesProperty = DependencyProperty.Register("MonthProperties",
               typeof(MonthProperties),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new MonthProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMonthPropertiesChanged));





    #endregion


    #region routed events

    public delegate void DaySelectionChangedEventHandler(object sender, EventArgs.SelectionChangedEventArgs<Day> e);
    public delegate void MonthSelectionChangedEventHandler(object sender, EventArgs.SelectionChangedEventArgs<Month> e);

    public delegate void MonthEventHandler(object sender, EventArgs.MonthEventArgs e);
    public delegate void DayEventHandler(object sender, EventArgs.DayEventArgs e);
    public delegate void WeekEventHandler(object sender, EventArgs.WeekEventArgs e);
    public delegate void WeekdayEventHandler(object sender, EventArgs.WeekdayEventArgs e);

    public static readonly RoutedEvent DaySelectionChangedEvent = EventManager.RegisterRoutedEvent(
       "DaySelectionChanged", RoutingStrategy.Direct, typeof(DaySelectionChangedEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent MonthSelectionChangedEvent = EventManager.RegisterRoutedEvent(
       "MonthSelectionChanged", RoutingStrategy.Direct, typeof(MonthSelectionChangedEventHandler), typeof(MonthCalendar));

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


    public static readonly RoutedEvent MonthClickEvent = EventManager.RegisterRoutedEvent(
       "MonthClick", RoutingStrategy.Direct, typeof(MonthEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent MonthDoubleClickEvent = EventManager.RegisterRoutedEvent(
       "MonthDoubleClick", RoutingStrategy.Direct, typeof(MonthEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent MonthLeaveEvent = EventManager.RegisterRoutedEvent(
       "MonthLeave", RoutingStrategy.Direct, typeof(MonthEventHandler), typeof(MonthCalendar));

    public static readonly RoutedEvent MonthEnterEvent = EventManager.RegisterRoutedEvent(
       "MonthEnter", RoutingStrategy.Direct, typeof(MonthEventHandler), typeof(MonthCalendar));


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
    public event DaySelectionChangedEventHandler DaySelectionChanged
    {
      add { AddHandler(DaySelectionChangedEvent, value); }
      remove { RemoveHandler(DaySelectionChangedEvent, value); }
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

    [Category("Calendar")]
    [Browsable(true)]
    public event MonthSelectionChangedEventHandler MonthSelectionChanged
    {
      add { AddHandler(MonthSelectionChangedEvent, value); }
      remove { RemoveHandler(MonthSelectionChangedEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event MonthEventHandler MonthClick
    {
      add { AddHandler(MonthClickEvent, value); }
      remove { RemoveHandler(MonthClickEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event MonthEventHandler MonthDoubleClick
    {
      add { AddHandler(MonthDoubleClickEvent, value); }
      remove { RemoveHandler(MonthDoubleClickEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event MonthEventHandler MonthLeave
    {
      add { AddHandler(MonthLeaveEvent, value); }
      remove { RemoveHandler(MonthLeaveEvent, value); }
    }

    [Category("Calendar")]
    [Browsable(true)]
    public event MonthEventHandler MonthEnter
    {
      add { AddHandler(MonthEnterEvent, value); }
      remove { RemoveHandler(MonthEnterEvent, value); }
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
      this.monthView = GetTemplateChild("PART_Months") as MonthView;
      if (this.monthView != null)
      {
        this.monthView.SelectionChanged += MonthView_SelectionChanged;
        this.monthView.MonthEnter += MonthView_MonthEnter;
        this.monthView.MonthLeave += MonthView_MonthLeave;
        this.monthView.MonthClick += MonthView_MonthClick;
        this.monthView.MonthDoubleClick += MonthView_MonthDoubleClick;
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
      if (VisualMode == VisualMode.Days)
      {
        SetupCalendar();
      }
      if (VisualMode == VisualMode.Months)
      {
        SetupMonthView();
      }
      if (Weekdays)
      {
        SetupWeekdays();
      }
      if (Weeknumbers)
      {
        SetupWeeknumbers();
      }
    }

    private void SetupCalendar()
    {
      if (this.calendar != null)
      {
        this.calendar.SetupDays(this.Year, this.Month, this.MinDate, this.MaxDate, this.Days.ToList(), this.DisabledDays.ToList(), this.DayTemplate);
        this.calendar.SelectionMode = this.SelectionMode;
      }
    }

    private void SetupMonthView()
    {
      if (this.monthView != null)
      {
        this.monthView.SetupMonths(this.Year, this.Months.ToList(), this.MonthTemplate);
        this.monthView.SelectionMode = this.SelectionMode;
      }
    }

    private void SetupWeekdays()
    {
      if (this.weekdays != null)
      {
        this.weekdays.SetupDays(this.Year, this.Month, this.DayOfWeek.ToList(), this.WeekdayTemplate);
      }
    }

    private void SetupWeeknumbers()
    {
      if (this.weeknumbers != null)
      {
        this.weeknumbers.SetupWeeks(this.calendar.Days.First().Date, this.Weeks.ToList(), this.WeekTemplate);
      }
    }

    private void SetupHeader()
    {
      if (this.header != null)
      {
        this.header.Setup(this.MinDate, this.MaxDate, this.Year, this.Month);
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
    public VisualMode VisualMode
    {
      get
      {
        return (VisualMode)this.GetValue(VisualModeProperty);
      }
      set
      {
        this.SetValue(VisualModeProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public DataTemplate MonthTemplate
    {
      get
      {
        return (DataTemplate)this.GetValue(MonthTemplateProperty);
      }
      set
      {
        this.SetValue(MonthTemplateProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public DataTemplate DayTemplate
    {
      get
      {
        return (DataTemplate)this.GetValue(DayTemplateProperty);
      }
      set
      {
        this.SetValue(DayTemplateProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public DataTemplate WeekdayTemplate
    {
      get
      {
        return (DataTemplate)this.GetValue(WeekdayTemplateProperty);
      }
      set
      {
        this.SetValue(WeekdayTemplateProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public DataTemplate WeekTemplate
    {
      get
      {
        return (DataTemplate)this.GetValue(WeekTemplateProperty);
      }
      set
      {
        this.SetValue(WeekTemplateProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public DateTime MinDate
    {
      get
      {
        return (DateTime)this.GetValue(MinDateProperty);
      }
      set
      {
        this.SetValue(MinDateProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public DateTime MaxDate
    {
      get
      {
        return (DateTime)this.GetValue(MaxDateProperty);
      }
      set
      {
        this.SetValue(MaxDateProperty, value);
      }
    }


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
    public TrulyObservableCollection<Month> Months
    {
      get
      {
        return (TrulyObservableCollection<Month>)this.GetValue(MonthsProperty);
      }
      set
      {
        this.SetValue(MonthsProperty, value);
      }
    }

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public ObservableCollection<DateTime> DisabledDays
    {
      get
      {
        return (ObservableCollection<DateTime>)this.GetValue(DisabledDaysProperty);
      }
      set
      {
        this.SetValue(DisabledDaysProperty, value);
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
        return (bool)this.GetValue(WeekdaysVisibleProperty) && (VisualMode)this.GetValue(VisualModeProperty) == VisualMode.Days;
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
        return (bool)this.GetValue(WeeknumbersVisibleProperty) && (VisualMode)this.GetValue(VisualModeProperty) == VisualMode.Days; ;
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

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public MonthProperties MonthProperties
    {
      get
      {
        return (MonthProperties)this.GetValue(MonthPropertiesProperty);
      }
      set
      {
        this.SetValue(MonthPropertiesProperty, value);
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


    private void Calendar_SelectionChanged(object sender, EventArgs.CalendarSelectionChangedEventArgs<Day> e)
    {
      EventArgs.SelectionChangedEventArgs<Day> args = new EventArgs.SelectionChangedEventArgs<Day>(DaySelectionChangedEvent, e.Selected);
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

    private void MonthView_SelectionChanged(object sender, EventArgs.CalendarSelectionChangedEventArgs<Month> e)
    {
      EventArgs.SelectionChangedEventArgs<Month> args = new EventArgs.SelectionChangedEventArgs<Month>(MonthSelectionChangedEvent, e.Selected);
      RaiseEvent(args);
    }

    private void MonthView_MonthDoubleClick(object sender, EventArgs.CalendarMonthEventArgs e)
    {
      EventArgs.MonthEventArgs args = new EventArgs.MonthEventArgs(MonthDoubleClickEvent, e.Month);
      RaiseEvent(args);
    }

    private void MonthView_MonthClick(object sender, EventArgs.CalendarMonthEventArgs e)
    {
      EventArgs.MonthEventArgs args = new EventArgs.MonthEventArgs(MonthClickEvent, e.Month);
      RaiseEvent(args);
    }

    private void MonthView_MonthLeave(object sender, EventArgs.CalendarMonthEventArgs e)
    {
      EventArgs.MonthEventArgs args = new EventArgs.MonthEventArgs(MonthLeaveEvent, e.Month);
      RaiseEvent(args);
    }

    private void MonthView_MonthEnter(object sender, EventArgs.CalendarMonthEventArgs e)
    {
      EventArgs.MonthEventArgs args = new EventArgs.MonthEventArgs(MonthEnterEvent, e.Month);
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
        this.SetupCalendar();
      }
    }

    private static void OnMonthPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnMonthPropertiesChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnMonthPropertiesChanged(object newValue, object oldValue)
    {
      if (this.monthView != null)
      {
        this.monthView.Properties = (MonthProperties)newValue;
        this.monthView.Properties.PropertyChanged -= MonthPropertiesChanged;
        this.monthView.Properties.PropertyChanged += MonthPropertiesChanged;
        this.SetupMonthView();
      }
    }

    private void MonthPropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      SetupMonthView();
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
        this.SetupCalendar();
        this.Days.CollectionChanged += (s, e) =>
        {
          this.SetupCalendar();
        };
      }
    }

    private static void OnMonthsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnMonthsChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnMonthsChanged(object newValue, object oldValue)
    {
      if (this.monthView != null)
      {
        this.SetupMonthView();
        this.Months.CollectionChanged += (s, e) =>
        {
          this.SetupMonthView();
        };
      }
    }


    private static void OnDisabledDaysChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnDisabledDaysChanged(e.NewValue, e.OldValue);
    }


    protected virtual void OnDisabledDaysChanged(object newValue, object oldValue)
    {
      if (this.calendar != null)
      {
        this.SetupCalendar();
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

        this.weeknumbers.SetupWeeks(this.calendar.Days.First().Date, this.Weeks.ToList(), this.WeekTemplate);
        this.Weeks.CollectionChanged += (s, e) =>
        {
          if (this.calendar != null && this.weeknumbers != null)
          {
            this.weeknumbers.SetupWeeks(this.calendar.Days.First().Date, this.Weeks.ToList(), this.WeekTemplate);
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

        this.weekdays.SetupDays(this.Year, this.Month, this.DayOfWeek.ToList(), this.WeekdayTemplate);
        this.Weeks.CollectionChanged += (s, e) =>
        {
          if (this.calendar != null && this.weekdays != null)
          {
            this.weekdays.SetupDays(this.Year, this.Month, this.DayOfWeek.ToList(), this.WeekdayTemplate);
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
      if (this.monthView != null)
      {
        this.monthView.SelectionMode = (MonthCalendarSelectionMode)newValue;
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
      SetupHeader();
      SetupCalendar();
      SetupWeeknumbers();
      SetupWeekdays();
    }

    private static void OnMinDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnMinDateChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnMinDateChanged(object newValue, object oldValue)
    {
      SetupHeader();
      SetupCalendar();
    }

    private static object OnCoerceMinDateChanged(DependencyObject d, object value)
    {
      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        return calendar.OnCoerceMinDateChanged(value);
      else
        return value;
    }

    protected virtual object OnCoerceMinDateChanged(object value)
    {
      DateTime date = (DateTime)value;
      return (date >= MaxDate) ? MaxDate.AddDays(-1) : date;
     }

    private static void OnMaxDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnMaxDateChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnMaxDateChanged(object newValue, object oldValue)
    {
      SetupHeader();
      SetupCalendar();
    }

    private static object OnCoerceMaxDateChanged(DependencyObject d, object value)
    {
      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        return calendar.OnCoerceMaxDateChanged(value);
      else
        return value;
    }

    protected virtual object OnCoerceMaxDateChanged(object value)
    {
      DateTime date = (DateTime)value;
      return (date <= MinDate) ? MinDate.AddDays(1) : date;
    }

    private static void OnWeekdaysVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnWeekdaysVisibleChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnWeekdaysVisibleChanged(object newValue, object oldValue)
    {
      //SetupHeader();
      //SetupCalendar();
    }

    private static object OnCoerceWeekdaysVisibleChanged(DependencyObject d, object value)
    {
      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        return calendar.OnCoerceWeekdaysVisibleChanged(value);
      else
        return value;
    }

    protected virtual object OnCoerceWeekdaysVisibleChanged(object value)
    {
      bool visible = (bool)value;
      return visible && VisualMode == VisualMode.Days;
    }

    private static void OnWeeknumbersVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnWeeknumberVisibleChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnWeeknumberVisibleChanged(object newValue, object oldValue)
    {
      //SetupHeader();
      //SetupCalendar();
    }

    private static object OnCoerceWeeknumbersVisibleChanged(DependencyObject d, object value)
    {
      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        return calendar.OnCoerceWeeknumbersVisibleChanged(value);
      else
        return value;
    }

    protected virtual object OnCoerceWeeknumbersVisibleChanged(object value)
    {
      bool visible = (bool)value;
      return visible && VisualMode == VisualMode.Days;
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

    private static void OnDayTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnDayTemplateChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnDayTemplateChanged(object newValue, object oldValue)
    {
      if (this.calendar != null)
      {
        
        SetupCalendar();
      }
    }
    private static void OnMonthTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnMonthTemplateChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnMonthTemplateChanged(object newValue, object oldValue)
    {
      if (this.monthView != null)
      {

        SetupMonthView();
      }
    }


    private static void OnVisualModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
      {
        d.CoerceValue(WeeknumbersVisibleProperty);
        d.CoerceValue(WeekdaysVisibleProperty);
        calendar.OnVisualModeChanged(e.NewValue, e.OldValue);
      }
    }

    protected virtual void OnVisualModeChanged(object newValue, object oldValue)
    {
      if (this.calendar != null)
      {

      }
    }

    private static void OnWeekTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnWeekTemplateChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnWeekTemplateChanged(object newValue, object oldValue)
    {
      if (this.weeknumbers != null)
      {
        SetupWeeknumbers();
      }
    }

    private static void OnWeekdayTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnWeekdayTemplateChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnWeekdayTemplateChanged(object newValue, object oldValue)
    {
      if (this.weekdays != null)
      {
        SetupWeekdays();
      }
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

    public void Select(List<DateTime> dates)
    {
      if (this.calendar != null)
      {
        this.calendar.Select(dates);
      }
    }

    public void Deselect(List<DateTime> dates)
    {
      if (this.calendar != null)
      {
        this.calendar.Deselect(dates);
      }
    }

    public void SelectWeek(int week)
    {
      if (this.calendar != null)
      {
        this.calendar.SelectWeek(week);
      }
    }

    public void DeselectWeek(int week)
    {
      if (this.calendar != null)
      {
        this.calendar.DeselectWeek(week);
      }
    }

    public void SelectWeekday(DayOfWeek weekday)
    {
      if (this.calendar != null)
      {
        this.calendar.SelectWeekday(weekday);
      }
    }

    public void DeselectWeekday(DayOfWeek weekday)
    {
      if (this.calendar != null)
      {
        this.calendar.DeselectWeekday(weekday);
      }
    }


    #endregion
  }
}
