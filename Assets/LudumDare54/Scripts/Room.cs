using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
  [SerializeField] List<Alien> aliens = new List<Alien>();

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
}
