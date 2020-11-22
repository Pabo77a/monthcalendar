using System;
using System.Collections.Generic;
using System.Text;

namespace Pabo.MonthCalendar.Model
{
  public class Pos
  {
    public Pos()
    {

    }

    public Pos(int col, int row) : this()
    {
      this.Col = col;
      this.Row = row;
    }

    public int Col { get; set; }

    public int Row { get; set; }

    public override string ToString()
    {
      return $"({Col},{Row})";
    }
  }
}
