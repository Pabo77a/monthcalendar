using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using Pabo.MonthCalendar.Model;
using System.Windows.Media;
using Pabo.MonthCalendar.Properties;
using System.Windows.Controls;

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

    private void Setup()
    {
      this.Width = Properties.FontSize + 25;
    }
  }

}