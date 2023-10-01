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
    var shopButton = GetComponent<Button>();

    ColorBlock colors = shopButton.colors;
    colors.disabledColor = Color.green;
    shopButton.colors = colors;

    shopButton.interactable = false;

    MoneyManager.instance.PurchaseItem(cost);
  }
}
