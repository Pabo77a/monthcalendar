using System.Windows;
using System.Windows.Media;

namespace Pabo.MonthCalendar.Properties
{
  public class WeeknumberProperties : PanelProperties
  {
    public WeeknumberProperties() : base()
    {
      this.TextColor = Colors.Blue;
      this.BackgroundColor = Colors.White;
      this.TextHorizontalAlignment = HorizontalAlignment.Center;
      this.TextVerticalAlignment = VerticalAlignment.Center;
      this.TextMargin = new Thickness(10, 0, 10, 0);

      this.MouseOverBackgroundColor = Colors.Transparent;
      this.MouseOverBorderColor = Colors.Transparent;
      this.MouseOverOpacity = .25;

    }
  }
}
