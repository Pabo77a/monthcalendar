using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pabo.MonthCalendar.Common
{
  public class NotificationHandler : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}