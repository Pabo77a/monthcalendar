using System;
using System.Windows;
using System.ComponentModel;
using Pabo.MonthCalendar.Properties;

namespace Pabo.MonthCalendar
{
  [ToolboxItem(false)]
  internal class Footer : BaseControl
  {

    #region events

    internal event EventHandler FooterLeave;

    internal event EventHandler FooterEnter;

    #endregion

    public Footer()
    {
      this.MouseLeave += Footer_MouseLeave;
      this.MouseEnter += Footer_MouseEnter;
    }

    #region constructor

    static Footer()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Footer), new FrameworkPropertyMetadata(typeof(Footer)));

    }

    #endregion

    #region dependency properties
        
    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
               typeof(FooterProperties),
               typeof(Footer),
               new FrameworkPropertyMetadata(new FooterProperties(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion


    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
    }

    #endregion

      

    #region properties

    
    [Description("")]
    [Category("Header")]
    [Browsable(true)]
    internal FooterProperties Properties
    {
      get
      {
        return (FooterProperties)this.GetValue(PropertiesProperty);
      }
      set
      {
        this.SetValue(PropertiesProperty, value);
      }
    }


    #endregion


    #region Eventhandlers

    private void Footer_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
      this.OnMouseEnter();
    }

    private void Footer_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
      this.OnMouseLeave();
    }


    #endregion

    #region private methods
    private void OnMouseEnter()
    {
      EventHandler handler = FooterEnter;
      handler?.Invoke(this, new System.EventArgs());
    }

    private void OnMouseLeave()
    {
      EventHandler handler = FooterLeave;
      handler?.Invoke(this, new System.EventArgs());
    }


    #endregion


  }
}
