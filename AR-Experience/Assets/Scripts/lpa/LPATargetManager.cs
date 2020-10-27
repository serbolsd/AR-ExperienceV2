using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPATargetManager : MonoBehaviour
{
  public GameObject[] m_targets;
  public GameObject m_targetIcon;
  public LPAChangePropertys m_lPAChangePropertys;
  public float m_heightOffset = 1.5f;

  float m_timeCounter = 0.0f;
  public float m_speed = 1.0f;

  // Start is called before the first frame update
  void onStart()
  {

  }

  // Update is called once per frame
  void Update()
  {
    m_timeCounter += Time.deltaTime * m_speed;
    m_targetIcon.transform.position = m_targets[m_lPAChangePropertys.getTarget()].transform.position + (Vector3.up * m_heightOffset) + (Vector3.up * Mathf.Sin(m_timeCounter)*0.1f);
  }
}
