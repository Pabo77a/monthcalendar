using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public interface ITooltipProperties
  {
      Color TooltipBackgroundColor { get; set; }
    Color TooltipBorderColor { get; set; }
    FontFamily TooltipFontFamily { get; set; }
    int TooltipFontSize { get; set; }
    FontStyle TooltipFontStyle { get; set; }
    FontWeight TooltipFontWeight { get; set; }
    Color TooltipTextColor { get; set; }
  }
}