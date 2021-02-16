using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDVBuildType : MonoBehaviour
{
  public int m_id=-1;

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }
}
