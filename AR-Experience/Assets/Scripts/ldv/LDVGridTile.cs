using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDVGridTile : MonoBehaviour
{
  public int m_indexX = 0;
  public int m_indexY = 0;

  public Vector3 m_position = new Vector3(0,0,0);
  public bool m_hasObject = false;
  public bool m_movibleObject = true;
  public GameObject myObject = null; 
}
