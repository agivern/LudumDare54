using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [SerializeField] AudioSource spawn;
  [SerializeField] AudioSource lostStar;
  [SerializeField] AudioSource earnStar;
  [SerializeField] AudioSource dropInRoom;

  public static AudioManager instance;

  private void Awake()
  {
    instance = this;
  }

  public void PlaySpawnAudio()
  {
    spawn.Play();
  }

  public void PlayEarnStarAudio()
  {
    earnStar.Play();
  }

  public void PlayLostStarAudio()
  {
    lostStar.Play();
  }

  public void PlayDropInRoomAudio()
  {
    dropInRoom.Play();
  }
}
