using TMPro;
using UnityEngine;

public class AlienStarIndicator : MonoBehaviour
{
  public float timeActive = 4f;
  public GameObject starHappy;
  public GameObject starNotHappy;
  public void Play(int stars)
  {
    if (stars > 0)
    {
      starHappy.SetActive(true);
    }
    else
    {
      starNotHappy.SetActive(true);
    }
    
    Invoke("Stop", timeActive);
  }

  public void Stop()
  {
    starHappy.SetActive(false);
    starNotHappy.SetActive(false);
  }
}