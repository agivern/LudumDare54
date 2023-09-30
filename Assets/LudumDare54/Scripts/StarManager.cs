
using UnityEngine;
using TMPro;

public class StarManager : MonoBehaviour
{
  public static StarManager instance;

  [SerializeField] int stars;
  [SerializeField] TextMeshProUGUI starsUI;

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    UpdateUI();
  }

  public void CustomerTip()
  {
    stars++;

    if (stars >= 100)
    {
      // TODO Victory screen
    }

    UpdateUI();
  }

  private void UpdateUI()
  {
    starsUI.text = stars.ToString();
  }

  public int Stars
  {
    get { return stars; }
  }
}
