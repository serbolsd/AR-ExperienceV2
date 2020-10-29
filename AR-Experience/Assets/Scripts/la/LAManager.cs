using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HSVPicker;
public class LAManager : MonoBehaviour
{
  public LADrawLine m_drawLine = null;
  public GameObject m_colors;
  public GameObject m_brushs;
  public GameObject m_btnOkColor;

  bool m_bDraw = true;
  bool m_bBrushesOpen = false;
  bool m_bColorsOpen = false;
  bool m_bColoring = false;
  bool m_bMouseOnButton = false;

  public ColorPicker m_color;
  public GameObject m_toggle;
  public GameObject m_R;
  public GameObject m_G;
  public GameObject m_B;
  public GameObject m_A;
  public GameObject m_pre;

  // Start is called before the first frame update
  void Start()
  {
    m_drawLine = FindObjectOfType<LADrawLine>();
    if (null != m_drawLine)
    {
      m_drawLine.onStart();
    }
  }

  // Update is called once per frame
  void Update()
  {
    if(m_bColoring)
    {
      m_toggle.SetActive(false);
      m_R.SetActive(false);
      m_G.SetActive(false);
      m_B.SetActive(false);
      m_A.SetActive(false);
      m_pre.SetActive(false);
    }
    if (null != m_drawLine && !m_bMouseOnButton)
    {
      m_drawLine.onUpdate();
    }
  }

  public void selectColor()
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    if (m_bColorsOpen)
    {
      m_bColorsOpen = false;
      m_colors.SetActive(false);
      m_btnOkColor.SetActive(false);
      m_bColoring = false;
    }
    else
    {
      m_bColorsOpen = true;
      m_toggle.SetActive(false);
      m_R.SetActive(false);
      m_G.SetActive(false);
      m_B.SetActive(false);
      m_A.SetActive(false);
      m_pre.SetActive(false);
      m_toggle.SetActive(false);
      m_R.SetActive(false);
      m_G.SetActive(false);
      m_B.SetActive(false);
      m_A.SetActive(false);
      m_pre.SetActive(false);
      m_btnOkColor.SetActive(true);
      m_colors.SetActive(true);
      m_bColoring = true;
    }
  }

  public void selectBrush()
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    if (m_bBrushesOpen)
    {
      m_bBrushesOpen = false;
      m_brushs.SetActive(false);
    }
    else
    {
      m_bBrushesOpen = true;
      m_brushs.SetActive(true);
    }
  }

  public void okColor()
  {
    m_drawLine.UpdateUIBrushColor(m_color.CurrentColor);

    AudioManager.playSound(Sounds.click, 1.0f);
    //m_bDraw = true;
    m_colors.SetActive(false);
    m_btnOkColor.SetActive(false);
    m_bColoring = false;
    m_bColorsOpen = false;
    m_bMouseOnButton = false;

  }

  public void okBrush()
  {

    //m_bDraw = true;
    //m_brushs.SetActive(false);
    //m_bMouseOnButton = false;
  }

  public void mouseOnButton()
  {
    m_bMouseOnButton = true;
  }
  public void mouseExitButton()
  {
    m_bMouseOnButton = false;
  }

  
}
