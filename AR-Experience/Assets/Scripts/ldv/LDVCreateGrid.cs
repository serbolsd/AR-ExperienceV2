using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDVCreateGrid : MonoBehaviour
{
  // Positions of grid
  // Se manejara de esta forma grid[Y][0][X];
  public Vector3[][][] grid;
  // Se manejara de esta forma grid[Y][0][X];
  public LDVGridTile[][][] gridTiles;
  // number of rows (ancho)
  public int rows;
  // number of columns (largo)
  public int columns;
  // number of levels (alto)
  public int levels;
  // a cube for draw the grid
  public GameObject prefab;
  // a cube for move
  public GameObject objectTestMove;
  // the tile size
  public float tileSize = 0;
  // the tile size multiply by the columns
  public float ancho;
  //the first position to init
  public float init_ancho;
  // the tile size multiply by the rows
  public float largo;
  //the first position to init
  public float init_largo;
  // to draw grid
  public bool DrawGrid = false;

  public Vector2Int initTile;
  public Vector2Int finishTile;
  public Vector3 winTile;
  public float starPos;
  public float limitDown=0.0f;
  public Vector3 playerPos;

  public bool activeFireThrower = true;
  public Transform worldTransform;
  public GameObject fireThrower;
  //float distance;

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }

  // Start is called before the first frame update
  public void onStart()
  {
    grid = new Vector3[levels][][];
    gridTiles = new LDVGridTile[levels][][];
    for (int i = 0; i < levels; i++)
    {
      grid[i] = new Vector3[rows][];
      gridTiles[i] = new LDVGridTile[rows][];

    }
    for (int i = 0; i < levels; i++)
    {
      for (int j = 0; j < rows; j++)
      {
        grid[i][j] = new Vector3[columns];
        gridTiles[i][j] = new LDVGridTile[columns];
      }
    }
    ancho = tileSize * columns;
    starPos = init_ancho = -ancho / 2;
    ancho /= 2;
    largo = tileSize * rows;
    init_largo = -largo / 2;
    largo /= 2;
    int indexColumn = 0;
    int indexRow = 0;
    float poslevel = 0;
    float currentlevelpos = tileSize * levels;
    float diferenceLevel = currentlevelpos / levels;
    limitDown = currentlevelpos = -(currentlevelpos/2);
    float nextLevel = 1 * tileSize;
    for (int level = 0; level < levels; level++)
    {
      for (float i = init_largo; i < largo; i += tileSize)
      {
        for (float j = init_ancho; j < ancho; j += tileSize)
        {
          grid[level][indexRow][indexColumn] = new Vector3(j, currentlevelpos, i)+transform.position;
          
          GameObject newg = Instantiate(prefab, new Vector3(grid[level][indexRow][indexColumn].x, currentlevelpos, grid[level][indexRow][indexColumn].z), Quaternion.identity);
          newg.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
          newg.transform.SetParent(this.gameObject.transform);
          newg.GetComponent<LDVGridTile>().m_indexX = level;
          newg.GetComponent<LDVGridTile>().m_indexY = indexColumn;
          newg.GetComponent<LDVGridTile>().m_position = grid[level][indexRow][indexColumn];
          gridTiles[level][indexRow][indexColumn] = newg.GetComponent<LDVGridTile>();

          indexColumn++;
        }
        indexRow++;
        indexColumn = 0;
      }
      poslevel += nextLevel;
      currentlevelpos += diferenceLevel;
      indexRow = 0;
      indexColumn = 0;
    }
   //GameObject obj = Instantiate(objectTestMove, grid[0][0][0], Quaternion.identity);
   //gridTiles[0][0][0].m_hasObject = true;
   //gridTiles[0][0][0].myObject = obj;
   //
   //obj = Instantiate(objectTestMove, grid[1][0][0], Quaternion.identity);
   //gridTiles[1][0][0].m_hasObject = true;
   //gridTiles[1][0][0].myObject = obj;

    int ran = Random.Range(0,levels-1);
    initTile = new Vector2Int(ran, 0);
    if (ran < levels - 1)
    {
      gridTiles[ran][0][0].m_hasObject = true;
      gridTiles[ran][0][0].m_movibleObject = false;
    }
    playerPos = gridTiles[ran + 1][0][0].m_position;
    ran = Random.Range(0, levels - 1);
    finishTile = new Vector2Int(ran, columns - 1);
    if (ran < levels - 1)
    {
      gridTiles[ran][0][columns - 1].m_hasObject = true;
      gridTiles[ran][0][columns - 1].m_movibleObject = false;
    }

    winTile = grid[ran][0][columns - 1];
    winTile.y += diferenceLevel;
    if (activeFireThrower)
    {
      for (int i = 4; i < columns - 4; i+=2)
      {
        ran = Random.Range(0, 100);
        if (ran < 50)
        {
          ran = Random.Range(0, levels - 1);
          gridTiles[ran][0][i].m_hasObject = true;
          gridTiles[ran][0][i].m_movibleObject = false;
          GameObject newObj = null;
          newObj = Instantiate(fireThrower, grid[ran][0][i], Quaternion.identity, worldTransform);
          gridTiles[ran][0][i].myObject = newObj;
        }
      }
    }
  }

  public void deleteAllObjects()
  {
    int indexColumn = 0;
    int indexRow = 0;
    float poslevel = 0;
    float currentlevelpos = tileSize * levels;
    float diferenceLevel = currentlevelpos / levels;
    limitDown = currentlevelpos = -(currentlevelpos / 2);
    float nextLevel = 1 * tileSize;
    for (int level = 0; level < levels; level++)
    {
      for (float i = init_largo; i < largo; i += tileSize)
      {
        for (float j = init_ancho; j < ancho; j += tileSize)
        {
          if (gridTiles[level][indexRow][indexColumn].m_hasObject && 
              gridTiles[level][indexRow][indexColumn].m_movibleObject)
          {
            Destroy(gridTiles[level][indexRow][indexColumn].myObject);
            gridTiles[level][indexRow][indexColumn].myObject = null;
            gridTiles[level][indexRow][indexColumn].m_hasObject = false;
          }

          indexColumn++;
        }
        indexRow++;
        indexColumn = 0;
      }
      poslevel += nextLevel;
      currentlevelpos += diferenceLevel;
      indexRow = 0;
      indexColumn = 0;
    }
  }
}
