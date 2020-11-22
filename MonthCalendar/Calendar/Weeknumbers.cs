using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using Pabo.MonthCalendar.Model;
using System.Windows.Media;
using Pabo.MonthCalendar.Properties;
using System.Windows.Controls;
using System.Globalization;

using Pabo.MonthCalendar.Common;
using Pabo.MonthCalendar.EventArgs;
using System.Windows.Input;
using System.Diagnostics;

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Host", Type = typeof(System.Windows.Controls.ItemsControl))]
  [ToolboxItem(false)]
  internal class Weeknumbers : ItemsControl<CalendarWeek>
  {

    #region dependency properties

    public static readonly DependencyProperty WeeksProperty = DependencyProperty.Register("Weeks",
               typeof(List<CalendarWeek>),
               typeof(Weeknumbers),
               new FrameworkPropertyMetadata(new List<CalendarWeek>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnWeeksChanged));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(WeeknumberProperties),
               typeof(Weeknumbers),
               new FrameworkPropertyMetadata(new WeeknumberProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion
  
    private DateTime firstDate;
    private List<Week> weekItems;

    private DataTemplate template;

    public Weeknumbers() : base(1, 6)
    {
      base.Items = this.Weeks;
      this.popup = CreatePopup(this.Properties);
      this.Click += (sender, e) =>
      {
        this.OnWeekClick(new CalendarWeekEventArgs(clickItem));
      };
    }

    #region events

    internal event EventHandler<CalendarWeekEventArgs> WeekLeave;

    internal event EventHandler<CalendarWeekEventArgs> WeekEnter;

    internal event EventHandler<CalendarWeekEventArgs> WeekClick;

    internal event EventHandler<CalendarWeekEventArgs> WeekDoubleClick;

    #endregion

    #region constructor

    static Weeknumbers()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Weeknumbers), new FrameworkPropertyMetadata(typeof(Weeknumbers)));

    }

    #endregion


    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.Width = this.Properties.TextFontSize + 25;
    }

    #endregion

    #region properties

    internal List<CalendarWeek> Weeks
    {
      get
      {
        return (List<CalendarWeek>)this.GetValue(WeeksProperty);
      }
      set
      {
        this.SetValue(WeeksProperty, value);
      }
    }


    internal WeeknumberProperties Properties
    {
      get
      {
        return (WeeknumberProperties)this.GetValue(PropertiesProperty);
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

    private static void OnWeeksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      Weeknumbers weeknumbers = d as Weeknumbers;
      if (weeknumbers != null)
        weeknumbers.OnWeeksChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnWeeksChanged(object newValue, object oldValue)
    {
      base.Items = (List<CalendarWeek>)newValue;
    }

    internal void SetupWeeks(DateTime firstDate, List<Week> weekItems, DataTemplate template)
    {
      if (!SuspendLayout)
      {
        this.firstDate = firstDate;
        this.weekItems = weekItems;
        this.template = template;
        this.Width = this.Properties.TextFontSize + 25;

        var year = firstDate.Year;

        var weeks = new List<CalendarWeek>();
        var date = firstDate;
        for (int i = 0; i < 6; i++)
        {
          var week = new CalendarWeek(date);
          var number = ISOWeek.GetWeekOfYear(date);
          week.Template = this.template;
          week.Number = number;
          var item = weekItems.FirstOrDefault(x => x.Year == year && x.Number == number);
          Utils.CopyProperties<WeeknumberProperties, CalendarWeek>(Properties, week);
          if (item != null)
          {
            Utils.CopyProperties<Week, CalendarWeek>(item, week);
          }
          weeks.Add(week);
          date = date.AddDays(7);
        }

        this.Weeks = weeks;
      }

    }

    protected override void OnSelectionChanged()
    {
    }

    protected override void Setup()
    {
      this.SetupWeeks(this.firstDate, this.weekItems, this.template);
    }

    private void OnWeekClick(CalendarWeekEventArgs e)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekClick;
      handler?.Invoke(this, e);

    }

    protected override void OnItemLeave(CalendarWeek item)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekLeave;
      handler?.Invoke(this, new CalendarWeekEventArgs(item));

    }
    protected override void OnItemEnter(CalendarWeek item)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekEnter;
      handler?.Invoke(this, new CalendarWeekEventArgs(item));
    }

    protected override void OnItemDoubleClick(CalendarWeek item)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekDoubleClick;
      handler?.Invoke(this, new CalendarWeekEventArgs(item));
    }

    #region event handlers

    private void PropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
    }

 
    #endregion


  }

}