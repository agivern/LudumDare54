using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
  public static AlienSpawner instance;

  public float baseSpawnRate = 10f;

  public float randomAmount = 0.5f;

  public GameObject alienPrefab;

  public Transform spawnPoint;


  private float nextSpawnTime = 0f;

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
    Instantiate(alienPrefab, spawnPoint.position, Quaternion.identity);
  }
}