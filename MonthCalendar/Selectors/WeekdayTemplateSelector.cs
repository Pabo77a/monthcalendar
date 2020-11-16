using Pabo.MonthCalendar.Model;
using System.Windows;
using System.Windows.Controls;

namespace Pabo.MonthCalendar.Selectors
{
  public class WeekdayTemplateSelector : DataTemplateSelector
  {
    public override DataTemplate
        SelectTemplate(object item, DependencyObject container)
    {
      FrameworkElement element = container as FrameworkElement;

      if (element != null && item != null && item is CalendarWeekday)
      {
        CalendarWeekday weekdayItem = item as CalendarWeekday;

        var defaultTemplate = element.FindResource("WeekdayDefaultTemplate") as DataTemplate;

        return weekdayItem.Template != null ? weekdayItem.Template : defaultTemplate;

      }

      return null;
    }
  }
}