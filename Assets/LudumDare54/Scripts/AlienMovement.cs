using System;
using UnityEngine;


public class AlienMovement : MonoBehaviour
{
    public bool hasDestination = false;
    public bool isInSpace = false;
    private Vector2 currentDestination;

    public float speed = 10f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        if (isInSpace)
        {
            return;
        }
        
        if (!hasDestination)
        {
            rb.velocity = Vector2.zero;
        }

        var xMovement = (currentDestination.x - transform.position.x) * 6;
        xMovement = Mathf.Min(1, xMovement);
        xMovement = Mathf.Max(-1, xMovement);
        xMovement *= speed;
        rb.velocity = new Vector2(xMovement, rb.velocity.y);

        // var xDistance = Mathf.Abs(currentDestination.x - transform.position.x);
        // if (xDistance < 0.01f)
        // {
        //     hasDestination = false;
        //     rb.velocity = new Vector2(0, rb.velocity.y);
        // }
    }

    public void SetDestination(Vector2 position)
    {
        currentDestination = position;
        hasDestination = true;
    }
}