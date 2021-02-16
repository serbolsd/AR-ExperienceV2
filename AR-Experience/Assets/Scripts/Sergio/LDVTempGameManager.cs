using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LDVTempGameManager : MonoBehaviour
{
  LDVCreateGrid grid = null;
  LDVMoveObjectInGrid moveObjetcs;
  LDVplayer player;
  LDVwinTrigger winTrigger;
  public GameObject btnCube;
  public GameObject btnRampUp;
  public GameObject btnRampDown;
  public GameObject btnJump;
  public GameObject btnErase;
  public GameObject btnMove;
  public GameObject btnPlayReset;
  public GameObject btnDeleteAll;
  public GameObject m_gameOverScreen;
  GameObject currentBtn;
  public Sprite spritePlay;
  public Sprite spriteStop;
  public bool mpuseOnSomething=false;

  public GameObject infoObject;

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }
  // Start is called before the first frame update
  void Start()
  {
    btnCube.transform.GetChild(0).gameObject.SetActive(false);
    btnRampUp.transform.GetChild(0).gameObject.SetActive(false);
    btnRampDown.transform.GetChild(0).gameObject.SetActive(false);
    btnJump.transform.GetChild(0).gameObject.SetActive(false);
    btnErase.transform.GetChild(0).gameObject.SetActive(false);
    currentBtn = btnMove;

    grid = FindObjectOfType<LDVCreateGrid>();
    moveObjetcs = FindObjectOfType<LDVMoveObjectInGrid>();
    player = FindObjectOfType<LDVplayer>();
    winTrigger = FindObjectOfType<LDVwinTrigger>();
    if (null != grid)
    {
      grid.onStart();
    }
    if (null != moveObjetcs)
    {
      moveObjetcs.onStart();
      moveObjetcs.addStartObject(grid.initTile.x, grid.initTile.y);
      moveObjetcs.addFinishObjet(grid.finishTile.x, grid.finishTile.y);
      
    }
    if (null != winTrigger)
    {
      winTrigger.transform.position = grid.winTile;
    }
    if (null != player)
    {
      player.onStart();
      player.transform.position = grid.playerPos; 
      player.m_startPosition = grid.playerPos;
      player.m_limitDown = grid.limitDown;
    }

    if (!PlayerPrefs.HasKey("FirtsLevel"))
    {
      infoObject.SetActive(true);
      PlayerPrefs.SetInt("FirtsLevel", 1);
    }
  }

  private void OnDestroy()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (moveObjetcs)
    {
      moveObjetcs.onUpdate();
    }
    if (null != player)
    {
      player.onUpdate();
    }
  }

  public void setObjetcToAdd(int type)
  {
    currentBtn.transform.GetChild(0).gameObject.SetActive(false);
    switch (type)
    {
      case 0: //cube
        btnCube.transform.GetChild(0).gameObject.SetActive(true);
        currentBtn = btnCube;
        break;
      case 1: //rampUp
        btnRampUp.transform.GetChild(0).gameObject.SetActive(true);
        currentBtn = btnRampUp;
        break;
      case 2: //rampDown
        btnRampDown.transform.GetChild(0).gameObject.SetActive(true);
        currentBtn = btnRampDown;
        break;
      case 3: //jumpTrigger
        btnJump.transform.GetChild(0).gameObject.SetActive(true);
        currentBtn = btnJump;
        break;
      default:
        break;
    }
    moveObjetcs.setObjectToAdd(type);
  }

  public void DeleteObjetc()
  {
    currentBtn.transform.GetChild(0).gameObject.SetActive(false);
    btnErase.transform.GetChild(0).gameObject.SetActive(true);
    currentBtn = btnErase;
    moveObjetcs.prepareToDelete();
  }

  public void moveObject()
  {
    currentBtn.transform.GetChild(0).gameObject.SetActive(false);
    btnMove.transform.GetChild(0).gameObject.SetActive(true);
    currentBtn = btnMove;
    moveObjetcs.cancelAction();
  }

  public void playStop()
  {
    AudioManager.playSound(Sounds.button, 1.0f);
    if (!player.m_active)
    {
      player.active();
      btnPlayReset.GetComponent<Button>().image.sprite = spriteStop;
    }
    else
    {
      player.reset();
      btnPlayReset.GetComponent<Button>().image.sprite = spritePlay;
    }
  }
  public void changePlayStop()
  {
    if (!player.m_active)
    {
      btnPlayReset.GetComponent<Button>().image.sprite = spriteStop;
    }
    else
    {
      btnPlayReset.GetComponent<Button>().image.sprite = spritePlay;
    }
  }
  public void mouseOnButton()
  {
    mpuseOnSomething = true;
  }
  public void mouseExitButton()
  {
    mpuseOnSomething = false;
  }

  public void deleteAll()
  {
    grid.deleteAllObjects();
  }

  public void seguirEditando()
  {
    //other.GetComponent<LDVplayer>().m_active = false;
    m_gameOverScreen.SetActive(false);
    mpuseOnSomething = false;
    btnPlayReset.GetComponent<Button>().image.sprite = spritePlay;
    player.reset();
  }
}
