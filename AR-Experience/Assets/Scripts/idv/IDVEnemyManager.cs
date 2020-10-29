using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDVEnemyManager : MonoBehaviour
{
  public GameObject m_worldRoot;
  public GameObject m_canvas;
  public GameObject[] m_enemyPrefab;
  public GameObject m_arrowPrefab;
  public Text m_enemyCountText;

  //The game manager activates this when the game starts
  public bool m_startSpawning = false;
  public bool m_roundEnded = false;

  //total enemies to spawn this round
  public int m_totalEnemies = 10;
  int m_totalEnemiesCounter = 0;
  public int m_enemiesKilled = 0;

  //distance to spawn the enemies from the player
  public float m_spawnDistance;

  //width*0.5f of the enemy spawn area
  public float m_spawnAperture;

  //height to instantiate the enemies
  public float m_spawnHeight = 0.0f;

  //time between enemies spawns
  public float m_spawnInterval = 1.0f;
  float m_spawnIntervalCounter = 0.0f;


  void Start()
  {
    m_enemyCountText.text = m_enemiesKilled + "/" + m_totalEnemies;
  }

  void Update()
  {
    if (!m_startSpawning)
    {
      return;
    }

    m_enemyCountText.text = m_enemiesKilled + "/" + m_totalEnemies;

    if (m_totalEnemiesCounter < m_totalEnemies)
    {
      if (m_spawnIntervalCounter > m_spawnInterval)
      {
        m_spawnIntervalCounter = 0.0f;
        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.z = m_spawnDistance;
        spawnPoint.x = Random.Range(-m_spawnAperture, m_spawnAperture);
        spawnPoint.y = m_spawnHeight;
        GameObject instancedEnemy = Instantiate(m_enemyPrefab[Random.RandomRange(0, m_enemyPrefab.Length)], spawnPoint, Quaternion.identity, m_worldRoot.transform);
        instancedEnemy.GetComponent<IDVEnemy>().m_worldRoot = m_worldRoot;
        instancedEnemy.GetComponent<IDVEnemy>().m_manager = this;
        GameObject Arrow = Instantiate(m_arrowPrefab, m_canvas.transform);
        Arrow.GetComponent<IDVFollowInCanvas>().objetToFollow = instancedEnemy;
        Arrow.GetComponent<IDVFollowInCanvas>().canvas = m_canvas.GetComponent<RectTransform>();
        instancedEnemy.GetComponent<IDVEnemy>().m_arrow = Arrow.GetComponent<IDVFollowInCanvas>();
        m_totalEnemiesCounter++;
      }
      else
      {
        m_spawnIntervalCounter += Time.deltaTime;
      }
    }
    else
    {
      if (FindObjectsOfType<IDVEnemy>().Length == 0)
      {
        m_roundEnded = true;
      }
    }
    
  }
}
