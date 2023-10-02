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

      var randomHate = Random.Range(0, 100);

      var race2 = race;
      var probaHate = 60;
      if (AlienSpawner.instance.SpawnableRaces().Count == 3)
      {
        probaHate = 65;
      }
      else if (AlienSpawner.instance.SpawnableRaces().Count == 4)
      {
        probaHate = 70;
      }

      if (randomHate < probaHate)
      {
        do
        {
          race2 = possibleRaces[Random.Range(0, possibleRaces.Count)];

        } while (race2 == race);
        desires.Add(new RaceDesire(race2, -1));
      }

      if (AlienSpawner.instance.SpawnableRaces().Count == 4)
      {
        var randomMoreLike = Random.Range(0, 100);
        if (randomMoreLike > 75)
        {
          var race3 = race;
          do
          {
            race3 = possibleRaces[Random.Range(0, possibleRaces.Count)];

          } while (race3 == race || race3 == race2);
          desires.Add(new RaceDesire(race3, 1));
        }
      }
    }

    if (AlienSpawner.instance.SpawnableRaces().Count == 1)
    {
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
    }
    desires.Add(new LineDesire(10f, -1f));

    return desires;
  }

  private int numberOfDesires()
  {
    return 1;
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
      var satisfactionLevel = Random.value > 0.4f ? 1 : -1;
      return new RaceDesire(race, satisfactionLevel);
    }
    else
    {
      var satisfactionLevel = Random.value > 0.25f ? 1 : -1;
      return new RaceDesire(race, satisfactionLevel);
    }
  }
}