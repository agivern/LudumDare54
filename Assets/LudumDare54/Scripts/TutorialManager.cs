using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
  [SerializeField] List<string> tutorialText = new List<string>();
  [SerializeField] TextMeshProUGUI dialogBoxText;
  [SerializeField] GameObject dialogBox;

  private int currentStep = 0;

  void Start()
  {
    dialogBoxText.text = tutorialText[0];
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
    {
      currentStep++;

      if (currentStep >= tutorialText.Count)
      {
        AlienSpawner.instance.pause = false;
        dialogBox.SetActive(false);
      }
      else
      {
        dialogBoxText.text = tutorialText[currentStep];
      }
    }
  }
}