using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
  public static MoneyManager instance;

  [SerializeField] int money;
  [SerializeField] int defaultHotelCost;
  [SerializeField] TextMeshProUGUI moneyUI;

  private float timer = 1f;

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    UpdateUI();
  }

  private void LateUpdate()
  {
    timer -= Time.deltaTime;

    if (timer <= 0)
    {
      timer = 1f;

      DeductCost();
    }
  }

  public void CustomerPay(int value)
  {
    money += Mathf.Max(0, value);
    UpdateUI();
  }

  public void PurchaseItem(int value)
  {
    if (value > 0)
    {
      money = Mathf.Max(0, money - value);
      UpdateUI();
    }
  }

  private void DeductCost()
  {
    money = Mathf.Max(0, money - GetHotelCost());
    UpdateUI();

    if (money == 0)
    {
      // TODO Trigger GAME OVER
    }
  }

  private void UpdateUI()
  {
    moneyUI.text = money.ToString();
  }

  public int GetHotelCost()
  {
    return defaultHotelCost + ((StarManager.instance.MaxStarsLevel + 20) / 20) - 1;
  }
}