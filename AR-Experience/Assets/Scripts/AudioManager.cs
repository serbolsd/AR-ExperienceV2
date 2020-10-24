using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
  shoot = 0,
  explosion,
  hit,
  hurt,
  jump,
  place,
  correct,
  button,
  click,
};

public class AudioManager : MonoBehaviour
{
  private static AudioSource m_audioSource;
  private static AudioClip[] m_Sounds = new AudioClip[Sounds.GetNames(typeof(Sounds)).Length]; //size of the enum

  //audio clips
  public AudioClip shoot;
  public AudioClip explosion;
  public AudioClip hit;
  public AudioClip hurt;
  public AudioClip jump;
  public AudioClip place;
  public AudioClip correct;
  public AudioClip button;
  public AudioClip click;

  void Awake()
  {
    m_audioSource = GetComponent<AudioSource>();
    linkToEnum(Sounds.shoot, shoot);
    linkToEnum(Sounds.explosion, explosion);
    linkToEnum(Sounds.hit, hit);
    linkToEnum(Sounds.hurt, hurt);
    linkToEnum(Sounds.jump, jump);
    linkToEnum(Sounds.place, place);
    linkToEnum(Sounds.correct, correct);
    linkToEnum(Sounds.button, button);
    linkToEnum(Sounds.click, click);
  }

  // Start is called before the first frame update
  void Start()
  {
  }

  private void Update()
  {
  }

  void linkToEnum(Sounds soundType, AudioClip clip)
  {
    m_Sounds[(int)soundType] = clip;
  }

  public static void playSound(Sounds sound, float volume = 1)
  {
    m_audioSource.PlayOneShot(m_Sounds[(int)sound], volume);
  }

  public static void playSound3D(Sounds sound, Vector3 position, float volume = 1)
  {
    AudioSource.PlayClipAtPoint(m_Sounds[(int)sound], position, volume);
  }

}

