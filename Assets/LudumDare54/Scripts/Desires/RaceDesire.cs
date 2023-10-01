public class RaceDesire : Desire
{
  public AlienRace race;
  public float satisfactionLevel { get; private set; }

  public RaceDesire(AlienRace race, float satisfactionLevel)
  {
    this.race = race;
    this.satisfactionLevel = satisfactionLevel;
  }


  public float satisfaction(Alien alien)
  {
    if (alien.room == null)
    {
      return 0;
    }

    if (alien.room.ContainsRace(race, alien))
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
    if (other is RaceDesire desire)
    {
      return desire.race == race;
    }

    return false;
  }
}