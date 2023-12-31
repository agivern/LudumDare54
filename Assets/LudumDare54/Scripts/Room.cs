using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
  [SerializeField] List<Alien> aliens = new List<Alien>();
  [SerializeField] int maxObject;
  [SerializeField] List<GameObject> backgrounds;
  [SerializeField] List<GameObject> objects;

  private int roomStars = 0;
  public float roomWidth = 6f;
  private List<RoomObjectType> roomObjects = new List<RoomObjectType>();

  void Start()
  {
    backgrounds[0].SetActive(true);
  }

  void Update()
  {
    while (roomStars < StarManager.instance.MaxStarsLevel)
    {
      UpgradeRoomStars();
    }
  }

  private void UpgradeRoomStars()
  {
    roomStars++;
    if (roomStars == 8)
    {
      objects[0].SetActive(true);
    }
    else if (roomStars == 10)
    {
      backgrounds[0].SetActive(false);
      backgrounds[1].SetActive(true);
    }
    else if (roomStars == 13)
    {
      objects[1].SetActive(true);
    }
    else if (roomStars == 16)
    {
      objects[2].SetActive(true);
    }
    else if (roomStars == 18)
    {
      objects[3].SetActive(true);
    }
    else if (roomStars == 20)
    {
      objects[4].SetActive(true);
    }
    else if (roomStars == 22)
    {
      backgrounds[1].SetActive(false);
      backgrounds[2].SetActive(true);
    }
    else if (roomStars == 25)
    {
      objects[5].SetActive(true);
    }
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

  public bool ContainsRace(AlienRace race, Alien exceptAlien = null)
  {
    return aliens.Where(alien => alien != exceptAlien).Any(alien => alien.race == race);
  }

  public bool ContainsObject(RoomObjectType type)
  {
    return roomObjects.Any(roomObject => roomObject == type);
  }
}