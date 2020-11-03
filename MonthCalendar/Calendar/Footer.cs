﻿using System.Windows;
using System.ComponentModel;

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
