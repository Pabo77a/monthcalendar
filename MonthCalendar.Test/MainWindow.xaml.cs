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
    ObservableCollection<DateTime> disabledDays = new ObservableCollection<DateTime>();
    TrulyObservableCollection<Week> weeks = new TrulyObservableCollection<Week>();
    TrulyObservableCollection<Weekday> weekdays = new TrulyObservableCollection<Weekday>();

    HeaderProperties headerProperties = new HeaderProperties();
    FooterProperties footerProperties = new FooterProperties();
    WeeknumberProperties weeknumberProperties = new WeeknumberProperties();
    WeekdaysProperties weekdaysProperties = new WeekdaysProperties();
    CalendarProperties calendarProperties = new CalendarProperties();

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


      this.days.Add(new Day() { Date = new DateTime(2020, 6, 6), BackgroundColor = Colors.ForestGreen, Text = "HEJ HEJ" });
      this.days.Add(new Day() { Date = new DateTime(2020, 10, 21), BackgroundColor = Colors.BlueViolet });
      this.days.Add(new Day() { Date = new DateTime(2020, 10, 11), BackgroundColor = Colors.Orange });

      this.days.Add(new Day()
      {
        Date = new DateTime(2020, 8, 13),
        BackgroundColor = Colors.HotPink,
        DateColor = Colors.Yellow,
        Image = new BitmapImage(new Uri("pack://application:,,,/Resources/star.png", UriKind.Absolute)),
        DateFontSize = 22,
        Text = "BIDEN",
        Tooltip = "This is a test.",
        TextColor = Colors.White,
        TextFontSize = 36 //,
        //Template = (DataTemplate)Application.Current.FindResource("myDayTemplate2")
    });
      this.days.Add(new Day() { Date = new DateTime(2020, 8, 16), Tooltip = "Make america great again!", 
        BackgroundColor = Colors.Linen, DateColor = Colors.DarkOrange, Text = "TRUMP", TextColor = Colors.Ivory, TextFontSize = 22 });

      this.days.Add(new Day() { Date = new DateTime(2020, 7, 28), DateColor = Colors.Red, Text = "yyy", TextColor = Colors.Black });


      this.weeks.Add(new Week() { TextColor = Colors.White, BackgroundColor = Colors.Green, TextFontWeight = FontWeights.Bold, 
        Number = 32, Year = 2020, Tooltip = "Denna vecka är den först i ...." });

        this.weekdays.Add(new Weekday() { /*Template = (DataTemplate)Application.Current.FindResource("myWeekdayTemplate"),*/
          BackgroundColor = Colors.Orange,
          Tooltip = "Detta är en veckodag.",
          TextColor = Colors.White, TextFontSize = 16, TextFontWeight = FontWeights.Bold, Year = 2020, Month = 8, DayOfWeek = DayOfWeek.Monday });

      //this.weeknumberProperties.TextColor = Colors.DarkOliveGreen;
      //this.weeknumberProperties.FontSize = 32;
      //this.weeknumberProperties.FontWeight = FontWeights.Bold;
      //this.weeknumberProperties.TextDecoration = "Underline";
      //this.weeknumberProperties.BackgroundColor = Colors.LightBlue;
      //this.headerProperties.BackgroundColor = Colors.Lavender;
      this.weekdaysProperties.BackgroundColor = Colors.DarkSeaGreen;
      //this.weekdaysProperties.FontWeight = FontWeights.Bold;
      //this.weekdaysProperties.TextDecoration = "Strikethrough";
      //this.weekdaysProperties.TextColor = Colors.Firebrick;
      //this.weekdaysProperties.FontSize = 30;
      //this.HeaderProperties.DateText = "jjj"; // .Text = "TEST TEST TEST";

      //this.weekdaysProperties.AbbreviatedNames = false;

      this.calendarProperties.ShowNotCurrentMonth = true;

      this.calendarProperties.DateFontSize = 24;
      this.calendarProperties.DateColor = Colors.Green;
      this.calendarProperties.DateTextDecoration = "Underline";
      this.calendarProperties.DateFontStyle = FontStyles.Italic;
      this.calendarProperties.DateFontWeight = FontWeights.Bold;
      this.calendarProperties.SelectedBackgroundColor = Colors.Pink;
      this.calendarProperties.SelectedBackgroundColor = Colors.Red;

      //this.calendarProperties.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/bricks.jpeg", UriKind.Absolute));

      this.calendarProperties.NotCurrentMonthBackgroundColor = Colors.Gray;
      this.calendarProperties.NotCurrentMonthDateColor = Colors.White;
      MyCalendar.DataContext = this;
      
    }

    public TrulyObservableCollection<Day> Days => days;

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

    public WeeknumberProperties WeeknumberProperties => weeknumberProperties;

    public WeekdaysProperties WeekdaysProperties => weekdaysProperties;

    private void MonthCalendar_SelectionChanged(object sender, Pabo.MonthCalendar.EventArgs.SelectionChangedEventArgs e)
    {
      var  i = 1;
    }

    private void MonthCalendar_MonthChanged(object sender, Pabo.MonthCalendar.EventArgs.MonthChangedEventArgs e)
    {
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      /*this.headerProperties.Text = "Uj UJ UJ";
      var day = this.days.FirstOrDefault(x => x.Date == new DateTime(2020, 8, 13));
      if (day != null)
      {
        day.TextColor = Colors.Blue;
      }
      var week = this.weeks.FirstOrDefault(x => x.Number == 32);
      if (day != null)
      {
        week.TextColor = Colors.Red;
      }

      MyCalendar.SuspendLayout();*/

      //this.WeeknumberProperties.TextColor = Colors.Purple;
      //this.WeeknumberProperties.TextFontSize = 24;
      //this.WeeknumberProperties.TextFontWeight = FontWeights.Bold;
      //this.WeekdaysProperties.TextColor = Colors.Purple;
      //this.WeekdaysProperties.TextFontSize = 24;
      //this.WeekdaysProperties.TextFontWeight = FontWeights.Bold;

      //this.CalendarProperties.DateColor = Colors.Blue;
      //this.CalendarProperties.DateFontSize = 10;

      //MyCalendar.ResumeLayout();

      //this.MyCalendar.Refresh();

      //MyCalendar.Select(new List<DateTime>() { new DateTime(2020, 8, 7), new DateTime(2020, 8, 8) });
      //MyCalendar.Select(new List<DateTime>() { new DateTime(2020, 8, 15), new DateTime(2020, 8, 16) });
      //MyCalendar.Deselect(new List<DateTime>() { new DateTime(2020, 8, 7), new DateTime(2020, 8, 8) });

      //MyCalendar.SelectWeek(34);
      //MyCalendar.DeselectWeek(34);

      //MyCalendar.SelectWeekday(DayOfWeek.Thursday);
      //MyCalendar.DeselectWeekday(DayOfWeek.Thursday);

      //MyCalendar.MinDate = new DateTime(2020, 8, 12);

      //this.MinDate = new DateTime(2020, 8, 16);

      this.DisabledDays = new ObservableCollection<DateTime>() { new DateTime(2020, 8, 12), new DateTime(2020, 8, 16) };

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
      //Debug.WriteLine("Leave=" + e.Weekday.DayOfWeek.ToString());
    }

    private void MonthCalendar_WeekdayEnter(object sender, Pabo.MonthCalendar.EventArgs.WeekdayEventArgs e)
    {
      //Debug.WriteLine("Enter=" + e.Weekday.DayOfWeek.ToString());
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
  }
}
