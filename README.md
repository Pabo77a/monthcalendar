# Another monthcalendar
This is a custom monthcalendar for WPF very similar to the [winforms month calender](https://www.codeproject.com/Articles/10840/Another-Month-Calendar) that I published way back in 2006. The calendar supports selection of both days and months as well as custom templates for days, months, weeknumbers and weekdays. 

![demo](https://user-images.githubusercontent.com/92783962/158376402-cdfaf00b-f9c6-4f56-a719-af156ef32bbe.png)

# Getting started
use NuGet to install
```cmd
dotnet add package Pabo.MonthCalendar
```
Add XAML namespace
```xaml
xmlns:calendar="clr-namespace:Pabo.MonthCalendar;assembly=Pabo.MonthCalendar"
```

# Layout and design

The basic design for the control is pretty much like the standard Visual Studio Calendar, but it is a lot more flexible. The Calendar basically consists of five
different regions, each with its own set of properties.

![calendar2](https://user-images.githubusercontent.com/92783962/157632367-66582740-88a4-4563-92fb-e6d54b78a1f5.png)

The only region that must be visible is the calendar region, all the others can be switched on or off.

# Properties

![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **VisualMode**: Indicates what should be selected (days or months)\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **MinDate**: Min selectable date\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **MaxDate**: Max selectable date\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **MinYear**: Min selectable year (months selection)\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **MaxYear**: Max selectable year (months selection)\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Header**: Header visibility\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Footer**: Footer visibility\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Weekdays**: Weekdays visibility (days selection)\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Weeknumbers**: Weeknumbers visibility (days selection)\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Month**: Current month\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Year**: Current year (months selection)\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **SelectionMode**: Current selection mode (None, Single or Multiple)\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **CalendarProperties**: Default appearance and behavior for calendar view\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **MonthProperties**: Default appearance and behavior for month view\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **WeekdaysProperties**: Default appearance and behavior for weekdays\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **WeeknumberProperties**: Default appearance and behavior for weeknumbers\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **HeaderProperties**: Default appearance and behavior for header\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **FooterProperties**: Default appearance and behavior for footer\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Days**: Collection of formatted days\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **DisabledDays**: Collection of invalid/non selectable dates.\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Months**: Collection of formatted months (Months selection) \
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **Weeks**: Collection of formatted weeks\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **DayOfWeek**: Collection of formated weekdays\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **DayTemplate**: Custom data template overriding default day appearance\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **MonthTemplate**: Custom data template overriding default month appearance\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **WeekTemplate**: Custom data template overriding default week appearance\
![property2](https://user-images.githubusercontent.com/92783962/157639682-3b2cfa79-1d76-4f86-b78f-de53b0d713fb.png) **WeekdayTemplate**: Custom data template overriding default weekday appearance

# Events
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **MonthSelectionChanged**: Indicates month selection was changed\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **DaySelectionChanged**: Indicates day selection was changed\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekClick**: Indicates a click on a specific week\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekDoubleClick**: Indicates a double click on a specific week\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekdayClick**: Indicates a click on a specific weekday\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekdayDoubleClick**: Indicates a double clicked on a specific weelday\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **DayClick**: Indicates a click on a specific day\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **DayDoubleClick**: Indicates a double click on a specific day\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **DayLeave**: Indicates mouse leaving a specific day\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **DayEnter**: Indicates mouse entering a specific day\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekLeave**: Indicates mouse leaving a specific week\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekEnter**: Indicates mouse entering a specific week\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekdayLeave**: Indicates mouse leaving a specific weekday\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **WeekdayEnter**: Indicates mouse entering a specific weekday\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **FooterLeave**: Indicates mouse leaving footer\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **FooterEnter**: Indicates mouse entering footer\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **MonthClick**: Indicates mouse click on a specific month\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **MonthDoubleClick**: Indicates mouse double click on a specific month\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **MonthLeave**: Indicates mouse leavinvg a specific month\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **MonthEnter**: Indicates mouse entering a specific month\
![event](https://user-images.githubusercontent.com/92783962/157666814-ad403d1e-a960-423d-99d9-e1911381b9f7.png) **MonthChanged**: Indicates that current month has changed

# Formatting days, months, weekdays and weeknumbers

Its possible to change the appearence of specific entities by creating formatted collections.

```c#
TrulyObservableCollection<Day> days = new TrulyObservableCollection<Day>();
TrulyObservableCollection<Month> months = new TrulyObservableCollection<Month>();
TrulyObservableCollection<Week> weeks = new TrulyObservableCollection<Week>();
TrulyObservableCollection<Weekday> weekdays = new TrulyObservableCollection<Weekday>();

this.days.Add(new Day() { Date = new DateTime(2020, 8, 13), BackgroundColor = Colors.HotPink,
  DateColor = Colors.Yellow, Image = new BitmapImage(new Uri("pack://application:,,,/Resources/star.png", UriKind.Absolute)),
  DateFontSize = 22, Text = "Party", Tooltip = "This is a test.", TextColor = Colors.White, TextFontSize = 36  });

this.weeks.Add(new Week() { TextColor = Colors.White, BackgroundColor = Colors.Green, 
  TextFontWeight = FontWeights.Bold, 
   Number = 32, Year = 2020, Tooltip = "This is the first vacation week" });

this.weekdays.Add(new Weekday() { BackgroundColor = Colors.Orange, Tooltip = "This is a weekday.",
   TextColor = Colors.White, TextFontSize = 16, TextFontWeight = FontWeights.Bold, Year = 2020, 
   Month = 8, DayOfWeek = DayOfWeek.Monday });

```
Then bind to these properties in XAML

```xaml
<calendar:MonthCalendar Grid.Row="0"
                        VisualMode="Days"
                        Background="White"
                        Name="MyCalendar"
                        Year="2020" Month="8"
                        Days="{Binding Path=Days, Mode=OneWay}"
                        Weeks="{Binding Path=Weeks, Mode=OneWay}"
                        DayOfWeek="{Binding Path=Weekdays, Mode=OneWay}">
</calendar:MonthCalendar>
```

# Override default data templates

If you want you can even change the layout and presentation for days, months, weeknumbers and weekdays\
by supplying your own data template. Its possible to change the template for every object of a certain type\
by assigning the template to the monthcalender or you can change the template for a specific object\
e.g. a certain day or weekday. 
 
Create your own data templates and place them in a resource section.

```xaml
<Application x:Class="MonthCalendar.Test.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:model="clr-namespace:Pabo.MonthCalendar.Model;assembly=Pabo.MonthCalendar"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
      
    <DataTemplate x:Key="myDayTemplate" DataType="{x:Type model:CalendarDay}" >
      <Grid Background="Azure">
        <TextBlock Text="{Binding Date.Day}"></TextBlock>
      </Grid>
    </DataTemplate>
    <DataTemplate x:Key="myWeekTemplate" DataType="{x:Type model:CalendarWeek}" >
      <Grid Background="Azure">
        <TextBlock Text="{Binding Date.Weeknumber}"></TextBlock>
      </Grid>
    </DataTemplate>
    <DataTemplate x:Key="myWeekdayTemplate" DataType="{x:Type model:CalendarWeekday}" >
      <Grid Background="LimeGreen">
        <TextBlock Text="{Binding Text}" VerticalAlignment="Center" HorizontalAlignment="Center" FontSize="10" Foreground="White"></TextBlock>
      </Grid>
    </DataTemplate>

  </Application.Resources>
</Application>
```

Then assign these new templates to the monthcalendar in XAML.

```xaml
<calendar:MonthCalendar Grid.Row="0"
                        VisualMode="Days"
                        Background="White"
                        Name="MyCalendar"
                        Year="2020" Month="8"
                        DayTemplate="{StaticResource myDayTemplate}"
                        SelectionMode="None">
      
</calendar:MonthCalendar>
```
Or you can assign the template in code behind.

```c#
this.days.Add(new Day()
      {
        Date = new DateTime(2020, 8, 13),
        Template = (DataTemplate)Application.Current.FindResource("myDayTemplate2")
       });
```

Note that if you want the custom template to work with selections, mouse over and\
disabled state you have to add formatting for that to the template.

# License
(C)opyright 2022 Patrik Bohman\
Published under [MIT](https://choosealicense.com/licenses/mit/) License
