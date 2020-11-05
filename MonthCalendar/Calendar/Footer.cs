using System.Windows;
using System.ComponentModel;
using Pabo.MonthCalendar.Properties;

namespace Pabo.MonthCalendar
{
  [ToolboxItem(false)]
  internal class Footer : BaseControl
  {

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




  }
}
