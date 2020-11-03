using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using Pabo.MonthCalendar.Model;

namespace Pabo.MonthCalendar
{
  [ToolboxItem(false)]
  internal class Weekdays : BaseControl
  {

    #region dependency properties

    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(List<Weekday>),
               typeof(Weekdays),
               new FrameworkPropertyMetadata(new List<Weekday>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region constructor

    static Weekdays()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Weekdays), new FrameworkPropertyMetadata(typeof(Weekdays)));

    }

    #endregion


    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.Height = this.FontSize + 20;
    }

    #endregion


    #region properties

    internal List<Weekday> Days
    {
      get
      {
        return (List<Weekday>)this.GetValue(DaysProperty);
      }
      set
      {
        this.SetValue(DaysProperty, value);
      }
    }

    #endregion

    internal void SetupDays()
    {

      List<Weekday> days = new List<Weekday>();

      CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
      DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

      for (int i = (int)firstDayOfWeek; i <= (int)DayOfWeek.Saturday; i++)
      {
        days.Add(new Weekday((DayOfWeek)Enum.Parse(typeof(DayOfWeek), i.ToString())));
      }
      if (firstDayOfWeek == DayOfWeek.Monday)
      {
        days.Add(new Weekday(DayOfWeek.Sunday));
      }

      this.Days = days.ToList<Weekday>();
    }
  }
}
