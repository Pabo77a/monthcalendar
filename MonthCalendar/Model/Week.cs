
using System;
using System.Globalization;

namespace Pabo.MonthCalendar.Model
{
  internal class Week
  {

    public Week()
    {
    }

    public Week(DateTime date) : this()
    {
      FirstDateOWeek = date;
    }


    public DateTime FirstDateOWeek { get; set; }

    public string Weeknumber
    {
      get
      {
        return ISOWeek.GetWeekOfYear(FirstDateOWeek).ToString();
      }
    }

  }
}
