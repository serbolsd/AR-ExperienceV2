using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IDVGameManager : MonoBehaviour
{
  public IDVPlayer m_player;
  public IDVEnemyManager m_enemyManager;

  public Button m_startButton;
  public Button m_restartButton;
  public Button m_exitButton;
  public Button m_pauseButton;
  public Button m_resumeButton;
  public Button m_pauseExitButton;

  public GameObject m_gameStartScreen;
  public GameObject m_gameOverScreen;
  public GameObject m_pauseScreen;

  public Text m_winOrLoseText;

  bool m_paused = false;

  void Start()
  {
    Time.timeScale = 1.0f;

    m_gameStartScreen.SetActive(true);
    m_gameOverScreen.SetActive(false);

    m_startButton.onClick.AddListener(OnButtonStart);
    m_restartButton.onClick.AddListener(OnButtonRestart);
    m_exitButton.onClick.AddListener(OnButtonExit);

    m_pauseButton.onClick.AddListener(OnButtonPause);
    m_resumeButton.onClick.AddListener(OnButtonResume);
    m_pauseExitButton.onClick.AddListener(OnButtonExit);
  }

  void Update()
  {
    if (m_player.m_life <= 0)
    {
      m_gameOverScreen.SetActive(true);
      m_winOrLoseText.text = "Game Over";
    }
    else if (m_enemyManager.m_roundEnded)
    {
      m_gameOverScreen.SetActive(true);
      m_winOrLoseText.text = "¡Ganaste!";
    }
  }

  public void OnButtonStart()
  {
    AudioManager.playSound(Sounds.button, 1.0f);
    m_gameStartScreen.SetActive(false);
    m_enemyManager.m_startSpawning = true;
  }

  public void OnButtonRestart()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void OnButtonExit()
  {
    Time.timeScale = 1.0f;
    SceneManager.LoadScene("MainMenu");
  }

  public void OnButtonPause()
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    if (!m_paused)
    {
      m_paused = true;
      Time.timeScale = 0.0f;
      m_pauseScreen.SetActive(true);
    }
    else
    {
      OnButtonResume();
    }
    
  }
  public void OnButtonResume()
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    m_paused = false;
    Time.timeScale = 1.0f;
    m_pauseScreen.SetActive(false);
  }
}
