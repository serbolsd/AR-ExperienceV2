using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDVBullet : MonoBehaviour
{

  // Set the radius to the esphere
  [Range(0.02f, 5.0f)]
  public float m_radius;

  // If was shooted by the player is true 
  public bool m_playerShot = false;

  // The speed of bullet
  [Range(0.2f, 5.0f)]
  public float m_speed;

  // Time to destroy the bullet by itself
  [Range(0.2f, 5.0f)]
  public float m_lifeTime;

  // The direcction to move
  public Vector3 m_direction;

  // The color if the bulled was shooted by the player
  public Material m_materialPlayer;

  // The color if the bulled was shooted by a enemy
  public Material m_materialEnemy;

  // Start is called before the first frame update
  void Start()
  {
    float radius = m_radius * 2;
    transform.localScale = new Vector3(radius, radius, radius);
    Destroy(this.gameObject, m_lifeTime);
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 pos = transform.position;
    pos += m_direction * m_speed * Time.deltaTime;
    transform.position = pos;
  }

  // Set color for is player shoot
  public void setColorPlayer()
  {
    MeshRenderer mr = this.GetComponent<MeshRenderer>();
    if (mr)
    {
      mr.material = m_materialPlayer;
    }
  }

  // Set color for is enemy shoot
  public void setColorEnemy()
  {
    MeshRenderer mr = this.GetComponent<MeshRenderer>();
    if (mr)
    {
      mr.material = m_materialEnemy;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    IDVPlayer player = other.GetComponent<IDVPlayer>();

    if (m_playerShot && !player) //if is a player shoot destroy de any other object
    {
      IDVEnemy enemy = other.GetComponent<IDVEnemy>();
      if (enemy != null)
      {
        enemy.HurtEnemy(1);
      }

      Destroy(this.gameObject);
    }
    else if (!m_playerShot && player) // if is a enemy shoot and collisioned with the player
    {
      if (player)
      {
        if (player.m_life > 0)
        {
          AudioManager.playSound(Sounds.hurt, 1.0f);
          player.m_hearths[player.m_life - 1].SetActive(false);
        }
        --player.m_life; // quit a life point
      }
    }
  }
}
