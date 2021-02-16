using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDVjumpTrigger : MonoBehaviour
{

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      other.GetComponent<LDVplayer>().Jump();
    }
  }
}
