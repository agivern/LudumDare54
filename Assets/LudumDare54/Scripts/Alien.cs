using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    public Room room { get; private set; }
    public LikeBox likeBox;
    public AlienRace race = AlienRace.Green;
    [SerializeField] Image timerImage;

    private int maxRoomStayDuration;
    private float roomStayDuration;
    private float happiness = 0;
    private List<Desire> desires = new List<Desire>();
    private bool exiting = false;

    public AlienAnimator animator;

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
        MoveTo(destination);
    }


    void LateUpdate()
    {
        UpdateHappiness();

        if (room != null)
        {
            // Leave hotel at the top of the loop to be trigger a frame after roomStayDuration update (prevent bug in happiness calcul)
            if (roomStayDuration == 0)
            {
                LeaveHotel();
            }
        }

        UpdateRemainingTime();
        UpdateTimerImage();
    }

    private void UpdateTimerImage()
    {
        timerImage.transform.parent.gameObject.SetActive(room != null);
        timerImage.fillAmount = roomStayDuration / maxRoomStayDuration;
    }

    public void MoveToRoom(Room newRoom)
    {
        room?.RemoveAlien(this);
        room = newRoom;
        room.AddAlien(this);
        MoveToRandomPositionInRoom();
        LineManager.instance.RemoveAlien(this);
    }

    private void UpdateHappiness()
    {
        var statisfaction = desires.Sum(desire => desire.satisfaction(this));
        happiness += statisfaction * Time.deltaTime;

        animator.SetEmotion(statisfaction);
    }

    private void UpdateRemainingTime()
    {
        if (room == null)
        {
            return;
        }
        roomStayDuration = Mathf.Max(0, roomStayDuration - Time.deltaTime);
    }

    private void SetRandomRoomStayDuration()
    {
        int baseDuration = Random.Range(15, 20);
        int multiplier = StarManager.instance.Stars;

        // Convert the multiplier to a factor between 100 (1x) and 300 (3x)
        int factor = 100 + 2 * multiplier;

        maxRoomStayDuration = (baseDuration * factor) / 100;
        roomStayDuration = maxRoomStayDuration;
    }


    private void LeaveHotel()
    {
        exiting = true;
        var pay = 5 + Mathf.Max(0, 50 + (Mathf.RoundToInt(happiness) * 3));
        MoneyManager.instance.CustomerPay(pay);
        Debug.Log(happiness);
        if (happiness >= 5)
        {
            StarManager.instance.CustomerTip();
        }
        else if (happiness <= -5)
        {
            StarManager.instance.CustomerHate();
        }


        room.RemoveAlien(this);

        room = null;

        GameObject exitWarp = GameObject.FindWithTag("exit_warp");
        GameObject exit = GameObject.FindWithTag("exit");
        if (exitWarp != null)
        {
            this.transform.position = exitWarp.transform.position;
            MoveTo(exit.transform.position);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("exit") && exiting)
        {
            GameObject.Destroy(this.gameObject);
        }
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

        StarManager.instance.CustomerHate();
    }
}