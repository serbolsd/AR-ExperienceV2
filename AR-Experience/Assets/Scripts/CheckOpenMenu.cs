using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckOpenMenu : MonoBehaviour
{
  public pauseMenu m_pause;
  public infoMenu m_info;

  public void pauseButton()
  {
    if (m_info.m_showing)
    {
      m_info.OnButtonResume();
    }
  }

  public void infoButton()
  {
    if (m_pause.m_showing)
    {
      m_pause.OnButtonResume();
    }
  }
}
