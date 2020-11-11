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
  internal class Weeknumbers :PanelControl
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
    private ItemsControl itemsControl;


    public Weeknumbers() : base(1,6)
    {

    }

    #region events

    internal event EventHandler<CalendarWeekEventArgs> WeekLeave;

    internal event EventHandler<CalendarWeekEventArgs> WeekEnter;

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
      this.Width = this.FontSize + 25;

      this.Properties.PropertyChanged += Properties_PropertyChanged;

      this.itemsControl = GetTemplateChild("PART_Host") as ItemsControl;
      if (this.itemsControl != null)
      {
        this.itemsControl.MouseMove += ItemsControl_MouseMove;
        this.itemsControl.MouseEnter += ItemsControl_MouseEnter;
        this.itemsControl.MouseLeave += ItemsControl_MouseLeave;
      }
 
  }

    private void Properties_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
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
        Setup();
      }
    }

    #endregion

    internal void SetupWeeks(DateTime firstDate, List<Week> weekItems)
    {
      var year = firstDate.Year;
    
      var weeks = new List<CalendarWeek>();
      var date = firstDate;
      for (int i = 0;i<6;i++)
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

    private void Setup()
    {
      this.Width = Properties.TextFontSize + 25;
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
      var week = this.Weeks[GetPanel(e.GetPosition(this))];
      this.activeWeek = week;
      this.activeWeek.MouseOver = true;
      this.OnWeekEnter(new CalendarWeekEventArgs(week));

    }

    private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
    {
      var week = this.Weeks[GetPanel(e.GetPosition(this))];
      if (this.activeWeek != week)
      {
        this.activeWeek.MouseOver = false;
        this.OnWeekLeave(new CalendarWeekEventArgs(this.activeWeek));
        this.activeWeek = week;
        this.activeWeek.MouseOver = true;
        this.OnWeekEnter(new CalendarWeekEventArgs(this.activeWeek));
      }
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