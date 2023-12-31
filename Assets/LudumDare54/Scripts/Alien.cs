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
  public bool exiting = false;

  public AlienAnimator animator;

  void Start()
  {
    SetRandomRoomStayDuration();
    desires = new DesireGenerator().GenerateDesire();
    likeBox.Initialize(desires);
    AudioManager.instance.PlaySpawnAudio();
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


    if (roomStayDuration == 0 || happiness == -5 || happiness == 10)
    {
      LeaveHotel();
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
    AudioManager.instance.PlayDropInRoomAudio();
  }

  private void UpdateHappiness()
  {
    if (exiting)
    {
      return;
    }

    var statisfaction = desires.Sum(desire => desire.satisfaction(this));
    happiness = Mathf.Clamp(happiness + statisfaction * Time.deltaTime, -5, 10);

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
    maxRoomStayDuration = 15;
    roomStayDuration = maxRoomStayDuration;
  }


  private void LeaveHotel()
  {
    if (exiting)
    {
      return;
    }

    this.GetComponent<AlienDragDrop>().enabled = false;

    exiting = true;
    var pay = 5 + Mathf.Max(0, 50 + (Mathf.RoundToInt(happiness) * 3));
    MoneyManager.instance.CustomerPay(pay);

    if (happiness >= 5)
    {
      StarManager.instance.CustomerTip();
      GetComponent<AlienStarIndicator>().Play(1);
      animator.SetEmotion(happiness);
    }
    else if (happiness <= -5)
    {
      StarManager.instance.CustomerHate(2);
      GetComponent<AlienStarIndicator>().Play(-2);
      animator.SetEmotion(happiness);
    }
    else
    {
      animator.SetEmotion(0);
    }


    LineManager.instance.RemoveAlien(this);
    room?.RemoveAlien(this);

    room = null;

    GameObject exitWarp = GameObject.FindWithTag("exit_warp");


    this.transform.position = exitWarp.transform.position;
    MoveTo(exitWarp.transform.position);


    Invoke("WalkToExit", 1f);
  }

  private void WalkToExit()
  {
    GameObject exit = GameObject.FindWithTag("exit");
    MoveTo(exit.transform.position);
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
    if (room != null || LineManager.instance.GetNextCustomer() == this)
    {
      ExpressDesires();
    }
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

    if (StarManager.instance.MaxStarsLevel > 15)
    {
      StarManager.instance.CustomerHate(3);
    }
    else if (StarManager.instance.MaxStarsLevel > 10)
    {
      StarManager.instance.CustomerHate(2);
    }
    else
    {
      StarManager.instance.CustomerHate(1);
    }

    Destroy(this.gameObject, 3f);
  }
}