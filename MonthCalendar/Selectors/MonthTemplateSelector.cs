using Pabo.MonthCalendar.Model;
using System.Windows;
using System.Windows.Controls;

namespace Pabo.MonthCalendar.Selectors
{
  public class MonthTemplateSelector : DataTemplateSelector
  {
    public override DataTemplate
        SelectTemplate(object item, DependencyObject container)
    {
      FrameworkElement element = container as FrameworkElement;

      if (element != null && item != null && item is CalendarMonth)
      {
        CalendarMonth monthItem = item as CalendarMonth;

        var defaultTemplate = element.FindResource("MonthDefaultTemplate") as DataTemplate;

        return monthItem.Template != null ? monthItem.Template : defaultTemplate;

      }

      return null;
    }
  }
}