using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HSVPicker.Examples;

public class LADrawLine : MonoBehaviour
{
  public GameObject m_linePrefab;
  public GameObject m_currentLine;

  Material m_currentMaterial;

  public LineRenderer m_lineRenderer;
  public List<Vector3> m_fingerPositions;

  public List<List<LineRenderer>> m_linesBuffer;
  public List<List<Color>> m_linesColorsBuffer;
  int m_currentFrame = 0;

  [Header("Brush Size Buttons")]
  public Button[] m_Brushbuttons;

  public Transform m_world;

  float m_currWidth = 1.0f;
  int m_selectedBrush = 4;

  [Header("Animation settings")]
  public float m_frameDuration = 0.25f;
  float m_frameTimeCounter = 0.0f;
  bool m_isPlaying = false;

  // Start is called before the first frame update
  public void onStart()
  {
    m_Brushbuttons[0].onClick.AddListener(delegate { onSelecetWidth(0.2f, 0); });
    m_Brushbuttons[1].onClick.AddListener(delegate { onSelecetWidth(0.4f, 1); });
    m_Brushbuttons[2].onClick.AddListener(delegate { onSelecetWidth(0.6f, 2); });
    m_Brushbuttons[3].onClick.AddListener(delegate { onSelecetWidth(0.8f, 3); });
    m_Brushbuttons[4].onClick.AddListener(delegate { onSelecetWidth(1.0f, 4); });

    m_linesBuffer = new List<List<LineRenderer>>();
    m_linesBuffer.Add(new List<LineRenderer>());

    m_linesColorsBuffer = new List<List<Color>>();
    m_linesColorsBuffer.Add(new List<Color>());
    

    UpdateUIBrushColor(Color.white);
  }

  // Update is called once per frame
  public void onUpdate()
  {
    if (Input.GetMouseButtonDown(0))
    {
      CreateLine();
    }
    if (Input.GetMouseButton(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 1000.0f))
      {
        if (m_fingerPositions.Count > 0)
        {
          if (Vector3.Distance(hit.point, m_fingerPositions[m_fingerPositions.Count - 1]) > 0.01f)
          {
            UpdateLine(hit.point);
          }
        }
      }
        
    }

    if (Input.GetMouseButtonUp(0))
    {
      FindObjectOfType<ColorPickerTester>().renderer = null;
    }


    if (m_isPlaying)
    {
      m_frameTimeCounter += Time.deltaTime;
      if (m_frameTimeCounter >= m_frameDuration)
      {
        m_frameTimeCounter = 0;

        if (m_currentFrame < m_linesBuffer.Count - 1)
        {
          m_currentFrame++;

          HideLines(m_currentFrame - 1); //se oculta la anterior

          ShowLines(m_currentFrame); //se muestra y colorea la actual
          ColoredLines(m_currentFrame);

        }
      }

      if (m_currentFrame == m_linesBuffer.Count-1)
      {
        m_isPlaying = false;
        m_frameTimeCounter = 0;
      }
    }
  }

  void CreateLine()
  {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit, 1000.0f))
    {
      m_currentLine = Instantiate(m_linePrefab, Vector3.zero, Quaternion.identity, m_world);
      FindObjectOfType<ColorPickerTester>().renderer = m_currentLine.GetComponent<LineRenderer>();
      FindObjectOfType<ColorPickerTester>().onStart();
      m_lineRenderer = m_currentLine.GetComponent<LineRenderer>();
      m_lineRenderer.widthMultiplier = m_currWidth;
      m_linesBuffer[m_currentFrame].Add(m_lineRenderer);
      m_linesColorsBuffer[m_currentFrame].Add(m_lineRenderer.materials[0].color);
      m_lineRenderer.sortingOrder = m_linesBuffer[m_currentFrame].Count;
      //m_lineRenderer.material = m_currentMaterial;
      m_fingerPositions.Clear();
      m_fingerPositions.Add(hit.point - (Vector3.forward * m_linesBuffer[m_currentFrame].Count * 0.005f * (m_currentFrame+1)));
      m_fingerPositions.Add(hit.point - (Vector3.forward * m_linesBuffer[m_currentFrame].Count * 0.005f * (m_currentFrame + 1)));
      m_lineRenderer.SetPosition(0, m_fingerPositions[0]);
      m_lineRenderer.SetPosition(1, m_fingerPositions[1]);
    }
  }

  void UpdateLine(Vector3 newFingerPos)
  {
    if (m_linesBuffer[m_currentFrame].Count > 0)
    {
      newFingerPos -= Vector3.forward * m_linesBuffer[m_currentFrame].Count * 0.005f * (m_currentFrame + 1);
      m_fingerPositions.Add(newFingerPos);
      m_lineRenderer.positionCount++;
      m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, newFingerPos);
    }
    
  }

  void onSelecetWidth(float width, int selectedBrushIndex)
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    m_currWidth = width;
    int previousSelected = m_selectedBrush;
    m_selectedBrush = selectedBrushIndex;
    UpdateUIBrushColor(m_Brushbuttons[previousSelected].GetComponent<Image>().color);
    FindObjectOfType<LAManager>().okBrush();
  }
  public void redoButton()
  {
    AudioManager.playSound(Sounds.hit, 0.5f);
    if (m_linesBuffer[m_currentFrame].Count > 0)
    {
      LineRenderer tempLine = m_linesBuffer[m_currentFrame][m_linesBuffer[m_currentFrame].Count - 1];
      m_linesBuffer[m_currentFrame].RemoveAt(m_linesBuffer[m_currentFrame].Count - 1);
      m_linesColorsBuffer[m_currentFrame].RemoveAt(m_linesColorsBuffer[m_currentFrame].Count - 1);
      Destroy(tempLine);
    }
  }

  public void onNewFrameClick()
  {
    m_currentFrame++;

    if (m_currentFrame-2 >= 0) //se oculta la anterior de l anterior
    {
      HideLines(m_currentFrame-2);
    }

    TransparentLines(m_currentFrame-1); //se transparenta la anterior

    m_linesBuffer.Add(new List<LineRenderer>());
    m_linesColorsBuffer.Add(new List<Color>());
  }

  public void onDeleteFrameClick()
  {
    if (m_currentFrame == 0)
    {
      return;
    }

    foreach (LineRenderer line in m_linesBuffer[m_currentFrame])
    {
      Destroy(line.gameObject);
    }
    m_linesBuffer.RemoveAt(m_currentFrame);
    m_linesColorsBuffer.RemoveAt(m_currentFrame);
    if (m_currentFrame > 0)
    {
      m_currentFrame--;
      ShowLines(m_currentFrame);
      ColoredLines(m_currentFrame);

      ShowLines(m_currentFrame-1);
      TransparentLines(m_currentFrame - 1);
    }
  }

  public void onPreviousFrameClick()
  {
    if (m_currentFrame > 0)
    {
      m_currentFrame--;

      HideLines(m_currentFrame+1); //se oculta la siguiente

      ShowLines(m_currentFrame); //se muestra y colorea la actual
      ColoredLines(m_currentFrame);

      if (m_currentFrame - 1 >= 0) //se transparenta la anterior
      {
        ShowLines(m_currentFrame-1);
        TransparentLines(m_currentFrame-1);
      }
    }
  }

  public void onNextFrameClick()
  {
    if (m_currentFrame < m_linesBuffer.Count-1)
    {
      m_currentFrame++;

      if (m_currentFrame - 2 >= 0) //se oculta la anterior de l anterior
      {
        HideLines(m_currentFrame - 2);
      }

      TransparentLines(m_currentFrame-1); //se transparenta la anterior

      ShowLines(m_currentFrame); //se muestra y colorea la actual
      ColoredLines(m_currentFrame);

    }
  }

  public void onPlayAnimClick()
  {
    m_isPlaying = true;

    HideLines(m_currentFrame - 1); //se oculta la anterior
    HideLines(m_currentFrame);
    m_currentFrame = 0;
    ShowLines(m_currentFrame);
    ColoredLines(m_currentFrame);

  }

  void HideLines(int frame)
  {
    foreach (LineRenderer line in m_linesBuffer[frame])
    {
      line.enabled = false;
    }
  }

  void ShowLines(int frame)
  {
    foreach (LineRenderer line in m_linesBuffer[frame])
    {
      line.enabled = true;
    }
  }

  void TransparentLines(int frame)
  {
    for (int i = 0; i < m_linesBuffer[frame].Count; i++)
    {
      m_linesBuffer[frame][i].materials[0].color = m_linesColorsBuffer[frame][i] * 0.15f;
    }
  }

  void ColoredLines(int frame)
  {
    for (int i = 0; i < m_linesBuffer[frame].Count; i++)
    {
      m_linesBuffer[frame][i].materials[0].color = m_linesColorsBuffer[frame][i];
    }
  }

  public void UpdateUIBrushColor(Color _color)
  {
    foreach (Button button in m_Brushbuttons)
    {
      Color tempColor = _color;
      tempColor.a = 0.3f;
      button.GetComponent<Image>().color = tempColor;
    }
    m_Brushbuttons[m_selectedBrush].GetComponent<Image>().color = _color;
  }

}
