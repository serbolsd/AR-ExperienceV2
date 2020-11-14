using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDVEnemy : MonoBehaviour
{
  public GameObject m_worldRoot;
  public IDVFollowInCanvas m_arrow;
  public IDVEnemyManager m_manager;
  public GameObject m_deathParticle;

  [Header("Target")]
  public Transform m_target;

  [Header("Stats Settings")]
  public int m_maxLife = 3;
  int m_currentLife = 3;
  public Slider m_lifeBar;

  [Header("Move Settings")]
  public float m_frontSpeed = 1.0f;
  public float m_sideSpeed = 1.0f;
  public float m_sideMovementLenght = 2.0f;
  Vector3 m_moveDir;

  [Header("Attack Settings")]
  // the prefab of bullet to intanciate when shooting
  public GameObject m_bulletPrefab;
  public float m_shootInterval = 1.5f;
  float m_shootIntervalCounter = 0.0f;

  public bool m_visible = false;
  void Start()
  {
    m_currentLife = m_maxLife;
    if (m_target == null)
    {
      m_target = Camera.main.transform;
    }
  }

  void Update()
  {
    if (null != m_arrow)
    {
      m_arrow.onUpdate();
    }
    LookAtTarget();
    MoveToTarget();

    if (m_shootIntervalCounter > m_shootInterval)
    {
      m_shootIntervalCounter = 0.0f;
      Shoot();
    }
    else
    {
      m_shootIntervalCounter += Time.deltaTime;
    }

    if (Vector3.Distance(transform.position, m_target.position) < 0.5f)
    {
      IDVPlayer player = m_target.GetComponent<IDVPlayer>();
      if (player != null)
      {
        player.m_life--;
        if (player.m_life > 0)
        {
          player.m_hearths[player.m_life - 1].SetActive(false);
        }
      }
      Destroy(gameObject);
    }
  }

  void LookAtTarget()
  {
    transform.LookAt(m_target);
  }

  void MoveToTarget()
  {
    Vector3 currPos = transform.position;

    //direction to target
    m_moveDir = (m_target.position - transform.position).normalized;

    //side movements
    m_moveDir += (Mathf.Cos(Time.time * m_sideSpeed) * transform.right * m_sideMovementLenght) + (Mathf.Sin(Time.time * m_sideSpeed) * transform.up * 0.5f * m_sideMovementLenght);

    //move
    currPos += m_moveDir * m_frontSpeed * Time.deltaTime;

    //apply movement
    transform.position = currPos;
  }

  public void Shoot()
  {
    Debug.Log("shoot");
    //Transform newTransform = transform;
    IDVBullet newbullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity, m_worldRoot.transform).GetComponent<IDVBullet>();
    //newbullet.transform = newTransform;
    newbullet.m_radius = 0.1f;
    newbullet.m_speed = 1.5f;
    newbullet.m_lifeTime = 7.0f;
    newbullet.onStart();
    newbullet.m_direction = (Camera.main.transform.position - transform.position).normalized;
    newbullet.m_playerShot = false;
    newbullet.setColorEnemy();
  }

  //reduces this enemy's life by the given parameter
  public void HurtEnemy(int dmgPoints)
  {
    AudioManager.playSound(Sounds.hit, 0.9f);

    m_currentLife -= dmgPoints;
    m_lifeBar.value = (float)m_currentLife/ (float)m_maxLife;
    if (m_currentLife <= 0)
    {
      m_manager.m_enemiesKilled++;
      AudioManager.playSound(Sounds.explosion, 0.9f);
      GameObject newParticle = Instantiate(m_deathParticle, transform.position, Quaternion.Euler(-90,0,0), m_worldRoot.transform);
      Destroy(m_arrow.gameObject);
      Destroy(gameObject);
    }
  }
}
