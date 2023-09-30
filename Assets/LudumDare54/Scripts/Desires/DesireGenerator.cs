using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DesireGenerator
{
    public static List<Desire> GenerateDesire()
    {
        return Enumerable.Range(0, numberOfDesires()).Select(i => createDesire()).ToList();
    }

    private static int numberOfDesires()
    {
        return Mathf.FloorToInt(1 + (StarManager.instance.Stars / 5f));
    }
    
    private static Desire createDesire()
    {
        // TODO add object desires
       return createRaceDesire();
    }

    private static RaceDesire createRaceDesire()
    {
        var possibleRaces = AlienSpawner.instance.SpawnableRaces();
        var race = possibleRaces[Random.Range(1, (possibleRaces.Count - 1))];
        var satisfactionLevel = Random.value > 0.5f ? 1 : -1;
        return new RaceDesire(race, satisfactionLevel);
    }
}