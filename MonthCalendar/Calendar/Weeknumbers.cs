using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar
{
  [ToolboxItem(false)]
  internal class Weeknumbers : BaseControl
  {

    #region dependency properties

    public static readonly DependencyProperty WeeksProperty = DependencyProperty.Register("Weeks",
               typeof(List<Week>),
               typeof(Weeknumbers),
               new FrameworkPropertyMetadata(new List<Week>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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
    }

    #endregion


    #region properties

    internal List<Week> Weeks
    {
      get
      {
        return (List<Week>)this.GetValue(WeeksProperty);
      }
      set
      {
        this.SetValue(WeeksProperty, value);
      }
    }

    #endregion

    internal void SetupWeeks(DateTime firstDate)
    {
      var weeks = new List<Week>();
      var date = firstDate;
      for (int i = 0;i<6;i++)
      {
        weeks.Add(new Week(date));
        date = date.AddDays(7);
      }

      this.Weeks = weeks;
      
    }
  }
}