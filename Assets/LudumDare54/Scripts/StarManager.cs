using UnityEngine;
using TMPro;

public class StarManager : MonoBehaviour
{
  public static StarManager instance;

  [SerializeField] int stars = 5;
  [SerializeField] int maxStarsLevel = 5;
  [SerializeField] TextMeshProUGUI starsUI;
  
  public GameObject victoryScreen;
  public GameObject defeatScreen;

  public bool isFinished = false;

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
    if (isFinished)
    {
      return;
    }
    stars++;

    if (stars >= 30)
    {
      isFinished = true;
      victoryScreen.SetActive(true);
    }

    if (stars > maxStarsLevel)
    {
      maxStarsLevel = stars;
    }

    AudioManager.instance.PlayEarnStarAudio();

    UpdateUI();
  }

  public void CustomerHate(int value)
  {
    if (isFinished)
    {
      return;
    }
    
    stars = Mathf.Max(0, stars - value);
    AudioManager.instance.PlayLostStarAudio();
    UpdateUI();

    if (stars <= 0)
    {
      isFinished = true;
      defeatScreen.SetActive(true);
    }
  }

  private void UpdateUI()
  {
    starsUI.text = stars.ToString();
  }

  public int Stars
  {
    get { return stars; }
  }

  public int MaxStarsLevel
  {
    get { return maxStarsLevel; }
  }
}