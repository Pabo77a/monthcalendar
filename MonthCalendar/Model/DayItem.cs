using System;
using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Model
{
  public class DayItem
  {

    public DayItem()
    {
      DateFontStyle = FontStyles.Normal;
      DateFontWeight = FontWeights.Normal;
      DateTextDecoration = "";
      DateFontSize = 16;
      DateColor = Colors.Black;

      TextFontStyle = FontStyles.Normal;
      TextFontWeight = FontWeights.Normal;
      TextFontSize = 16;
      TextColor = Colors.Black;
    }

    public DayItem(DateTime date) : this()
    {
      Date = date;
    }

    public DateTime Date { get; set; }

    public Color DateColor { get; set; }
    public int DateFontSize { get; set; }
    public FontWeight DateFontWeight { get; set; }
    public FontStyle DateFontStyle { get; set; }
    public FontFamily DateFontFamily { get; set; }
    public string DateTextDecoration { get; set; }

    public Color BackgroundColor { get; set; }


    public Color TextColor { get; set; }
    public int TextFontSize { get; set; }
    public FontWeight TextFontWeight { get; set; }
    public FontStyle TextFontStyle { get; set; }
    public FontFamily TextFontFamily { get; set; }
    public string TextTextDecoration { get; set; }


    public string Text { get; set; }

  }
}
