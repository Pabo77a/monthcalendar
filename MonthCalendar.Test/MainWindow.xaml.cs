﻿using System;
using System.Collections.Generic;
using System.Linq;
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
  public partial class MainWindow : Window
  {
    TrulyObservableCollection<Day> days = new TrulyObservableCollection<Day>();
    TrulyObservableCollection<Week> weeks = new TrulyObservableCollection<Week>();
    TrulyObservableCollection<Weekday> weekdays = new TrulyObservableCollection<Weekday>();
  
    HeaderProperties headerProperties = new HeaderProperties();
    FooterProperties footerProperties = new FooterProperties();
    WeeknumberProperties weeknumberProperties = new WeeknumberProperties();
    WeekdaysProperties weekdaysProperties = new WeekdaysProperties();
    CalendarProperties calendarProperties = new CalendarProperties();

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

      this.days.Add(new Day() { Date = new DateTime(2020, 8, 13), BackgroundColor = Colors.HotPink, DateColor=Colors.Yellow, DateFontSize=22, Text = "BIDEN", TextColor = Colors.White, TextFontSize = 36 });
      this.days.Add(new Day() { Date = new DateTime(2020, 8, 16), BackgroundColor = Colors.Linen, DateColor = Colors.DarkOrange, Text = "TRUMP", TextColor = Colors.Ivory, TextFontSize = 22 });

      this.days.Add(new Day() { Date = new DateTime(2020, 7, 28), DateColor = Colors.Red, Text = "yyy", TextColor = Colors.Black});


      this.weeks.Add(new Week() { TextColor = Colors.White, BackgroundColor=Colors.Green, FontWeight=FontWeights.Bold, Number= 32, Year=2020 });

      this.weekdays.Add(new Weekday() { BackgroundColor = Colors.Green, TextColor = Colors.White, FontSize = 16, FontWeight = FontWeights.Bold, Year = 2020, Month = 8, DayOfWeek = DayOfWeek.Tuesday });
      
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

      this.calendarProperties.DateFontSize = 24;
      this.calendarProperties.DateColor = Colors.Green;
      this.calendarProperties.DateTextDecoration = "Underline";
      this.calendarProperties.DateFontStyle = FontStyles.Italic;
      this.calendarProperties.DateFontWeight = FontWeights.Bold;
      this.calendarProperties.SelectedBackgroundColor = Colors.Pink;
      this.calendarProperties.SelectedBackgroundColor = Colors.Red;

      //this.calendarProperties.BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/bricks.jpeg", UriKind.Absolute));

      this.calendarProperties.TrailingBackgroundColor = Colors.Gray;
      this.calendarProperties.TrailingDateColor = Colors.White;
    }

    public TrulyObservableCollection<Day> Days => days;

    public TrulyObservableCollection<Week> Weeks => weeks;

    public TrulyObservableCollection<Weekday> Weekdays => weekdays;

    public HeaderProperties HeaderProperties => headerProperties;

    public CalendarProperties CalendarProperties => calendarProperties;

    public FooterProperties FooterProperties => footerProperties;

    public WeeknumberProperties WeeknumberProperties => weeknumberProperties;

    public WeekdaysProperties WeekdaysProperties => weekdaysProperties;

    private void MonthCalendar_SelectionChanged(object sender, Pabo.MonthCalendar.EventArgs.SelectionChangedEventArgs e)
    {
      int i = 1;
    }

    private void MonthCalendar_MonthChanged(object sender, Pabo.MonthCalendar.EventArgs.MonthChangedEventArgs e)
    {
      int i = 1;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      //this.headerProperties.Text = "Uj UJ UJ";
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
    }
  }
}
