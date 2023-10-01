using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomShopObject : MonoBehaviour
{
  [SerializeField] Room room;
  [SerializeField] RoomObjectType objectType;
  [SerializeField] int cost;

  public void OnClick()
  {
    room.AddRoomObject(objectType);
    GetComponent<Button>().enabled = false;
    MoneyManager.instance.PurchaseItem(cost);
  }
}
