using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotelStarsList : MonoBehaviour
{
  [SerializeField] GameObject prefabStar;
  List<GameObject> starsGameobject = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    var starsDifference = StarManager.instance.Stars - starsGameobject.Count;

    if (starsDifference < 0)
    {
      for (var i = 0; i > starsDifference; i--)
      {
        DestroyStars();
      }
    }
    else if (starsDifference > 0)
    {
      for (var i = 0; i < starsDifference; i++)
      {
        SpawnStars();
      }
    }
  }

  private void SpawnStars()
  {
    GameObject childObject = Instantiate(prefabStar);
    childObject.transform.SetParent(transform);
    childObject.transform.localPosition = new Vector3(0 + (.4f * starsGameobject.Count), 0, 0);
    starsGameobject.Add(childObject);
  }

  private void DestroyStars()
  {
    var starToDestroy = starsGameobject[starsGameobject.Count - 1];
    starsGameobject.RemoveAt(starsGameobject.Count - 1);
    GameObject.Destroy(starToDestroy);
  }
}
