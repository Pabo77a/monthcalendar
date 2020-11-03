﻿using System;
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

  [TemplatePart(Name = "PART_Left", Type = typeof(Button))]
  [TemplatePart(Name = "PART_Right", Type = typeof(Button))]
  [ToolboxItem(false)]
  internal class Header : BaseControl
  {

    #region private members

    private Button left;
    private Button right;

    private int month;
    private int year;

    #endregion

    #region routed events

    public static readonly RoutedEvent MonthDecreaseEvent = EventManager.RegisterRoutedEvent(
       "Decrease", RoutingStrategy.Direct, typeof(RoutedEventArgs), typeof(Header));

    public static readonly RoutedEvent MonthIncreaseEvent = EventManager.RegisterRoutedEvent(
       "Increase", RoutingStrategy.Direct, typeof(RoutedEventArgs), typeof(Header));

    #endregion

    #region dependency properties

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
               typeof(string),
               typeof(Header),
               new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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

    internal void SetDate(int year, int month)
    {
      this.year = year;
      this.month = month;

      var date = new DateTime(this.year, this.month, 1);
      this.Text = date.ToString("MMMM yyyy");
    }

    #endregion
  }
}
