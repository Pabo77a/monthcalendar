using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;

namespace Pabo.MonthCalendar
{
  public class BaseControl : Control, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;
    protected event EventHandler Click;

    protected bool dblClick = false;

    private DispatcherTimer clickTimer;
 

    public BaseControl()
    {
      this.clickTimer = new DispatcherTimer();
      this.clickTimer.Interval = TimeSpan.FromMilliseconds(500);
      this.clickTimer.Tick += ClickTimer_Tick;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    protected void Button_DoubleClick(object sender, MouseButtonEventArgs e)
    {
      clickTimer.Stop();
      e.Handled = true;
    }

    protected void Button_Click(object sender, RoutedEventArgs e)
    {
      clickTimer.Start();
    }


    private void ClickTimer_Tick(object sender, System.EventArgs e)
    {
      clickTimer.Stop();
      OnClick();
    }

    private void OnClick()
    {
      EventHandler handler = this.Click; ;
      handler?.Invoke(this, new System.EventArgs());
    }

   

  }
}
