using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class JokeManager : MonoBehaviour
{
  [SerializeField] List<string> jokeText = new List<string>();
  [SerializeField] TextMeshProUGUI dialogBoxText;
  [SerializeField] GameObject dialogBox;

  private int currentStep = 0;
  public float timeBeforeNextJoke;

  void Start()
  {
    SetTimeBeforeNextJoke();
  }

  void Update()
  {
    if (AlienSpawner.instance.pause == false)
    {
      timeBeforeNextJoke = Mathf.Max(0, timeBeforeNextJoke - Time.deltaTime);

      if (timeBeforeNextJoke == 0)
      {
        SetJoke();
        SetTimeBeforeNextJoke();
      }
    }
  }

  private void SetTimeBeforeNextJoke()
  {
    timeBeforeNextJoke = Random.Range(30, 45);
  }

  private void SetJoke()
  {
    dialogBox.SetActive(true);

    var joke = Random.Range(0, jokeText.Count);
    dialogBoxText.text = jokeText[joke];
    Invoke("DisableGameObject", 5);
  }

  void DisableGameObject()
  {
    dialogBox.SetActive(false);
  }
}