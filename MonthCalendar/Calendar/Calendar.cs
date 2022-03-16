using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using Pabo.MonthCalendar.Common;
using System.Globalization;
using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.EventArgs;
using Pabo.MonthCalendar.Properties;

namespace Pabo.MonthCalendar
{
  [TemplatePart(Name = "PART_Host", Type = typeof(System.Windows.Controls.ItemsControl))]
  [ToolboxItem(false)]
  public class Calendar : ItemsControl<CalendarDay>
  {

    public Calendar() : base(7, 6)
    {
      this.popup = CreatePopup(this.Properties);
      this.Click += (sender, e) =>
      {
        this.OnDayClick(new CalendarDayEventArgs(clickItem));
      };
    }

    #region private members

    private int year;
    private int month;
    private List<Day> dayItems;
    private List<DateTime> disabledDays;
    private DateTime minDate;
    private DateTime maxDate;

    private List<Day> prevSelected = new List<Day>();

    private DataTemplate template;

    #endregion



    #region constructor

    static Calendar()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));

    }

    #endregion


    #region events

    internal event EventHandler<CalendarSelectionChangedEventArgs<Day>> SelectionChanged;

    internal event EventHandler<CalendarDayEventArgs> DayLeave;

    internal event EventHandler<CalendarDayEventArgs> DayEnter;

    internal event EventHandler<CalendarDayEventArgs> DayClick;

    internal event EventHandler<CalendarDayEventArgs> DayDoubleClick;


    #endregion

    #region dependency properties

    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
               typeof(List<CalendarDay>),
               typeof(Calendar),
               new FrameworkPropertyMetadata(new List<CalendarDay>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDaysChanged));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(CalendarProperties),
               typeof(Calendar),
               new FrameworkPropertyMetadata(new CalendarProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    #endregion

    #region event handlers


    private void PropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Setup();
    }


    #endregion


    private static void OnDaysChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      Calendar view = d as Calendar;
      if (view != null)
        view.OnDaysChanged(e.NewValue, e.OldValue);
    }

    protected virtual void OnDaysChanged(object newValue, object oldValue)
    {
      base.Items = (List<CalendarDay>)newValue;
    }


    #region private methods
    

    protected override void OnSelectionChanged(List<CalendarDay> selected)
    {
      EventHandler<CalendarSelectionChangedEventArgs<Day>> handler = SelectionChanged;
      handler?.Invoke(this, new CalendarSelectionChangedEventArgs<Day>(selected.ToList<Day>()));
    }

    protected override void OnItemLeave(CalendarDay item)
    {
      EventHandler<CalendarDayEventArgs> handler = DayLeave;
      handler?.Invoke(this, new CalendarDayEventArgs(item));

    }
    protected override void OnItemEnter(CalendarDay item)
    {
      EventHandler<CalendarDayEventArgs> handler = DayEnter;
      handler?.Invoke(this, new CalendarDayEventArgs(item));
    }

    protected override void OnItemDoubleClick(CalendarDay item)
    {
      EventHandler<CalendarDayEventArgs> handler =DayDoubleClick;
      handler?.Invoke(this, new CalendarDayEventArgs(item));
    }

    private void OnDayClick(CalendarDayEventArgs e)
    {
      EventHandler<CalendarDayEventArgs> handler = DayClick;
      handler?.Invoke(this, e);

    }

    #endregion

    #region properties

    internal List<CalendarDay> Days
    {
      get
      {
        return (List<CalendarDay>)this.GetValue(DaysProperty);
      }
      set
      {
        this.SetValue(DaysProperty, value);
      }
    }
 
    [Description("")]
    [Category("Calendar")]
    [Browsable(true)]
    internal CalendarProperties Properties
    {
      get
      {
        return (CalendarProperties)this.GetValue(PropertiesProperty);
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

    public void Select(List<DateTime> dates)
    {
      foreach (DateTime date in dates)
      {
        var day = this.Days.FirstOrDefault(x => x.Date == date);
        if (day != null)
        {
          day.Selected = true;
        }
      }

      this.CheckIfSelectionChanged();
    }

    public void Deselect(List<DateTime> dates)
    {
      foreach (DateTime date in dates)
      {
        var day = this.Days.FirstOrDefault(x => x.Date == date);
        if (day != null)
        {
          day.Selected = false;
        }
      }

      this.CheckIfSelectionChanged();
    }

    public void SelectWeek(int week)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (ISOWeek.GetWeekOfYear(day.Date) == week)
        {
          day.Selected = true;
        }
      }

      this.CheckIfSelectionChanged();
    }

    public void DeselectWeek(int week)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (ISOWeek.GetWeekOfYear(day.Date) == week)
        {
          day.Selected = false;
        }
      }

      this.CheckIfSelectionChanged();
    }

    public void SelectWeekday(DayOfWeek weekday)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (day.Date.DayOfWeek == weekday)
        {
          day.Selected = true;
        }
      }

      this.CheckIfSelectionChanged();
    }

    public void DeselectWeekday(DayOfWeek weekday)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (day.Date.DayOfWeek == weekday)
        {
          day.Selected = false;
        }
      }

      this.CheckIfSelectionChanged();
    }

    #endregion

    #region private methods

    protected override void Setup()
    {
      SetupDays(this.year, this.month, this.minDate, this.maxDate, this.dayItems, this.disabledDays, this.template);
    }

    internal void SetupDays(int year, int month, DateTime minDate, DateTime maxDate, List<Day> items, List<DateTime> disabledDays, DataTemplate template)
    {

      this.year = year;
      this.month = month;
      this.dayItems = items;
      this.minDate = minDate;
      this.maxDate = maxDate;
      this.disabledDays = disabledDays;
      this.template = template;

      if (!SuspendLayout)
      {
        var firstDay = new DateTime(year, month, 1);
        items = items.Where(x => x.Date > firstDay.AddDays(-15) && x.Date < firstDay.AddDays(45)).ToList();

        CalendarDay[] days = new CalendarDay[42];

        CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
        DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

        var firstDayInMonth = new DateTime(year, month, 1);
        DayOfWeek firstDayOfMonth = firstDayInMonth.DayOfWeek;

        var startPos = (int)firstDayOfMonth + (1 - (int)firstDayOfWeek) - 1;
        if (startPos == -1)
        {
          startPos = 6;
        }
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var endPos = startPos + daysInMonth;

        var date = firstDayInMonth;
        for (int i = startPos; i < endPos; i++)
        {
          days[i] = new CalendarDay(date);
          date = date.AddDays(1);
        }

        date = firstDayInMonth.AddDays(-1);
        for (int i = startPos - 1; i >= 0; i--)
        {
          days[i] = new CalendarDay(date);
          date = date.AddDays(-1);
        }

        date = firstDayInMonth.AddDays(daysInMonth);
        for (int i = endPos; i < 42; i++)
        {
          days[i] = new CalendarDay(date);
          date = date.AddDays(1);
        }

        for (int i = 0; i < 42; i++)
        {

          var disabled = this.disabledDays.FirstOrDefault(x => x == days[i].Date);
          //var selected = this.Days.FirstOrDefault(x => x.Selected && x.Date == days[i].Date);

          days[i].Template = this.template;

          days[i].Selected = this.Days.FirstOrDefault(x => x.Selected && x.Date == days[i].Date) != null;
          days[i].Disabled = days[i].Date <= minDate || days[i].Date >= maxDate || disabled != default(DateTime);
          days[i].NotCurrentMonth = days[i].Date.Month != month;
          days[i].Visible = !days[i].NotCurrentMonth || (days[i].NotCurrentMonth && Properties.ShowNotCurrentMonth);
          if (!days[i].Disabled)
          {
     
            days[i].DateColor = days[i].NotCurrentMonth ? Properties.NotCurrentMonthDateColor : Properties.DateColor;
            days[i].BackgroundColor = Properties.BackgroundImage != null ?
                    Colors.Transparent :
                    days[i].NotCurrentMonth ? Properties.NotCurrentMonthBackgroundColor : Properties.BackgroundColor;
          }

          days[i].DateFontFamily = Properties.DateFontFamily;
          days[i].DateFontSize = Properties.DateFontSize;
          days[i].DateFontStyle = Properties.DateFontStyle;
          days[i].DateFontWeight = Properties.DateFontWeight;
          days[i].DateTextDecoration = Properties.DateTextDecoration;
          days[i].DateMargin = Properties.DateMargin;
          days[i].DateVerticalAlignment = Properties.DateVerticalAlignment;
          days[i].DateHorizontalAlignment = Properties.DateHorizontalAlignment;

          days[i].ImageMargin = Properties.ImageMargin;
          days[i].ImageVerticalAlignment = Properties.ImageVerticalAlignment;
          days[i].ImageHorizontalAlignment = Properties.ImageHorizontalAlignment;


          days[i].TextColor = Properties.TextColor;
          days[i].TextFontFamily = Properties.TextFontFamily;
          days[i].TextFontSize = Properties.TextFontSize;
          days[i].TextFontStyle = Properties.TextFontStyle;
          days[i].TextFontWeight = Properties.TextFontWeight;
          days[i].TextTextDecoration = Properties.TextTextDecoration;
          days[i].TextMargin = Properties.TextMargin;
          days[i].TextVerticalAlignment = Properties.TextVerticalAlignment;
          days[i].TextHorizontalAlignment = Properties.TextHorizontalAlignment;
        
          if (days[i].Selected)
            this.SetupSelectedBorders(days[i]);
        }

        foreach (Day item in items)
        {
          var day = days.FirstOrDefault(x => x.Date == item.Date);
          if (day != null)
          {
            Utils.CopyProperties<CalendarProperties, CalendarDay>(Properties, day);
            Utils.CopyProperties<Day, CalendarDay>(item, day);
          }
        }

        this.Days = days.ToList<CalendarDay>();
      }

    }

  }

  #endregion
}
