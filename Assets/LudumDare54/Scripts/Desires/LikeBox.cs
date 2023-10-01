using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class LikeBox : MonoBehaviour
{
    public GameObject alienSpritePrefab;

    public float spaceBetween;

    public Transform likeBoxTransform;
    public Transform dislikeBoxTransform;

    public AlienRaceToSprite[] alienRaceToSprites;

    void Start()
    {
    }

    public void Initialize(List<Desire> desires)
    {
        var likedDesires = desires.Where(desire => desire.Likes()).ToList();
        var dislikedDesires = desires.Where(desire => !desire.Likes()).ToList();

        for (var i = 0; i < likedDesires.Count(); i++)
        {
            InstantiateDesire(likedDesires[i], likeBoxTransform, i);
        }

        for (var i = 0; i < dislikedDesires.Count(); i++)
        {
            InstantiateDesire(dislikedDesires[i], dislikeBoxTransform, i);
        }
    }

    private void InstantiateDesire(Desire desire, Transform parent, int count)
    {
        if (desire is LineDesire)
        {
            return;
        }

        var alienSprite = Instantiate(alienSpritePrefab, parent.position, Quaternion.identity, parent);
        var alienSpriteRenderer = alienSprite.GetComponent<SpriteRenderer>();
        alienSpriteRenderer.sprite = GetSpriteForDesire(desire);

        alienSprite.transform.position = (Vector2)parent.position + (count * spaceBetween * Vector2.right);
    }

    private Sprite GetSpriteForDesire(Desire desire)
    {
        if (desire is RaceDesire raceDesire)
        {
            return alienRaceToSprites.First(map => map.race == raceDesire.race).sprite;
        }

        throw new NotImplementedException();
    }
}


[System.Serializable]
public class AlienRaceToSprite
{
    public AlienRace race;
    public Sprite sprite;
}