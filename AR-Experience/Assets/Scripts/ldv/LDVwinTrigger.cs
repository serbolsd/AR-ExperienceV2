using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LDVwinTrigger : MonoBehaviour
{
  public GameObject m_gameOverScreen;
  public Button m_restartButton;
  public Button m_exitButton;

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }

  private void Start()
  {
    m_restartButton.onClick.AddListener(OnButtonRestart);
    m_exitButton.onClick.AddListener(OnButtonExit);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      other.GetComponent<LDVplayer>().m_active = false;
      m_gameOverScreen.SetActive(true);
      AudioManager.playSound(Sounds.correct, 1.0f);
    }
  }

  public void OnButtonRestart()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void OnButtonExit()
  {
    SceneManager.LoadScene("MainMenu");
  }
}
