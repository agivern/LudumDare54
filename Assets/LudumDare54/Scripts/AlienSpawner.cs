using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public static AlienSpawner instance;

    public float baseSpawnRate = 10f;
    public float randomAmount = 0.5f;

    public GameObject alienPrefab;

    public Transform spawnPoint;

    private float nextSpawnTime = 0f;

    public AlienRaceByStars[] alienRaceByStars;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnAlien();
            nextSpawnTime = Time.time + NextSpawnSecs();
        }
    }

    private float NextSpawnSecs()
    {
        var reductionAmount = (StarManager.instance.Stars + 10) / 10;
        var spawnRate = baseSpawnRate / reductionAmount;

        var allo = spawnRate + UnityEngine.Random.Range(-randomAmount * spawnRate, randomAmount * spawnRate);
        return allo;
    }

    public void SpawnAlien()
    {
        var alienObj = Instantiate(alienPrefab, spawnPoint.position, Quaternion.identity);
        LineManager.instance.AddAlien(alienObj.GetComponent<Alien>());
    }

    public List<AlienRace> SpawnableRaces()
    {
        var stars = StarManager.instance.Stars;

        var spawnableRaces = new List<AlienRace>();
        spawnableRaces.Add(AlienRace.Green);

        return alienRaceByStars.Where(alienByStars => stars >= alienByStars.starsRequired)
            .Select(alienByStars => alienByStars.race).ToList();
    }
}

[System.Serializable]
public class AlienRaceByStars
{
    public AlienRace race;
    public int starsRequired;
}