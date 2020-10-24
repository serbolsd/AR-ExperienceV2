using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
  public GameObject menuScreen;
  public GameObject creditsScreen;

  /**
   * game is the game to load
   * 0 = idv
   * 1 = la
   * 2 = lda
   * 3 = ldv
   * 4 = lpa
   */
  public static void selecedGame(int game)
  {
    switch (game)
    {
      case 0:
        SceneManager.LoadScene("idvGame");// scene alredy exist
        break;
      case 1:
        SceneManager.LoadScene("LaGame");// scene alredy exist
        break;
      case 2:
        SceneManager.LoadScene("LDAGAME");// scene not exist yet and the name could change
        break;
      case 3:
        SceneManager.LoadScene("LDVGAME");// scene alredy exist
        break;
      case 4:
        SceneManager.LoadScene("LPAGAME"); // scene never go to exist 
        break;
      default:
        break;
    }
  }

  public void showCreditScreen()
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    menuScreen.SetActive(false);
    creditsScreen.SetActive(true);
  }

  public void showMenuScreen()
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    creditsScreen.SetActive(false);
    menuScreen.SetActive(true);
  }

}
