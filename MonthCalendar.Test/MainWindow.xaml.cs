using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pabo.MonthCalendar.Controls;
using Pabo.MonthCalendar.Model;
using Pabo.MonthCalendar.Properties;

namespace MonthCalendar.Test
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, INotifyPropertyChanged
  {
    TrulyObservableCollection<Day> days = new TrulyObservableCollection<Day>();
    TrulyObservableCollection<Month> months = new TrulyObservableCollection<Month>();
    ObservableCollection<DateTime> disabledDays = new ObservableCollection<DateTime>();
    TrulyObservableCollection<Week> weeks = new TrulyObservableCollection<Week>();
    TrulyObservableCollection<Weekday> weekdays = new TrulyObservableCollection<Weekday>();

    HeaderProperties headerProperties = new HeaderProperties();
    FooterProperties footerProperties = new FooterProperties();
    WeeknumberProperties weeknumberProperties = new WeeknumberProperties();
    WeekdaysProperties weekdaysProperties = new WeekdaysProperties();
    CalendarProperties calendarProperties = new CalendarProperties();
    MonthProperties monthProperties = new MonthProperties();

    private DateTime minDate = DateTime.MinValue;
    private DateTime maxDate = DateTime.MaxValue;

    public MainWindow()
    {
      InitializeComponent();
      this.DataContext = this;
      this.Setup();
    }

    private void Setup()
    {

      var year = DateTime.Now.Year;
      var month = DateTime.Now.Month;

      this.MyCalendar.Year = year;
      this.MyCalendar.Month = month;
      
      this.days.Add(new Day() { Date = new DateTime(year, month, 6), BackgroundColor = Colors.LightGreen, Text = "Dentist",
        Image = new BitmapImage(new Uri("pack://application:,,,/Resources/star.png", UriKind.Absolute))
      });
      this.days.Add(new Day() { Date = new DateTime(year, month, 21), Text = "Party!!",
        Image = new BitmapImage(new Uri("pack://application:,,,/Resources/present.png", UriKind.Absolute))
      });
      this.days.Add(new Day() { Date = new DateTime(year, month, 11), BackgroundColor = Colors.Orange, Text = "Pizza", 
        Image = new BitmapImage(new Uri("pack://application:,,,/Resources/pizza.png", UriKind.Absolute)) });

      this.days.Add(new Day() { Date = new DateTime(year, month, 16), BackgroundColor = Colors.LightYellow, Text = "Match",
        Image = new BitmapImage(new Uri("pack://application:,,,/Resources/soccer.png", UriKind.Absolute))
      });

      this.DisabledDays = new ObservableCollection<DateTime>() { new DateTime(year, month, 25), new DateTime(year, month, 26) };

      this.weeks.Add(new Week() { TextColor = Colors.White, BackgroundColor = Colors.Green, TextFontWeight = FontWeights.Bold, 
         Number = 32, Year = year });

      this.weekdays.Add(new Weekday() { BackgroundColor = Colors.Wheat, 
        TextColor = Colors.White, TextFontSize = 16, TextFontWeight = FontWeights.Bold, Year = year, Month = month, DayOfWeek = DayOfWeek.Tuesday });

      this.calendarProperties.ShowNotCurrentMonth = true;

      this.calendarProperties.DateFontSize = 24;
      this.calendarProperties.TextColor = Colors.HotPink;
      this.calendarProperties.DateColor = Colors.Green;
      this.calendarProperties.DateTextDecoration = "Underline";
      this.calendarProperties.DateFontStyle = FontStyles.Italic;
      this.calendarProperties.DateFontWeight = FontWeights.Bold;
      this.MonthProperties.SelectedBackgroundColor = Colors.LightBlue;
      this.calendarProperties.SelectedBackgroundColor = Colors.LightBlue;

      this.calendarProperties.NotCurrentMonthBackgroundColor = Colors.Gray;
      this.calendarProperties.NotCurrentMonthDateColor = Colors.White;

      this.monthProperties.AbbreviatedNames = true;
      this.calendarProperties.TextColor = Colors.Pink;

      this.months.Add(new Month()
      {
        Number = 10,
        Year = year,
        BackgroundColor = Colors.Orchid,
        Image = new BitmapImage(new Uri("pack://application:,,,/Resources/star.png", UriKind.Absolute)),
      });
      this.months.Add(new Month()
      {
        Number = 6,
        Year = year,
        BackgroundColor = Colors.Beige
      });

      MyCalendar.DataContext = this;
      
    }

    public TrulyObservableCollection<Day> Days => days;

    public TrulyObservableCollection<Month> Months => months;

    public ObservableCollection<DateTime> DisabledDays
    {
      get => disabledDays;
      set
      {
        if (value != disabledDays)
        {
          disabledDays = value;
          OnPropertyChanged(nameof(this.DisabledDays));
        }
      }
    }

    public TrulyObservableCollection<Week> Weeks => weeks;

    public TrulyObservableCollection<Weekday> Weekdays => weekdays;

    public HeaderProperties HeaderProperties => headerProperties;

    public CalendarProperties CalendarProperties
    {
      get => calendarProperties;
      set
      {
        calendarProperties = value;
        OnPropertyChanged(nameof(this.CalendarProperties));
      }
    }

    public DateTime MinDate
    {
      get => minDate;
      set
      {
        if (value != minDate)
        {
          this.minDate = value;
          OnPropertyChanged(nameof(this.MinDate));
        }
      }
    }

    public DateTime MaxDate
    {
      get => maxDate;
      set
      {
        if (value != maxDate)
        {
          this.maxDate = value;
          OnPropertyChanged(nameof(this.MaxDate));
        }
      }
    }

    public FooterProperties FooterProperties => footerProperties;

    public MonthProperties MonthProperties => monthProperties;

    public WeeknumberProperties WeeknumberProperties => weeknumberProperties;

    public WeekdaysProperties WeekdaysProperties => weekdaysProperties;

    private void MonthCalendar_MonthChanged(object sender, Pabo.MonthCalendar.EventArgs.MonthChangedEventArgs e)
    {
    }
       

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void MonthCalendar_DayLeave(object sender, Pabo.MonthCalendar.EventArgs.DayEventArgs e)
    {
    }

    private void MonthCalendar_DayEnter(object sender, Pabo.MonthCalendar.EventArgs.DayEventArgs e)
    {
    }

    private void MonthCalendar_WeekLeave(object sender, Pabo.MonthCalendar.EventArgs.WeekEventArgs e)
    {
    }

    private void MonthCalendar_WeekEnter(object sender, Pabo.MonthCalendar.EventArgs.WeekEventArgs e)
    {
    }

    private void MonthCalendar_WeekdayLeave(object sender, Pabo.MonthCalendar.EventArgs.WeekdayEventArgs e)
    {
    }

    private void MonthCalendar_WeekdayEnter(object sender, Pabo.MonthCalendar.EventArgs.WeekdayEventArgs e)
    {
    }

    private void MonthCalendar_FooterEnter(object sender, RoutedEventArgs e)
    {
    }

    private void MonthCalendar_FooterLeave(object sender, RoutedEventArgs e)
    {
    }

    private void MonthCalendar_DayClick(object sender, Pabo.MonthCalendar.EventArgs.DayEventArgs e)
    {
    }

    private void MonthCalendar_DayDoubleClick(object sender, Pabo.MonthCalendar.EventArgs.DayEventArgs e)
    {
    }

    private void MonthCalendar_WeekClick(object sender, Pabo.MonthCalendar.EventArgs.WeekEventArgs e)
    {
    }

    private void MonthCalendar_WeekDoubleClick(object sender, Pabo.MonthCalendar.EventArgs.WeekEventArgs e)
    {
    }

    private void MonthCalendar_WeekdayClick(object sender, Pabo.MonthCalendar.EventArgs.WeekdayEventArgs e)
    {
    }

    private void MonthCalendar_WeekdayDoubleClick(object sender, Pabo.MonthCalendar.EventArgs.WeekdayEventArgs e)
    {
    }

    private void MyCalendar_DaySelectionChanged(object sender, Pabo.MonthCalendar.EventArgs.SelectionChangedEventArgs<Day> e)
    {
    }

    private void MyCalendar_MonthSelectionChanged(object sender, Pabo.MonthCalendar.EventArgs.SelectionChangedEventArgs<Month> e)
    {
    }

    private void MyCalendar_MonthClick(object sender, Pabo.MonthCalendar.EventArgs.MonthEventArgs e)
    {
    }

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        this.MyCalendar.VisualMode = sender == mode1
        ? Pabo.MonthCalendar.Common.VisualMode.Days 
        : Pabo.MonthCalendar.Common.VisualMode.Months;
    }
  }
}
