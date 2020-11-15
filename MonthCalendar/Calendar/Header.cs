using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Pabo.MonthCalendar.Properties;

namespace Pabo.MonthCalendar
{

  [TemplatePart(Name = "PART_Left", Type = typeof(Button))]
  [TemplatePart(Name = "PART_Right", Type = typeof(Button))]
  [ToolboxItem(false)]
  internal class Header : BaseControl
  {

    #region private members

    private Button left;
    private Button right;

    private DateTime minDate;
    private DateTime maxDate;
 
   
    #endregion

    #region routed events

    public static readonly RoutedEvent MonthDecreaseEvent = EventManager.RegisterRoutedEvent(
       "Decrease", RoutingStrategy.Direct, typeof(RoutedEventArgs), typeof(Header));

    public static readonly RoutedEvent MonthIncreaseEvent = EventManager.RegisterRoutedEvent(
       "Increase", RoutingStrategy.Direct, typeof(RoutedEventArgs), typeof(Header));

    #endregion

    #region dependency properties
   
    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(HeaderProperties),
               typeof(Header),
               new FrameworkPropertyMetadata(new HeaderProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    #endregion

    #region constructor

    static Header()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Header), new FrameworkPropertyMetadata(typeof(Header)));

    }

    #endregion

    #region events

    [Category("Header")]
    [Browsable(true)]
    public event RoutedEventHandler Decrease
    {
      add { AddHandler(MonthDecreaseEvent, value); }
      remove { RemoveHandler(MonthIncreaseEvent, value); }
    }

    [Category("Header")]
    [Browsable(true)]
    public event RoutedEventHandler Increase
    {
      add { AddHandler(MonthIncreaseEvent, value); }
      remove { RemoveHandler(MonthIncreaseEvent, value); }
    }

    #endregion

    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.left = GetTemplateChild("PART_Left") as Button;
      if (this.left != null)
      {
        this.left.Click += Left_Click;
      }
      this.right = GetTemplateChild("PART_Right") as Button;
      if (this.right != null)
      {
        this.right.Click += Right_Click;
      }
    }



    #endregion


    #region properties
    

    [Description("")]
    [Category("Header")]
    [Browsable(true)]
    internal HeaderProperties Properties
    {
      get
      {
        return (HeaderProperties)this.GetValue(PropertiesProperty);
      }
      set
      {
        this.SetValue(PropertiesProperty, value);
      }
    }

    public bool CanDecreaseMonth
    {
      get
      {
        return this.minDate.ToString("MMMM yyyy") != new DateTime(this.Properties.Year, this.Properties.Month, 1).ToString("MMMM yyyy");
      }
    }

    public bool CanIncreaseMonth
    {
      get
      {
        return this.maxDate.ToString("MMMM yyyy") != new DateTime(this.Properties.Year, this.Properties.Month, 1).ToString("MMMM yyyy");
      }
    }


    #endregion


    #region eventhandlers

    private void Right_Click(object sender, RoutedEventArgs e)
    {

      RoutedEventArgs newEventArgs = new RoutedEventArgs(Header.MonthIncreaseEvent);
      newEventArgs.RoutedEvent = Header.MonthIncreaseEvent;
      RaiseEvent(newEventArgs);
    }

    private void Left_Click(object sender, RoutedEventArgs e)
    {
      RoutedEventArgs newEventArgs = new RoutedEventArgs(Header.MonthDecreaseEvent);
      newEventArgs.RoutedEvent = Header.MonthDecreaseEvent;
      RaiseEvent(newEventArgs);
    }


    #endregion

    #region methods

    public void Setup(DateTime minDate, DateTime maxDate, int year, int month)
    {
      this.minDate = minDate;
      this.maxDate = maxDate;
    
      this.Properties.SetDate(year, month);

      OnPropertyChanged(nameof(this.CanDecreaseMonth));
      OnPropertyChanged(nameof(this.CanIncreaseMonth));
    }

    #endregion
  }
}
