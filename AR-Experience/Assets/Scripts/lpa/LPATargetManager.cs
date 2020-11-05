using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPATargetManager : MonoBehaviour
{
  public GameObject m_currTarget;
  public GameObject m_prefabTarget;
  public GameObject[] m_targets;
  public GameObject m_targetIcon;
  public LPAChangePropertys m_lPAChangePropertys;
  public float m_heightOffset = 1.5f;
  int m_currTargetIndex = -1;

  public Transform m_wroldTransform;

  float m_timeCounter = 0.0f;
  public float m_speed = 1.0f;

  public int m_numberOfTargets = 3;

  // Start is called before the first frame update
  void Start()
  {
    m_targets = new GameObject[m_numberOfTargets];
    bool can = true;
    for (int i = 0; i < m_numberOfTargets; i++)
    {
      m_targets[i] = Instantiate(m_prefabTarget, m_wroldTransform);
      do
      {
        m_targets[i].transform.position = new Vector3(Random.Range(-5.0f, 5.0f),
                                                    -1.35f,
                                                    Random.Range(-1.0f, 4.0f));
        can = true;
        for (int j = 0; j < i; j++)
        {
          Vector3 dist = m_targets[i].transform.position - m_targets[j].transform.position;
          if (dist.magnitude <= 2.5)
          {
            can = false;
            break;
          }
        }
      } while (!can);
      
    }
    ChangeTarget();
  }

  // Update is called once per frame
  void Update()
  {
    m_timeCounter += Time.deltaTime * m_speed;
    m_targetIcon.transform.position = m_currTarget.transform.position + (Vector3.up * m_heightOffset) + (Vector3.up * Mathf.Sin(m_timeCounter)*0.1f);
  }

  public bool ChangeTarget()
  {
    if (m_currTargetIndex < m_targets.Length-1)
    {
      m_currTargetIndex++;
      m_currTarget = m_targets[m_currTargetIndex];
      return true;
    }
    else
    {
      Debug.Log("gameOver");
      return false;
    }
  }
}
