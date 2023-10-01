using UnityEngine;

public class LineDesire : Desire
{
  public float startTime;

  public float time;
  public float satisfactionLevel { get; private set; }

  public LineDesire(float time, float satisfactionLevel)
  {
    this.time = time;
    this.satisfactionLevel = satisfactionLevel;
    startTime = Time.time;
  }


  public float satisfaction(Alien alien)
  {
    if (alien.room != null)
    {
      return 0;
    }

    if ((Time.time - startTime) > time)
    {
      return satisfactionLevel;
    }

    return 0;
  }

  public bool Likes()
  {
    return satisfactionLevel > 0;
  }

  public bool Equals(Desire other)
  {
    return false;
  }
}