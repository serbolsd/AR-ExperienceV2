using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDVFireThrower : MonoBehaviour
{
  LDVTempGameManager m_manager;
  // Start is called before the first frame update
  void Start()
  {
    m_manager = FindObjectOfType<LDVTempGameManager>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      AudioManager.playSound(Sounds.explosion);
      m_manager.playStop();
    }
  }
}
