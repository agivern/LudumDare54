using UnityEngine;
using UnityEditor;
using System.Linq;

public class ToggleGameObject : MonoBehaviour
{
#if UNITY_EDITOR
  [MenuItem("Edit/Toggle Active State %e")]
  private static void ToggleActiveState()
  {
    foreach (var selected in Selection.gameObjects)
    {
      selected.SetActive(!selected.activeSelf);
    }
  }

  [MenuItem("Edit/Toggle Active State %e", true)]
  private static bool ValidateToggleActiveState()
  {
    return Selection.gameObjects.Any();
  }
#endif
}