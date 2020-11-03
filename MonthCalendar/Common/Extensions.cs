using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Pabo.MonthCalendar.Common
{
  public static class Extensions
  {

    public static T GetCopy<T>(this T S)
    {
      T newObj = Activator.CreateInstance<T>();

      foreach (PropertyInfo i in newObj.GetType().GetProperties())
      {

        //"EntitySet" is specific to link and this conditional logic is optional/can be ignored
        if (i.CanWrite && i.PropertyType.Name.Contains("EntitySet") == false)
        {
          object value = S.GetType().GetProperty(i.Name).GetValue(S, null);
          i.SetValue(newObj, value, null);
        }
      }

      return newObj;
    }
  }
}
