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
  [ToolboxItem(false)]
  internal class Weeknumbers : ItemsControl
  {

    #region dependency properties

    public static readonly DependencyProperty WeeksProperty = DependencyProperty.Register("Weeks",
               typeof(List<CalendarWeek>),
               typeof(Weeknumbers),
               new FrameworkPropertyMetadata(new List<CalendarWeek>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(WeeknumberProperties),
               typeof(Weeknumbers),
               new FrameworkPropertyMetadata(new WeeknumberProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion


    private CalendarWeek activeWeek;
    private System.Windows.Controls.ItemsControl itemsControl;
    private CalendarWeek clickWeek;

    private DateTime firstDate;
    private List<Week> weekItems;
    private bool suspendLayout = false;

    public Weeknumbers() : base(1, 6)
    {
      this.Click += (sender, e) =>
      {
        this.OnWeekClick(new CalendarWeekEventArgs(clickWeek));
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

      this.itemsControl = GetTemplateChild("PART_Host") as System.Windows.Controls.ItemsControl;
      if (this.itemsControl != null)
      {
        this.itemsControl.MouseMove += ItemsControl_MouseMove;
        this.itemsControl.MouseDoubleClick += ItemsControl_MouseDoubleClick;
        this.itemsControl.MouseDown += ItemsControl_MouseDown;
        this.itemsControl.MouseEnter += ItemsControl_MouseEnter;
        this.itemsControl.MouseLeave += ItemsControl_MouseLeave;
      }

    }
 
    #endregion

    #region properties


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

    private void PropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
    }

    #endregion

    internal void SetupWeeks(DateTime firstDate, List<Week> weekItems)
    {
      if (!SuspendLayout)
      {
        this.firstDate = firstDate;
        this.weekItems = weekItems;

        this.Width = this.Properties.TextFontSize + 25;

        var year = firstDate.Year;

        var weeks = new List<CalendarWeek>();
        var date = firstDate;
        for (int i = 0; i < 6; i++)
        {
          var week = new CalendarWeek(date);
          var number = ISOWeek.GetWeekOfYear(date);
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
    
    private void Setup()
    {
      this.SetupWeeks(this.firstDate, this.weekItems);
    }

    private void ItemsControl_MouseLeave(object sender, MouseEventArgs e)
    {
      if (this.activeWeek != null)
      {
        this.activeWeek.MouseOver = false;
        this.OnWeekLeave(new CalendarWeekEventArgs(this.activeWeek));
      }
      this.activeWeek = null;
    }

    private void ItemsControl_MouseEnter(object sender, MouseEventArgs e)
    {
      var week = this.Weeks[GetItem(e.GetPosition(this))];
      this.activeWeek = week;
      this.activeWeek.MouseOver = true;
      this.OnWeekEnter(new CalendarWeekEventArgs(week));

    }

    private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
    {
      var week = this.Weeks[GetItem(e.GetPosition(this))];
      if (this.activeWeek != week)
      {
        this.activeWeek.MouseOver = false;
        this.OnWeekLeave(new CalendarWeekEventArgs(this.activeWeek));
        this.activeWeek = week;
        this.activeWeek.MouseOver = true;
        this.OnWeekEnter(new CalendarWeekEventArgs(this.activeWeek));
      }
    }

    private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      this.Button_DoubleClick(sender, e);
      var day = this.Weeks[GetItem(e.GetPosition(this))];
      this.OnWeekDoubleClick(new CalendarWeekEventArgs(day));
    }

    private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
      this.clickWeek = this.Weeks[GetItem(e.GetPosition(this))];
      this.Button_Click(sender, e);
    }

    private void OnWeekClick(CalendarWeekEventArgs e)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekClick;
      handler?.Invoke(this, e);

    }

    private void OnWeekDoubleClick(CalendarWeekEventArgs e)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekDoubleClick;
      handler?.Invoke(this, e);
    }

    private void OnWeekLeave(CalendarWeekEventArgs e)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekLeave;
      handler?.Invoke(this, e);
    }

    private void OnWeekEnter(CalendarWeekEventArgs e)
    {
      EventHandler<CalendarWeekEventArgs> handler = WeekEnter;
      handler?.Invoke(this, e);
    }
  }

}