using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using Pabo.MonthCalendar.Common;
using System.Diagnostics;
using System.Globalization;
using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.Controls;
using Point = System.Windows.Point;
using Pabo.MonthCalendar.EventArgs;
using Pabo.MonthCalendar.Properties;
using System.Windows.Controls.Primitives;

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Host", Type = typeof(System.Windows.Controls.ItemsControl))]
  [ToolboxItem(false)]
  public class MonthView : ItemsControl<CalendarMonth>
  {

    public MonthView() : base(3,4)
    {
      this.popup = CreatePopup(this.Properties);
      this.Click += (sender, e) =>
      {
        this.OnMonthClick(new CalendarMonthEventArgs(this.activeItem));
      };
    }

    #region private members
 
    private int year;
    private List<Month> monthItems;

    private List<Month> prevSelected = new List<Month>();

    private DataTemplate template;

    #endregion

    #region constructor

    static MonthView()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MonthView), new FrameworkPropertyMetadata(typeof(MonthView)));

    }

    #endregion

    #region events

    internal event EventHandler<CalendarSelectionChangedEventArgs<Month>> SelectionChanged;

    internal event EventHandler<CalendarMonthEventArgs> MonthLeave;

    internal event EventHandler<CalendarMonthEventArgs> MonthEnter;

    internal event EventHandler<CalendarMonthEventArgs> MonthClick;

    internal event EventHandler<CalendarMonthEventArgs> MonthDoubleClick;


    #endregion

    #region dependency properties

    public static readonly DependencyProperty MonthsProperty = DependencyProperty.Register("Months",
               typeof(List<CalendarMonth>),
               typeof(MonthView),
               new FrameworkPropertyMetadata(new List<CalendarMonth>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMonthsChanged));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(MonthProperties),
               typeof(MonthView),
               new FrameworkPropertyMetadata(new MonthProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    #endregion

    #region event handlers

    private void PropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
    }


    #endregion


    #region private methods

  
    protected override void OnSelectionChanged()
    {
      var selectedMonths = this.Months.Where(x => x.Selected).ToList<Month>();

      var diff1 = selectedMonths.Except(this.prevSelected).ToList();
      var diff2 = this.prevSelected.Except(selectedMonths).ToList();
      if (diff1.Count() > 0 || diff2.Count > 0)
      {
        this.prevSelected = selectedMonths;
        foreach (CalendarMonth month in selectedMonths) { this.SetupSelectedBorders(month); };
        
        EventHandler<CalendarSelectionChangedEventArgs<Month>> handler = SelectionChanged;
        handler?.Invoke(this, new CalendarSelectionChangedEventArgs<Month>(selectedMonths));
      }
    }

    private void OnMonthClick(CalendarMonthEventArgs e)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthClick;
      handler?.Invoke(this, e);

    }

    protected override void OnItemLeave(CalendarMonth item)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthLeave;
      handler?.Invoke(this, new CalendarMonthEventArgs(item));

    }
    protected override void OnItemEnter(CalendarMonth item)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthEnter;
      handler?.Invoke(this, new CalendarMonthEventArgs(item));
    }

    protected override void OnItemDoubleClick(CalendarMonth item)
    {
      EventHandler<CalendarMonthEventArgs> handler = MonthDoubleClick;
      handler?.Invoke(this, new CalendarMonthEventArgs(item));
    }


    #endregion

    private static void OnMonthsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      MonthView view = d as MonthView;
      if (view != null)
        view.OnMonthsChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnMonthsChanged(object newValue, object oldValue)
    {
      base.Items = (List<CalendarMonth>)newValue;
    }

    #region properties
  
    internal List<CalendarMonth> Months
    {
      get
      {
        return (List<CalendarMonth>)this.GetValue(MonthsProperty);
      }
      set
      {
        this.SetValue(MonthsProperty, value);
      }
    }

    [Description("")]
    [Category("Header")]
    [Browsable(true)]
    internal MonthProperties Properties
    {
      get
      {
        return (MonthProperties)this.GetValue(PropertiesProperty);
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


    #region public methods

    #endregion

    #region private methods

    protected override void Setup()
    {
      SetupMonths(this.year, this.monthItems, this.template);
    }

    internal void SetupMonths(int year, List<Month> items, DataTemplate template)
    {

      this.year = year;
      this.monthItems = items;
      this.template = template;

      if (!SuspendLayout)
      {
        var months = new CalendarMonth[12];

        for (int i = 0;i <12;i++)
        {
          months[i] = new CalendarMonth(year, i+1);

          //var disabled = this.disabledMonths.FirstOrDefault(x => x == days[i].Date);
          //var selected = this.Days.FirstOrDefault(x => x.Selected && x.Date == days[i].Date);

          months[i].Template = this.template;

          months[i].Selected = this.Months.FirstOrDefault(x => x.Selected && x.Year == months[i].Year && x.Number == months[i].Number) != null;
          //months[i].Disabled = disabled; 

          if (months[i].Selected)
            this.SetupSelectedBorders(months[i]);

        }

        foreach (Month item in items)
        {
          var month = months.FirstOrDefault(x => x.Year == item.Year && x.Number == item.Number);
          if (month != null)
          {
            Utils.CopyProperties<MonthProperties, CalendarMonth>(Properties, month);
            Utils.CopyProperties<Month, CalendarMonth>(item, month);
          }
        }

        this.Months = months.ToList();
      }

    }

  }

  #endregion
}
