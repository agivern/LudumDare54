using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawn : MonoBehaviour
{

  public GameObject ship;
  public Transform shipSpawn;
  public Transform shipParking;


  private void OnTriggerEnter2D(Collider2D other)
  {
    
    float time = 2f;
    GameObject newShip = Instantiate(ship);
    newShip.transform.position = shipSpawn.position;
    SpaceShip ss = newShip.GetComponent<SpaceShip>();
    ss.spawn = shipSpawn;
    ss.parking = shipParking;
    ss.time = time;

  }
}
