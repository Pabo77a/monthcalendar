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
               new FrameworkPropertyMetadata(default(Int32), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnMonthChanged,
                   OnCoerceMonthChanged, false, UpdateSourceTrigger.PropertyChanged));

    public static readonly DependencyProperty YearProperty = DependencyProperty.Register("Year",
               typeof(int),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(default(Int32), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   OnYearChanged,
                   OnCoerceYearChanged, false, UpdateSourceTrigger.PropertyChanged));

    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(List<DayItem>),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(new List<DayItem>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDaysChanged));

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

    public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
       "SelectionChanged", RoutingStrategy.Direct, typeof(SelectionChangedEventHandler), typeof(MonthCalendar));

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
      this.calendar = GetTemplateChild("PART_Calendar") as Calendar;
      if (this.calendar != null)
      {
        this.calendar.SelectionChanged += Calendar_SelectionChanged;
      }
      this.weekdays = GetTemplateChild("PART_Weekdays") as Weekdays;
      if (this.weekdays != null)
      {
      }

      this.weeknumbers = GetTemplateChild("PART_Weeknumbers") as Weeknumbers;
      if (this.weeknumbers != null)
      {
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
        var firstDateInMonth = new DateTime(this.Year, this.Month, 1);

        this.calendar.SetupDays(this.Year, this.Month, this.Days.Where(x => x.Date > firstDateInMonth.AddDays(-15) && x.Date < firstDateInMonth.AddDays(45)).ToList());
        this.calendar.SelectionMode = this.SelectionMode;
      }
    }

    private void SetupWeekdays()
    {
      if (this.weekdays != null)
      {
        this.weekdays.SetupDays();
      }
    }

    private void SetupWeeknumbers()
    {
      if (this.weeknumbers != null)
      {
        this.weeknumbers.SetupWeeks(this.calendar.Days.First().Date);
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

      DayItem[] days = new DayItem[42];

      System.Globalization.CultureInfo ci =  System.Threading.Thread.CurrentThread.CurrentCulture;
      DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

      var date = new DateTime(this .Year, this.Month, 1);
      DayOfWeek firstDayOfMonth = date.DayOfWeek;

      var startPos = (int)firstDayOfMonth + (1 - (int)firstDayOfMonth);
      var daysInMonth = DateTime.DaysInMonth(this.Year, this.Month);

      for (int i = startPos;i<=startPos + daysInMonth;i++)
      {
        days[i] = new DayItem();
        days[i].Date = date;
        date.AddDays(1);
      }
    }

    #endregion

    #region public properties

    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    public List<DayItem> Days
    {
      get
      {
        return (List<DayItem>)this.GetValue(DaysProperty);
      }
      set
      {
        this.SetValue(DaysProperty, value);
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

    private void Calendar_SelectionChanged(object sender, EventArgs.CalendarSelectionChangedEventArgs e)
    {
      EventArgs.SelectionChangedEventArgs args = new EventArgs.SelectionChangedEventArgs(SelectionChangedEvent, e.CurrentSelection,e.PreviousSelection);
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
        this.calendar.SetupDays(this.Year, this.Month, this.Days);
      }
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
        this.calendar.SetupDays(this.Year, this.Month, this.Days);
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


    #endregion
  }
}
