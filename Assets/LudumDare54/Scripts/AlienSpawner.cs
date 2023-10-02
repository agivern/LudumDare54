using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienSpawner : MonoBehaviour
{
  public static AlienSpawner instance;

  public float baseSpawnRate = 10f;
  public float randomAmount = 0.5f;

  public Transform spawnPoint;

  public GameObject ship;
  public Transform shipSpawn;
  public Transform shipParking;

  private float nextSpawnTime = 0f;

  public AlienRaceByStars[] alienRaceByStars;

  public bool pause = true;

  void Awake()
  {
    instance = this;
  }

  private void Update()
  {
    if (pause == false)
    {
      if (Time.time > nextSpawnTime)
      {
        if (LineManager.instance.lineup.Count < 9)
        {
          SpawnAlien();
        }

        nextSpawnTime = Time.time + NextSpawnSecs();
      }
    }
  }

  private float NextSpawnSecs()
  {
    var reductionAmount = (StarManager.instance.MaxStarsLevel + 30) / 30;
    var spawnRate = baseSpawnRate / reductionAmount;

    var allo = spawnRate + Random.Range(-randomAmount * spawnRate, randomAmount * spawnRate);
    return allo;
  }

  public void SpawnAlien()
  {
    // spawn ship

    float time = 2f;
    GameObject newShip = Instantiate(ship);
    newShip.transform.position = shipSpawn.position;
    SpaceShip ss = newShip.GetComponent<SpaceShip>();
    ss.spawn = shipSpawn;
    ss.parking = shipParking;
    ss.time = time;

    // wait for ship animation (sync with ship time)
    Invoke("isntanciateAlien", time);
 }

  private void isntanciateAlien() {
    
    var alienPrefab = RandomSpawnableAliens();
    var alienObj = Instantiate(alienPrefab, spawnPoint.position, Quaternion.identity);
    LineManager.instance.AddAlien(alienObj.GetComponent<Alien>());
 
  }

  public List<AlienRace> SpawnableRaces()
  {
    var stars = StarManager.instance.MaxStarsLevel;

    return alienRaceByStars.Where(alienByStars => stars >= alienByStars.starsRequired)
      .Select(alienByStars => alienByStars.race).ToList();
  }

  public GameObject RandomSpawnableAliens()
  {
    var alienPrefabs = SpawnableAliens();
    return alienPrefabs[Random.Range(0, alienPrefabs.Count)];
  }

  public List<GameObject> SpawnableAliens()
  {
    var stars = StarManager.instance.MaxStarsLevel;

    return alienRaceByStars.Where(alienByStars => stars >= alienByStars.starsRequired)
      .Select(alienByStars => alienByStars.alienPrefab).ToList();
  }
}

[System.Serializable]
public class AlienRaceByStars
{
  public AlienRace race;
  public int starsRequired;
  public GameObject alienPrefab;
}