using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{     
    public float time = 2f;
    private float startTime;
    public Transform oriPos;
    public Transform destination;

    public void Start() {

        startTime = Time.time;

    }

    private void Update()
    {
    
        transform.position = Vector3.Lerp(oriPos.position, destination.position, (Time.time - startTime) / time);
    
        if(transform.position.x >= destination.position.x) {
            SpawnAndDestroy();
        }
    }

    private void SpawnAndDestroy() {
        Destroy(this);
    }
}
