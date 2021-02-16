using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class LPAChangePropertys : MonoBehaviour
{
  public LPATargetManager m_targetManager;
  public PostProcessLayer m_camPostProcess;
  public LensDistortion m_camLensDistorcion;
  public DepthOfField m_camDepthOfField;
  public ColorGrading m_camColorGrading;
  public FloatParameter m_focusDistance;
  public FloatParameter m_saturation;
  public FloatParameter m_contraste;
  public FloatParameter m_Distorcion;

  public lpaUI m_UI;
  public Image m_focusSquare;

  float m_minRange = 4.5f;
  float m_maxRange = 5.0f;

  float m_ObjetiveSaturation = 0.0f;
  float m_minSaturation = 0.0f;
  float m_maxSaturation = 0.0f;
  float m_ObjetiveContrast = 0.0f;
  float m_minContrast = 0.0f;
  float m_maxContrast = 0.0f;
  float m_ObjetiveDistorcion = 0.0f;
  float m_minDistorcion = 0.0f;
  float m_maxDistorcion = 0.0f;

  bool m_focusCorrect = false;
  bool m_saturationCorrect = false;
  bool m_contrastCorrect = false;
  bool m_lookCorrect = false;

  public bool m_gameOver = false;

  public GameObject gameOverWindow;

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
    m_UI = FindObjectOfType<lpaUI>();

    m_camLensDistorcion = m_camPostProcess.GetSettings<LensDistortion>();
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

    m_UI.SetSliderValue(lpaUI.SLIDERS.kFocalLenght, m_focusDistance.value);
    m_UI.SetSliderValue(lpaUI.SLIDERS.kSaturation, m_saturation.value);
    m_UI.SetSliderValue(lpaUI.SLIDERS.kContrast, m_contraste.value);

    calculateRequeriments();
  }

  // Update is called once per frame
  void Update()
  {
    if (m_gameOver)
    {
      return;
    }
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
      m_focusCorrect = true;
    }
    else
    {
      m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kFocalLenght);
      m_focusCorrect = false;
    }
    if (checkSaturation())
    {
      m_UI.SetSliderColor(Color.green, lpaUI.SLIDERS.kSaturation);
      m_saturationCorrect = true;
    }
    else
    {
      m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kSaturation);
      m_saturationCorrect = false;
    }
    if (checkContrast())
    {
      m_UI.SetSliderColor(Color.green, lpaUI.SLIDERS.kContrast);
      m_contrastCorrect = true;
    }
    else
    {
      m_UI.SetSliderColor(Color.red, lpaUI.SLIDERS.kContrast);
      m_contrastCorrect = false;
    }
    //if (checkDistorcion())
    //{
    //
    //}

    //is looking at target
    Ray raycast = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    RaycastHit hit;
    if (Physics.Raycast(raycast, out hit, 10000))
    {
      if (hit.transform.gameObject == m_targetManager.m_currTarget)
      {
        m_lookCorrect = true;
        m_focusSquare.color = Color.green;
      }
      else
      {
        m_lookCorrect = false;
        m_focusSquare.color = Color.white;
      }
    }
  }

  void calculateFocalDistance()
  {
    float distance = Vector3.Distance(m_targetManager.m_currTarget.transform.position, Camera.main.transform.position);
    m_maxRange = distance;
    m_minRange = distance - 1.0f;
    Debug.Log(distance);
  }

  void calculateRequeriments()
  {
    m_ObjetiveSaturation = Random.Range(-100, 100);
    m_maxSaturation = m_ObjetiveSaturation + 10;
    if (m_maxSaturation > 100)
      m_maxSaturation = 100;
    m_minSaturation = m_ObjetiveSaturation - 10;
    if (m_minSaturation < -100)
      m_minSaturation = -100;

    m_ObjetiveContrast = Random.Range(-50, 100);
    m_maxContrast = m_ObjetiveContrast + 10;
    if (m_maxContrast > 100)
      m_maxContrast = 100;
    m_minContrast = m_ObjetiveContrast - 10;
    if (m_minContrast < -50)
      m_minContrast = -50;

    m_ObjetiveDistorcion = Random.Range(-30, 30);
    m_maxDistorcion = m_ObjetiveDistorcion + 5;
    if (m_maxDistorcion > 30)
      m_maxDistorcion = 30;
    m_minDistorcion = m_ObjetiveDistorcion - 5;
    if (m_minDistorcion < -30)
      m_minDistorcion = -30;
  }

  bool checkFocusDistance()
  {
    calculateFocalDistance();
    if (m_focusDistance.value <= m_maxRange &&
        m_focusDistance.value >= m_minRange)
    {
      return true;
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

  public void takePhoto()
  {
    if (m_gameOver)
    {
      return;
    }
    if (m_lookCorrect && m_focusCorrect && m_saturationCorrect && m_contrastCorrect)
    {
      Debug.Log("correct target");
      if (m_targetManager.ChangeTarget())
      {
        calculateRequeriments();
      }
      else
      {
        m_gameOver = true;
        gameOverWindow.SetActive(true);
      }
      AudioManager.playSound(Sounds.photo, 1.0f);
      AudioManager.playSound(Sounds.correct, 0.2f);
    }
    else
    {
      Debug.Log("wrong target");
      AudioManager.playSound(Sounds.wrong, 0.9f);
    }
  }
}
