using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDVMoveObjectInGrid : MonoBehaviour
{
  public GameObject m_worldRoot;

  GameObject ObjectMoving=null;
  GameObject currentTile=null;
  
  public GameObject cube;
  public GameObject rampUp;
  public GameObject rampDown;
  public GameObject JumpTrigger;

  public Material selectingTileMaterial;
  public Material normalTileMaterial;

  bool addingObject = false;
  bool deleting = false;
  GameObject objetToAdd;
  int typeObjectToAdd;

  LDVCreateGrid mapGrid;
  LDVTempGameManager man;

  public void onStart()
  {
    mapGrid = FindObjectOfType<LDVCreateGrid>();
    man = FindObjectOfType<LDVTempGameManager>();
  }

  public void onUpdate()
  {
    if (man.mpuseOnSomething)
    {
      return;
    }
    if (Input.GetMouseButton(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 1000.0f))
      {
        //suppose i have two objects here named obj1 and obj2.. how do i select obj1 to be transformed 
        if (hit.transform != null)
        {
          if("GridTile" == hit.transform.gameObject.tag)
          {
            GameObject newt = hit.transform.gameObject;
            if (addingObject)
            {
              addObjectToGrid(ref newt);
              return;
            }
            if (deleting)
            {
              deleteObject(ref newt);
              return;
            }
            if (hit.transform.gameObject != currentTile)
            {
              if (newt.GetComponent<LDVGridTile>().m_hasObject && 
                null == ObjectMoving && 
                newt.GetComponent<LDVGridTile>().m_movibleObject)
              {
                ObjectMoving = newt.GetComponent<LDVGridTile>().myObject;
                if (null == currentTile)
                {
                  newt.GetComponent<LDVGridTile>().m_hasObject = false;
                  newt.GetComponent<LDVGridTile>().myObject = null;
                }
              }
              //ChangeTile(ref currentTile, ref newt);
              if (null != ObjectMoving && !newt.GetComponent<LDVGridTile>().m_hasObject)
              {
                ObjectMoving.transform.position = newt.GetComponent<LDVGridTile>().m_position;
                if (null != currentTile)
                {
                  currentTile.GetComponent<LDVGridTile>().m_hasObject = false;
                  currentTile.GetComponent<LDVGridTile>().myObject = null;
                }
                newt.GetComponent<LDVGridTile>().m_hasObject = true;
                newt.GetComponent<LDVGridTile>().myObject = ObjectMoving;
                currentTile = newt;
              }
            }
          }
          else
          {
            currentTile = null;
          }
        }
      }
    }

    if (Input.GetMouseButtonUp(0))
    {
      if (null != currentTile)
      {
        currentTile.GetComponent<MeshRenderer>().material = normalTileMaterial;
        if (null != ObjectMoving && !currentTile.GetComponent<LDVGridTile>().m_hasObject)
        {
          currentTile.GetComponent<LDVGridTile>().m_hasObject = true;
          ObjectMoving.transform.position = currentTile.GetComponent<LDVGridTile>().m_position;
        }
      }
      currentTile = null;
      ObjectMoving = null;
    }
  }

  void ChangeTile(ref GameObject currentOTile, ref GameObject newTile)
  {
    if (null != currentOTile)
    {
      currentOTile.GetComponent<MeshRenderer>().material = normalTileMaterial;
    }
    newTile.GetComponent<MeshRenderer>().material = selectingTileMaterial;
  }

  void addObjectToGrid(ref GameObject Tile)
  {
    if (null==Tile||Tile.GetComponent<LDVGridTile>().m_hasObject)
    {
      return;
    }
    AudioManager.playSound(Sounds.place, 1.0f);
    GameObject newObj =null;
    int x = Tile.GetComponent<LDVGridTile>().m_indexX;
    int y = Tile.GetComponent<LDVGridTile>().m_indexY;
    switch (typeObjectToAdd)
    {
      case 0: //cube
        newObj = Instantiate(cube, mapGrid.grid[x][0][y], Quaternion.identity, m_worldRoot.transform);
        break;
      case 1: //rampUp
        newObj = Instantiate(rampUp, mapGrid.grid[x][0][y], Quaternion.identity, m_worldRoot.transform);
        break;
      case 2: //rampDown
        newObj = Instantiate(rampDown, mapGrid.grid[x][0][y], Quaternion.identity, m_worldRoot.transform);
        break;
      case 3: //jumpTrigger
        newObj = Instantiate(JumpTrigger, mapGrid.grid[x][0][y], Quaternion.identity, m_worldRoot.transform);
        break;
      default:
        break;
    }
    Tile.GetComponent<LDVGridTile>().m_hasObject = true;
    Tile.GetComponent<LDVGridTile>().myObject = newObj;
    //addingObject = false;

  }

  public void setObjectToAdd(int type)
  {
    addingObject = true;
    deleting = false;
    typeObjectToAdd = type;

  }

  void deleteObject(ref GameObject Tile)
  {
    if (!Tile.GetComponent<LDVGridTile>().m_hasObject || !Tile.GetComponent<LDVGridTile>().m_movibleObject)
    {
      return;
    }
    AudioManager.playSound(Sounds.remove, 1.0f);
    Tile.GetComponent<LDVGridTile>().m_hasObject = false;
    GameObject deleteObj = Tile.GetComponent<LDVGridTile>().myObject;
    Tile.GetComponent<LDVGridTile>().myObject = null;
    Destroy(deleteObj);
  }

  public void prepareToDelete()
  {
    addingObject = false;
    deleting = true;
  }

  public void cancelAction()
  {
    addingObject = false;
    deleting = false;
  }

  public void addStartObject(int x, int y)
  {
    GameObject newObj = null;
    newObj = Instantiate(cube, mapGrid.grid[x][0][y], Quaternion.identity, m_worldRoot.transform);
    mapGrid.gridTiles[x][0][y].m_hasObject = true;
    mapGrid.gridTiles[x][0][y].m_movibleObject = false;
    mapGrid.gridTiles[x][0][y].myObject = newObj;
  }

  public void addFinishObjet(int x, int y)
  {
    GameObject newObj = null;
    newObj = Instantiate(cube, mapGrid.grid[x][0][y], Quaternion.identity, m_worldRoot.transform);
    mapGrid.gridTiles[x][0][y].m_hasObject = true;
    mapGrid.gridTiles[x][0][y].m_movibleObject = false;
    mapGrid.gridTiles[x][0][y].myObject = newObj;
  }
}
