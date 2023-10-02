using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{     
    public float time = 2f;
    private float startTime;
    public Transform spawn;
    public Transform parking;

    public void Start() {

        startTime = Time.time;

    }

    private void Update()
    {
    
        transform.position = 
        Vector3.Lerp(spawn.position, parking.position, (Time.time - startTime) / time);
    
        if(transform.position.x >= parking.position.x) {
            SpawnAndDestroy();
        }
    }

    private void SpawnAndDestroy() {
        Destroy(this.gameObject);
    }
}
