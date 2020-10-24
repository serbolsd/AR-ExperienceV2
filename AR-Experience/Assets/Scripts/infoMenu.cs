using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoMenu : MonoBehaviour
{
  public Button m_infoButton;
  public Button m_closeButton;
  public Button m_xButton;

  public GameObject m_infoScreen;

  bool m_paused = false;

  // Start is called before the first frame update
  void Start()
  {
    m_infoButton.onClick.AddListener(OnButtonPause);
    m_closeButton.onClick.AddListener(OnButtonResume);
    m_xButton.onClick.AddListener(OnButtonResume);
  }


  public void OnButtonPause()
  {
    if (!m_paused)
    {
      m_paused = true;
      Time.timeScale = 0.0f;
      m_infoScreen.SetActive(true);
    }
    else
    {
      OnButtonResume();
    }

  }
  public void OnButtonResume()
  {
    m_paused = false;
    Time.timeScale = 1.0f;
    m_infoScreen.SetActive(false);
  }
}
