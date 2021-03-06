﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDVEnemyManager : MonoBehaviour
{
  public GameObject m_worldRoot;
  public GameObject[] m_enemyPrefab;

  //The game manager activates this when the game starts
  public bool m_startSpawning = false;
  public bool m_roundEnded = false;

  //total enemies to spawn this round
  public int m_totalEnemies = 10;
  int m_totalEnemiesCounter = 0;

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

  }

  void Update()
  {
    if (!m_startSpawning)
    {
      return;
    }

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
