using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public static AlienSpawner instance;

    public float baseSpawnRate = 10f;
    
    public GameObject alienPrefab;
    
    void Awake()
    {
        instance = this;
    }
}
