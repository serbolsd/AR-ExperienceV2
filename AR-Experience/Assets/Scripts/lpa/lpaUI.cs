using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lpaUI : MonoBehaviour
{
  public enum SLIDERS
  {
    kContrast = 0,
    kSaturation,
    kFocalLenght
  }

  public Slider m_contrasteSlider;
  public Slider m_saturacionSlider;
  public Slider m_focalLenghtSlider;

  public Text m_contrasteText;
  public Text m_saturacionText;
  public Text m_focalLenghtText;

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }
  // Update is called once per frame
  void Update()
  {
    m_contrasteText.text = RoundNumberDigits(m_contrasteSlider.value, 2).ToString();
    m_saturacionText.text = RoundNumberDigits(m_saturacionSlider.value, 2).ToString();
    m_focalLenghtText.text = RoundNumberDigits(m_focalLenghtSlider.value, 2).ToString();
  }

  public float RoundNumberDigits(float value, int digits)
  {
    float mult = Mathf.Pow(10.0f, (float)digits);
    return Mathf.Round(value * mult) / mult;
  }

  public void SetSliderColor(Color _color, SLIDERS _slider)
  {
    switch (_slider)
    {
      case SLIDERS.kContrast:
        m_contrasteSlider.fillRect.GetComponent<Image>().color = _color;
        break;
      case SLIDERS.kSaturation:
        m_saturacionSlider.fillRect.GetComponent<Image>().color = _color;
        break;
      case SLIDERS.kFocalLenght:
        m_focalLenghtSlider.fillRect.GetComponent<Image>().color = _color;
        break;
      default:
        break;
    }
  }

  public float GetSliderValue(SLIDERS _slider)
  {
    switch (_slider)
    {
      case SLIDERS.kContrast:
        return m_contrasteSlider.value;
        break;
      case SLIDERS.kSaturation:
        return m_saturacionSlider.value;
        break;
      case SLIDERS.kFocalLenght:
        return m_focalLenghtSlider.value;
        break;
      default:
        break;
    }
    return 0.0f;
  }

  public void SetSliderValue(SLIDERS _slider, float _value)
  {
    switch (_slider)
    {
      case SLIDERS.kContrast:
        m_contrasteSlider.value = _value;
        break;
      case SLIDERS.kSaturation:
        m_saturacionSlider.value = _value; ;
        break;
      case SLIDERS.kFocalLenght:
        m_focalLenghtSlider.value = _value; ;
        break;
      default:
        break;
    }
  }
}
