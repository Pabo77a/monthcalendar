﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using Pabo.MonthCalendar.Common;
using System.Diagnostics;

using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Header", Type = typeof(Header))]
  [TemplatePart(Name = "PART_Footer", Type = typeof(Footer))]
  [TemplatePart(Name = "PART_Calendar", Type = typeof(Calendar))]
  [ToolboxItem(true)]
  public class MonthCalendar : BaseControl
  {

    #region private members

    private Header header;
    private Footer footer;
    private Calendar calendar;

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
               new FrameworkPropertyMetadata(new List<DayItem>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty HeaderVisibleProperty = DependencyProperty.Register("Header",
               typeof(bool),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty FooterVisibleProperty = DependencyProperty.Register("Footer",
               typeof(bool),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty FooterTextProperty = DependencyProperty.Register("FooterText",
               typeof(string),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,OnFooterTextChanged));

    public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register("SelectionMode",
               typeof(MonthCalendarSelectionMode),
               typeof(MonthCalendar),
               new FrameworkPropertyMetadata(MonthCalendarSelectionMode.Single, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectionModeChanged));



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
      this.Setup();

    }

   

    #endregion


    #region private methods

    private void Setup()
    {
      if (this.header != null)
      {
        this.header.SetDate(this.Year, this.Month);
      }
      if (this.footer != null)
      {
        this.footer.Text = !string.IsNullOrEmpty(this.FooterText) ? this.FooterText : DateTime.Now.ToShortDateString();
      }
      if (this.header != null)
      {
        this.GetWeekDays();
      }
      if (this.calendar != null)
      {
        this.calendar.SetupDays(this.Year, this.Month, this.Days.Where(x => x.Date.Month == this.Month).ToList());
        this.calendar.SelectionMode = this.SelectionMode;
      }
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
    public string FooterText
    {
      get
      {
        return (string)this.GetValue(FooterTextProperty);
      }
      set
      {
        this.SetValue(FooterTextProperty, value);
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

    private static void OnFooterTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnFooterTextChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnFooterTextChanged(object newValue, object oldValue)
    {
      this.Setup();
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
      this.Setup();
    }

    private static void OnMonthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthCalendar calendar = d as MonthCalendar;
      if (calendar != null)
        calendar.OnMonthChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnMonthChanged(object newValue, object oldValue)
    {
      this.Setup();
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
