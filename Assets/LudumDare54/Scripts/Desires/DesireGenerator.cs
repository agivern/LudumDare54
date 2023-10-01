using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DesireGenerator
{
    public List<Desire> GenerateDesire()
    {
        var desires = new List<Desire>();
        var numberOfDesires = this.numberOfDesires();
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
        return Mathf.FloorToInt(1 + (StarManager.instance.Stars / 5f));
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
        var satisfactionLevel = Random.value > 0.5f ? 1 : -1;
        return new RaceDesire(race, satisfactionLevel);
    }
}