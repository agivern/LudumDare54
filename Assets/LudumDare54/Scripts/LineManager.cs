using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LineManager : MonoBehaviour
{
  public List<Alien> lineup;

  public float spacing = 1f;

  public Transform endOfLineUp;

  public static LineManager instance;

  private void Awake()
  {
    instance = this;
  }

  private void Update()
  {
    if (lineup.Any())
    {
      lineup[0].ExpressDesires();
    }
  }

  public void AddAlien(Alien alien)
  {
    if (lineup.Contains(alien))
    {
      return;
    }

    lineup.Add(alien);
    var pos = LineupPositionToWorldPosition(lineup.Count - 1);
    alien.MoveTo(pos);
  }

  public void RemoveAlien(Alien alien)
  {
    var removed = lineup.Remove(alien);

    if (removed)
    {
      UpdateAllAlienPositions();
    }

    alien.CloseExpressDesires();
  }

  private void UpdateAllAlienPositions()
  {
    for (var i = 0; i < lineup.Count; i++)
    {
      var alien = lineup[i];
      var pos = LineupPositionToWorldPosition(i);
      alien.MoveTo(pos);
    }
  }

  public Alien GetNextCustomer()
  {
    return lineup[0];
  }

  private Vector2 LineupPositionToWorldPosition(int position)
  {
    return (Vector2)endOfLineUp.position + (Vector2.left * (position * spacing));
  }
}