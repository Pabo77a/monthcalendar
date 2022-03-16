
namespace Pabo.MonthCalendar.Model
{

  public class Week : Common
  {
    private int number;
    private int year;

    public int Number
    {
      get => number;
      set
      {
        if (value != number)
        {
          this.number = value;
          OnPropertyChanged();
        }
      }
    }

    public int Year
    {
      get => year;
      set
      {
        if (value != year)
        {
          this.year = value;
          OnPropertyChanged();
        }
      }
    }
  }
}
