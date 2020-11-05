using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LPAManager : MonoBehaviour
{
  public void again()
  {
    SceneManager.LoadScene("LpaGame");
  }

  public void exit()
  {
    SceneManager.LoadScene("MainMenu");
  }
}
