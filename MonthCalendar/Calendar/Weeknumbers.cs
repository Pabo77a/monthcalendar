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


namespace Pabo.MonthCalendar
{
  [ToolboxItem(false)]
  internal class Weeknumbers : BaseControl
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
      this.Width = Properties.FontSize + 25;
    }
  }

}