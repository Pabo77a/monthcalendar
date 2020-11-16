using Pabo.MonthCalendar.Model;
using System.Windows;
using System.Windows.Controls;

namespace Pabo.MonthCalendar.Selectors
{
  public class WeekTemplateSelector : DataTemplateSelector
  {
    public override DataTemplate
        SelectTemplate(object item, DependencyObject container)
    {
      FrameworkElement element = container as FrameworkElement;

      if (element != null && item != null && item is CalendarWeek)
      {
        CalendarWeek weekItem = item as CalendarWeek;

        var defaultTemplate = element.FindResource("WeekDefaultTemplate") as DataTemplate;

        return weekItem.Template != null ? weekItem.Template : defaultTemplate;

      }

      return null;
    }
  }
}