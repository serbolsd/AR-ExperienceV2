using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LPAChangePropertys : MonoBehaviour
{
  public PostProcessLayer m_camPostProcess;
  public LensDistortion m_camLensDistorcion;
  public DepthOfField m_camDepthOfField;
  public ColorGrading m_camColorGrading;
  public FloatParameter m_focusDistance;
  public FloatParameter m_saturation;
  public FloatParameter m_contraste;
  public FloatParameter m_Distorcion;

  public lpaUI m_UI;

  const float m_minRange1 = 4.5f;
  const float m_maxRange1 = 5.0f;
  const float m_minRange2 = 6.7f;
  const float m_maxRange2 = 7.5f;
  const float m_minRange3 = 8.2f;
  const float m_maxRange3 = 9.5f;

  float m_ObjetiveSaturation = 0.0f;
  float m_minSaturation = 0.0f;
  float m_maxSaturation = 0.0f;
  float m_ObjetiveContrast = 0.0f;
  float m_minContrast = 0.0f;
  float m_maxContrast = 0.0f;
  float m_ObjetiveDistorcion = 0.0f;
  float m_minDistorcion = 0.0f;
  float m_maxDistorcion = 0.0f;

  int cube;
  // Start is called before the first frame update
  void Start()
  {
    m_UI = FindObjectOfType<lpaUI>();

    cube = Random.Range(0,2);
    m_camLensDistorcion=m_camPostProcess.GetSettings<LensDistortion>();
    m_camDepthOfField = m_camPostProcess.GetSettings<DepthOfField>();
    m_camColorGrading = m_camPostProcess.GetSettings<ColorGrading>();
    m_focusDistance.value = Random.Range(0.1f, 10.0f);
    m_camDepthOfField.focusDistance = m_focusDistance;
    m_saturation.value = 0.0f;
    m_contraste.value = 0.0f;
    m_Distorcion.value = 0.0f;

    m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kFocalLenght);
    m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kSaturation);
    m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kContrast);

    m_UI.SetSliderValue(lpaUI.SLIDERS.kFocalLenght,m_focusDistance.value);
    m_UI.SetSliderValue(lpaUI.SLIDERS.kSaturation, m_saturation.value);
    m_UI.SetSliderValue(lpaUI.SLIDERS.kContrast,m_contraste.value);

    m_ObjetiveSaturation = Random.Range(-100,100);
    m_maxSaturation = m_ObjetiveSaturation + 10;
    if (m_maxSaturation>100)
      m_maxSaturation = 100;
    m_minSaturation = m_ObjetiveSaturation - 10;
    if (m_minSaturation < -100)
      m_minSaturation = -100;

    m_ObjetiveContrast = Random.Range(-50,100);
    m_maxContrast = m_ObjetiveContrast + 10;
    if (m_maxContrast > 100)
      m_maxContrast = 100;
    m_minContrast = m_ObjetiveContrast - 10;
    if (m_minContrast < -50)
      m_minContrast = -50;

    m_ObjetiveDistorcion = Random.Range(-30,30);
    m_maxDistorcion = m_ObjetiveDistorcion + 5;
    if (m_maxDistorcion > 30)
      m_maxDistorcion = 30;
    m_minDistorcion = m_ObjetiveDistorcion - 5;
    if (m_minDistorcion < -30)
      m_minDistorcion = -30;
  }

  // Update is called once per frame
  void Update()
  {
    m_focusDistance.value = m_UI.GetSliderValue(lpaUI.SLIDERS.kFocalLenght);
    m_saturation.value = m_UI.GetSliderValue(lpaUI.SLIDERS.kSaturation);
    m_contraste.value = m_UI.GetSliderValue(lpaUI.SLIDERS.kContrast);

    m_camDepthOfField.focusDistance = m_focusDistance;
    m_camColorGrading.saturation = m_saturation;
    m_camColorGrading.contrast = m_contraste;
    m_camLensDistorcion.intensity = m_Distorcion;
    if (checkFocusDistance())
    {
      m_UI.SetSliderColor(Color.green, lpaUI.SLIDERS.kFocalLenght);
    }
    else
    {
      m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kFocalLenght);
    }
    if (checkSaturation())
    {
      m_UI.SetSliderColor(Color.green, lpaUI.SLIDERS.kSaturation);
    }
    else
    {
      m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kSaturation);
    }
    if (checkContrast())
    {
      m_UI.SetSliderColor(Color.green, lpaUI.SLIDERS.kContrast);
    }
    else
    {
      m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kContrast);
    }
    //if (checkDistorcion())
    //{
    //
    //}
  }

  bool checkFocusDistance()
  {
    switch (cube)
    {
      case 0:
        if (m_focusDistance.value <= m_maxRange1 && 
            m_focusDistance.value >= m_minRange1)
        {
          return true;
        }
        break;
      case 1:
        if(m_focusDistance.value <= m_maxRange2 && 
           m_focusDistance.value >= m_minRange2)
        {
          return true;
        }
        break;
      case 2:
        if(m_focusDistance.value <= m_maxRange2 && 
           m_focusDistance.value >= m_minRange2)
        {
          return true;
        }
        break;
      default:
        break;
    }
    return false;
  }

  bool checkSaturation()
  {
    return compare(m_saturation.value, m_minSaturation, m_maxSaturation);
  }
  bool checkContrast()
  {
    return compare(m_contraste.value, m_minContrast, m_maxContrast);
  }
  bool checkDistorcion()
  {
    return compare(m_Distorcion.value, m_minDistorcion, m_maxDistorcion);
  }

  bool compare(float value, float minRange, float maxRange)
  {
    if (value <= maxRange && value >= minRange)
    {
      return true;
    }
    return false;
  }
}
