using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Alien : MonoBehaviour
{
    public Room room { get; private set; }
    private int roomStayDuration;
    private float happiness = 100;

    private float timer = 1f;

    public AlienRace race = AlienRace.Green;

    private List<Desire> desires = new List<Desire>();

    public LikeBox likeBox;

    void Start()
    {
        SetRandomRoomStayDuration();
        desires = new DesireGenerator().GenerateDesire();
        likeBox.Initialize(desires);
    }

    private void Update()
    {
        MoveInRoom();

        transform.position = new Vector3(transform.position.x, transform.position.y, -1); // Hack
    }

    void MoveInRoom()
    {
        if (room == null)
        {
            return;
        }

        var movement = GetComponent<AlienMovement>();
        if (movement.reachedDestination && Random.value < Time.deltaTime * 0.2f)
        {
            MoveToRandomPositionInRoom();
        }
    }

    private void MoveToRandomPositionInRoom()
    {
        var randomPositionInRoom = room.transform.position.x + Random.Range(-room.roomWidth / 2, room.roomWidth / 2);
        var destination = new Vector2(randomPositionInRoom, 0);
        GetComponent<AlienMovement>().SetDestination(destination);
    }
    

    void LateUpdate()
    {
        if (room != null)
        {
            // Leave hotel at the top of the loop to be trigger a frame after roomStayDuration update (prevent bug in happiness calcul)
            if (roomStayDuration == 0)
            {
                LeaveHotel();
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 1f;

                UpdateHappiness();

                UpdateRemainingTime();
            }
        }
    }

    public void MoveToRoom(Room newRoom)
    {
        room = newRoom;
        room.AddAlien(this);
        MoveToRandomPositionInRoom();
        LineManager.instance.RemoveAlien(this);
    }

    private void UpdateHappiness()
    {
        var statisfaction = desires.Sum(desire => desire.satisfaction(this));
        happiness += statisfaction * Time.deltaTime;
    }

    private void UpdateRemainingTime()
    {
        roomStayDuration = Mathf.Max(0, roomStayDuration - 1);
    }

    private void SetRandomRoomStayDuration()
    {
        int baseDuration = Random.Range(15, 20);
        int multiplier = StarManager.instance.Stars;

        // Convert the multiplier to a factor between 100 (1x) and 300 (3x)
        int factor = 100 + 2 * multiplier;

        roomStayDuration = (baseDuration * factor) / 100;
    }

    private void LeaveHotel()
    {
        if (happiness > 0)
        {
            MoneyManager.instance.CustomerPay(10);

            if (happiness > 80)
            {
                StarManager.instance.CustomerTip();
                // TODO trigger sound effect
            }
        }

        room.RemoveAlien(this);

        room = null;

        // TODO Trigger Animation
        // TODO Destroy Gameobject at the end of animation
        GameObject.Destroy(this.gameObject);
    }

    public void MoveTo(Vector2 position)
    {
        GetComponent<AlienMovement>().SetDestination(position);
    }

    public void ExpressDesires()
    {
        likeBox.gameObject.SetActive(true);
    }

    public void CloseExpressDesires()
    {
        likeBox.gameObject.SetActive(false);
    }
    
    void OnMouseOver()
    {
        ExpressDesires();
    }

    void OnMouseExit()
    {
        CloseExpressDesires();
    }

    public void MoveToLobby()
    {
        room?.RemoveAlien(this);
        room = null;
        LineManager.instance.AddAlien(this);
    }

    public void MoveToSpace()
    {
        room?.RemoveAlien(this);
        room = null;
        LineManager.instance.RemoveAlien(this);
        GetComponent<AlienMovement>().isInSpace = true;
        GetComponent<AlienDragDrop>().enabled = false;
        var rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.drag = 0;
        rb.velocity = transform.position.normalized * 0.2f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity = Random.Range(-300f, 300f);
    }
}