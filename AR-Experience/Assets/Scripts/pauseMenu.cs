using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
  public Button m_pauseButton;
  public Button m_resumeButton;
  public Button m_pauseExitButton;

  public GameObject m_pauseScreen;

  bool m_paused = false;
  public bool m_showing = false;

  // Start is called before the first frame update
  void Start()
  {
    m_pauseButton.onClick.AddListener(OnButtonPause);
    m_resumeButton.onClick.AddListener(OnButtonResume);
    m_pauseExitButton.onClick.AddListener(OnButtonExit);
  }

  
  public void OnButtonExit()
  {
    Time.timeScale = 1.0f;
    if (PlayerPrefs.HasKey("FirtsLevel"))
    {
      PlayerPrefs.DeleteKey("FirtsLevel");
    }
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
      m_showing = true;
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
    m_showing = false;
    var laman = FindObjectOfType<LAManager>();
    if (null != laman)
    {
      laman.mouseExitButton();
    }
  }
}
