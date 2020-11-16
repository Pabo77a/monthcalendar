using Pabo.MonthCalendar.Model;
using System.Windows;
using System.Windows.Controls;

namespace Pabo.MonthCalendar.Selectors
{
  public class DayTemplateSelector : DataTemplateSelector
  {
    public override DataTemplate
        SelectTemplate(object item, DependencyObject container)
    {
      FrameworkElement element = container as FrameworkElement;

      if (element != null && item != null && item is CalendarDay)
      {
        CalendarDay dayItem = item as CalendarDay;
        
        var defaultTemplate = element.FindResource("DayDefaultTemplate") as DataTemplate;
       
        return dayItem.Template != null ? dayItem.Template : defaultTemplate;

      }

      return null;
    }
  }
}