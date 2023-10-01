using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
  [SerializeField] List<Alien> aliens = new List<Alien>();
  [SerializeField] List<RoomObject> roomObjects = new List<RoomObject>();

  public float roomWidth = 6f;
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void AddAlien(Alien alien)
  {
    if (aliens.Contains(alien) == false)
    {
      aliens.Add(alien);
    }
    else
    {
      Debug.LogError("Alien already in the room");
    }
  }

  public void RemoveAlien(Alien alien)
  {
    if (aliens.Contains(alien))
    {
      aliens.Remove(alien);
    }
    else
    {
      Debug.LogError("Try to remove an alien but he is not in the room");
    }
  }

  public bool ContainsRace(AlienRace race)
  {
    return aliens.Any(alien => alien.race == race);
  }
  
  public bool ContainsObject(RoomObjectType type)
  {
    return roomObjects.Any(roomObject => roomObject.type == type);
  }
}
