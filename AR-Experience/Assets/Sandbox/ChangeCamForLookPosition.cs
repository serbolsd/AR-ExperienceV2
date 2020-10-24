using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamForLookPosition : MonoBehaviour
{
  // Start is called before the first frame update
  public LookAtTarget lookTarget = null;
  public Transform transformRenderCam;
  public Transform transformVIOCam;
  public Transform transformMainCam;
  public int indexCam = 0;
  public Text debugCamText;
  void Start()
  {
    lookTarget = FindObjectOfType<LookAtTarget>();
    if (null != lookTarget)
    {
      lookTarget.m_target = transformRenderCam;
      debugCamText.text = "Active Cam: RenderCam";
      indexCam = 0;
    }
  }

  public void changeCamera()
  {
    debugCamText.text = "Active Cam: ";
    if (null != lookTarget)
    {
      ++indexCam;
      if (3<=indexCam)
      {
        indexCam = 0;
      }
      switch (indexCam)
      {
        case 0:
          lookTarget.m_target = transformRenderCam;
          debugCamText.text += "RenderCam";
          break;
        case 1:
          lookTarget.m_target = transformVIOCam;
          debugCamText.text += "VIO";
          break;
        case 2:
          lookTarget.m_target = transformMainCam;
          debugCamText.text += "MainCam";
          break;
        default:
          break;
      }
    }
  }
}
