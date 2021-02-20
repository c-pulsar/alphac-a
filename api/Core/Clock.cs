using System;

namespace AlphacA.Core
{
  public interface IClock
  {
    DateTime UtcNow();
  }

  public class Clock : IClock
  {
    public DateTime UtcNow()
    {
      return DateTime.UtcNow;
    }
  }
}