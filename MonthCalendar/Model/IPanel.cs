using System.Windows;

namespace Pabo.MonthCalendar.Model
{
  public interface IPanel
  {
    bool Disabled { get; set; }
    bool MouseOver { get; set; }
    bool Selected { get; set; }

    string Id { get; }

    string Tooltip { get; set; }

    Thickness BorderThickness { get; set; }
  }
}