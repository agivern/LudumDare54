using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
  private Room room;
  private int roomStayDuration;
  private int happiness = 100;

  private float timer = 1f;

  void Start()
  {
    SetRandomRoomStayDuration();
    // Set random list of "Wants" "Doesn't want"
  }

  void LateUpdate()
  {
    if (room != null)
    {
      // Leave hotel at the top of the loop to be trigger a frame after roomStayDuration update (prevent bug in happiness calcul)
      if (roomStayDuration == 0)
      {
        LeaveHotel();
      }

      timer -= Time.deltaTime;
      if (timer <= 0)
      {
        timer = 1f;

        UpdateHappiness();

        UpdateRemainingTime();
      }
    }
  }

  public void MoveToRoom(Room newRoom)
  {
    room = newRoom;
    room.AddAlien(this);

    // TODO move gameobject in room
  }

  private void UpdateHappiness()
  {
    // TODO 
  }

  private void UpdateRemainingTime()
  {
    roomStayDuration = Mathf.Max(0, roomStayDuration - 1);
  }

  private void SetRandomRoomStayDuration()
  {
    int baseDuration = Random.Range(15, 31);
    int multiplier = StarManager.instance.Stars;

    // Convert the multiplier to a factor between 100 (1x) and 300 (3x)
    int factor = 100 + 2 * multiplier;

    roomStayDuration = (baseDuration * factor) / 100;
  }

  private void LeaveHotel()
  {
    if (happiness > 0)
    {
      MoneyManager.instance.CustomerPay(10);

      if (happiness > 80)
      {
        StarManager.instance.CustomerTip();
        // TODO trigger sound effect
      }
    }

    room.RemoveAlien(this);

    room = null;

    // TODO Trigger Animation
    // TODO Destroy Gameobject at the end of animation
    GameObject.Destroy(this.gameObject);
  }
}