using System;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Model
{
  public class DayItem
  {

    public DayItem()
    {
      BackgroundColor = Colors.Transparent;
      DateColor = Colors.Black;
      TextColor = Colors.Black;
      Text = "";
    }

    public DayItem(DateTime date) : this()
    {
      Date = date;
    }

    public DateTime Date { get; set; }

    public Color BackgroundColor { get; set; }

    public Color TextColor { get; set; }

    public Color DateColor { get; set; }
    
    public string Text { get; set; }

  }
}
