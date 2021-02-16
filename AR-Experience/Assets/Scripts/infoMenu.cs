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
  public bool m_showing = false;
  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }
  // Start is called before the first frame update
  void Start()
  {
    m_infoButton.onClick.AddListener(OnButtonPause);
    m_closeButton.onClick.AddListener(OnButtonResume);
    m_xButton.onClick.AddListener(OnButtonResume);
  }


  public void OnButtonPause()
  {
    AudioManager.playSound(Sounds.click, 1.0f);

    if (!m_paused)
    {
      m_paused = true;
      Time.timeScale = 0.0f;
      m_infoScreen.SetActive(true);
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
    m_infoScreen.SetActive(false);
    m_showing = false;
    var laman = FindObjectOfType<LAManager>();
    if (null != laman)
    {
      laman.mouseExitButton();
    }
  }
}
