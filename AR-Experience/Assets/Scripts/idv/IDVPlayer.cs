using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDVPlayer : MonoBehaviour
{
  //parent of instanced objects
  public GameObject m_worldRoot;

  // Only foe check is collisioned
  public Text debugCollision;

  // the player's points life 
  public int m_life = 5;

  public GameObject[] m_hearths = new GameObject[5];

  // the prefab of bullet to intanciate when shooting
  public GameObject bullet;

  // Start is called before the first frame update
  void Start()
  {
    debugCollision.text = "No collision";
  }

  // Update is called once per frame
  void Update()
  {

  }

  // function to instantiate a bullet with the forward of player like the direction
  public void shoot()
  {
    AudioManager.playSound(Sounds.shoot, 0.5f);
    IDVBullet newbullet= Instantiate(bullet,transform.position, Quaternion.identity, m_worldRoot.transform).GetComponent<IDVBullet>();
    newbullet.m_direction = transform.forward;
    newbullet.m_speed = 10;
    newbullet.onStart();
    newbullet.m_playerShot = true;
    newbullet.setColorPlayer();
  }

  private void OnTriggerEnter(Collider other)
  {
    debugCollision.text = "In collision";
  }

  private void OnTriggerStay(Collider other)
  {
    debugCollision.text = "In collision";
  }

  private void OnTriggerExit(Collider other)
  {
    debugCollision.text = "No collision";
  }
}
