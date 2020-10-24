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

  public List<LineRenderer> m_linesBuffer;

  [Header("Brush Size Buttons")]
  public Button m_button1;
  public Button m_button2;
  public Button m_button3;
  public Button m_button4;
  public Button m_button5;

  public Transform m_world;

  float m_currWidth = 1.0f;

  // Start is called before the first frame update
  public void onStart()
  {
    m_button1.onClick.AddListener(delegate { onSelecetWidth(0.2f); });
    m_button2.onClick.AddListener(delegate { onSelecetWidth(0.4f); });
    m_button3.onClick.AddListener(delegate { onSelecetWidth(0.6f); });
    m_button4.onClick.AddListener(delegate { onSelecetWidth(0.8f); });
    m_button5.onClick.AddListener(delegate { onSelecetWidth(1.0f); });
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
        if (Vector3.Distance(hit.point, m_fingerPositions[m_fingerPositions.Count - 1]) > 0.01f)
        {
          UpdateLine(hit.point);
        }
      }
        
    }

    if (Input.GetMouseButtonUp(0))
    {
      FindObjectOfType<ColorPickerTester>().renderer = null;
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
      m_linesBuffer.Add(m_lineRenderer);
      m_lineRenderer.sortingOrder = m_linesBuffer.Count;
      //m_lineRenderer.material = m_currentMaterial;
      m_fingerPositions.Clear();
      m_fingerPositions.Add(hit.point - (Vector3.forward * m_linesBuffer.Count * 0.005f));
      m_fingerPositions.Add(hit.point - (Vector3.forward * m_linesBuffer.Count * 0.005f));
      m_lineRenderer.SetPosition(0, m_fingerPositions[0]);
      m_lineRenderer.SetPosition(1, m_fingerPositions[1]);
    }
  }

  void UpdateLine(Vector3 newFingerPos)
  {
    if (m_linesBuffer.Count > 0)
    {
      newFingerPos -= Vector3.forward * m_linesBuffer.Count * 0.005f;
      m_fingerPositions.Add(newFingerPos);
      m_lineRenderer.positionCount++;
      m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, newFingerPos);
    }
    
  }

  void onSelecetWidth(float width)
  {
    AudioManager.playSound(Sounds.click, 1.0f);
    m_currWidth = width;
    FindObjectOfType<LAManager>().okBrush();
  }
  public void redoButton()
  {
    AudioManager.playSound(Sounds.hit, 0.5f);
    if (m_linesBuffer.Count > 0)
    {
      LineRenderer tempLine = m_linesBuffer[m_linesBuffer.Count - 1];
      m_linesBuffer.RemoveAt(m_linesBuffer.Count - 1);
      Destroy(tempLine);
    }
  }
}
