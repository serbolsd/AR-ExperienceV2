using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LPATargetManager : MonoBehaviour
{
  public Text m_photoCounterText;
  public GameObject m_currTarget;
  public GameObject[] m_prefabTarget;
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
    int prefabIndex = 0;
    float XPos = -5.0f;
    float XPosIncrement = 10.0f/ m_numberOfTargets;
    float XAngIncrement = 150.0f/ m_numberOfTargets;
    for (int i = 0; i < m_numberOfTargets; i++)
    {
      if (prefabIndex > m_prefabTarget.Length-1)
      {
        prefabIndex = 0;
      }
      Vector3 center = Camera.main.transform.position;
      float ang = 15 + (i * XAngIncrement);
      Vector3 pos = RandomCircle(center, Random.Range(4.0f, 6.0f), ang);
      Vector3 rot = Quaternion.LookRotation(Camera.main.transform.position - pos, Vector3.up).eulerAngles;
      rot.x = 0.0f;
      rot.z = 0.0f;
      m_targets[i] = Instantiate(m_prefabTarget[prefabIndex], pos, Quaternion.Euler(rot), m_wroldTransform);
      prefabIndex++;

      

      //m_targets[i] = Instantiate(m_prefabTarget[prefabIndex], m_wroldTransform);
      /*
      do
      {
        m_targets[i].transform.position = new Vector3(XPos,
                                                    -1.8f,
                                                    Random.Range(2.0f, 8.0f));

        
        Vector3 rot = Quaternion.LookRotation(Camera.main.transform.position - m_targets[i].transform.position, Vector3.up).eulerAngles;
        rot.x = 0.0f;
        rot.z = 0.0f;

        m_targets[i].transform.eulerAngles = rot;
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
      XPos += XPosIncrement;
      */


    }

    //scramble order
    for (int i = m_targets.Length - 1; i > 0; i--)
    {
      int randomIndex = Random.Range(0, m_targets.Length);

      GameObject temp = m_targets[i];
      m_targets[i] = m_targets[randomIndex];
      m_targets[randomIndex] = temp;
    }

    ChangeTarget();
  }

  // Update is called once per frame
  void Update()
  {
    m_photoCounterText.text = m_currTargetIndex + "/" + m_numberOfTargets;

    m_timeCounter += Time.deltaTime * m_speed;
    m_targetIcon.transform.position = m_currTarget.transform.position + (Vector3.up * m_heightOffset) + (Vector3.up * Mathf.Sin(m_timeCounter)*0.1f);
  }

  Vector3 RandomCircle(Vector3 center, float radius, float ang)
  {
    Vector3 pos;
    pos.x = center.x + radius * Mathf.Sin((ang-90) * Mathf.Deg2Rad);
    pos.y = -1.8f;
    pos.z = center.z + radius * Mathf.Cos((ang-90) * Mathf.Deg2Rad);
    return pos;
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
      m_currTargetIndex++;
      Debug.Log("gameOver");
      return false;
    }
  }
}
