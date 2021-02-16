using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LPAManager : MonoBehaviour
{
  public float m_timer = 120;
  public GameObject m_gameOverLose;
  public GameObject m_instrucScreen;
  public GameObject m_timerTxt;
  public bool m_timerActive = true;
  public LPAChangePropertys m_changePropertys;
  public Button m_pause;
  public Button m_info;

  public IDVFollowInCanvas arrow;

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }

  void Start()
  {
    if (m_timerActive)
    {
      m_timerTxt.SetActive(true);
      
    }
    else
    {
      m_timerTxt.SetActive(false);
    }
    m_instrucScreen.SetActive(true);
    Time.timeScale = 0;
  }

  void Update()
  {
    if (m_changePropertys.m_gameOver)
    {
      m_pause.interactable = false;
      m_info.interactable = false;
      return;
    }
    arrow.onUpdate();
    m_timer -= Time.deltaTime;
    int entero = (int)m_timer;
    m_timerTxt.GetComponent<Text>().text = entero.ToString();
    if (m_timer <= 0 && m_timerActive)
    {
      AudioManager.playSound(Sounds.wrong);
      m_timer = 0;
      m_gameOverLose.SetActive(true);
      m_changePropertys.m_gameOver = true;
    }
  }
  public void again()
  {
    SceneManager.LoadScene("LpaGame");
  }

  public void exit()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void start()
  {
    m_instrucScreen.SetActive(false);
    Time.timeScale = 1;
  }
}
