using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using Pabo.MonthCalendar.Common;
using System.Diagnostics;

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
    
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
               typeof(string),
               typeof(Footer),
               new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    
    #region overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
    }

    #endregion

      

    #region properties

    [Description("")]
    [Category("Footer")]
    [Browsable(true)]
    internal string Text
    {
      get
      {
        return (string)this.GetValue(TextProperty);
      }
      set
      {
        this.SetValue(TextProperty, value);
      }
    }


    #endregion


  

      }
}
