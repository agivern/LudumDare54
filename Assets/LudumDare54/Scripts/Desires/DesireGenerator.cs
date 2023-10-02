using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DesireGenerator
{
  public List<Desire> GenerateDesire()
  {
    var desires = new List<Desire>();
    var numberOfDesires = this.numberOfDesires();

    if (AlienSpawner.instance.SpawnableRaces().Count > 1)
    {
      var possibleRaces = AlienSpawner.instance.SpawnableRaces();
      var race = possibleRaces[Random.Range(0, possibleRaces.Count)];
      var des = new RaceDesire(race, 1);
      desires.Add(des);
    }

    for (var i = 0; i < numberOfDesires; i++)
    {
      var desire = createDesire();
      // Fix me ugly
      if (desires.Any(d => d.Equals(desire)))
      {
        continue;
      }

      desires.Add(desire);
    }

    desires.Add(new LineDesire(10f, -1f));

    return desires;
  }

  private int numberOfDesires()
  {
    if (StarManager.instance.MaxStarsLevel > 25f)
    {
      return Random.value > 0.35f ? 3 : 2;
    }

    if (StarManager.instance.MaxStarsLevel > 15f)
    {
      return 2;
    }

    return Random.value > 0.5f ? 2 : 1;
  }

  private Desire createDesire()
  {
    // TODO add object desires
    return createRaceDesire();
  }

  private RaceDesire createRaceDesire()
  {
    var possibleRaces = AlienSpawner.instance.SpawnableRaces();
    var race = possibleRaces[Random.Range(0, possibleRaces.Count)];

    if (AlienSpawner.instance.SpawnableRaces().Count > 1)
    {
      var satisfactionLevel = Random.value > 0.6f ? 1 : -1;
      return new RaceDesire(race, satisfactionLevel);
    }
    else
    {
      var satisfactionLevel = Random.value > 0.25f ? 1 : -1;
      return new RaceDesire(race, satisfactionLevel);
    }
  }
}