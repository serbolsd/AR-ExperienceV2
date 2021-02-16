using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDVFollowInCanvas : MonoBehaviour
{
  public RectTransform canvas;
  public GameObject objetToFollow;
  public Transform camTransform;
  public Vector3 canvasPos = new Vector3(0, 0, 0);
  public Vector3 dir;
  public Vector3 forward;
  public Vector3 realdir;
  public Vector3 poscam;
  public Vector3 posEnemi;
  public float width;
  public float height;
  public float angle;

  private void Awake()
  {
    #if UNITY_EDITOR
      Debug.unityLogger.logEnabled = true;
    #else
      Debug.unityLogger.logEnabled = false;
    #endif
  }

  //private void Update()
  //{
  //  onUpdate();
  //}
  // Update is called once per frame
  public void onUpdate()
  {

    //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z);
    //Debug.Log("screenCenter " + screenCenter);
    //
    //Vector3 screenHeight = new Vector3(Screen.width / 2, Screen.height, Camera.main.transform.position.z);
    //Debug.Log("screenHeight " + screenHeight);
    //
    //Vector3 screenWidth = new Vector3(Screen.width, Screen.height / 2, Camera.main.transform.position.z);
    //Debug.Log("screenWidth " + screenWidth);
    //
    if (!objetToFollow)
    {
      Destroy(gameObject);
    }
    poscam = Camera.main.transform.position;
    posEnemi = objetToFollow.transform.position;
    Vector3 goscreen = Camera.main.WorldToScreenPoint(objetToFollow.transform.position);
    Debug.Log("GoPos " + goscreen);

    float distX = Vector3.Distance(new Vector3(Screen.width/2, 0f, 0f), new Vector3(goscreen.x, 0f, 0f));
    Debug.Log("distX " + distX);

    float distY = Vector3.Distance(new Vector3(0f, Screen.height/2, 0f), new Vector3(0f, goscreen.y, 0f));
    Debug.Log("distY " + distY);

    if (distX > Screen.width/2 || distY > Screen.height/2)
    {
      GetComponent<Image>().enabled = true;
    }
    else
    {
      GetComponent<Image>().enabled = false;
    }
    forward = Camera.main.transform.forward;
    dir = objetToFollow.transform.position - (Camera.main.transform.position);
    dir.Normalize();
    realdir = dir - forward;
    realdir.z = 0;
    realdir.Normalize();
    Vector3 toAngle = realdir;
    if(Vector3.Dot(dir, forward)<0)
    {
      GetComponent<Image>().enabled = true;
    }
    width = canvas.rect.width;
    height = canvas.rect.height;
    angle = Vector3.Angle(Vector3.right, toAngle);
    if (toAngle.x>0)
    {
      canvasPos.x = ((canvas.rect.width / 2) - GetComponent<Image>().sprite.rect.width) * toAngle.x;
    }
    else
    {
      canvasPos.x = (-canvas.rect.width / 2  + GetComponent<Image>().sprite.rect.width) * -toAngle.x;
    }
    if (toAngle.y > 0)
    {
      canvasPos.y = canvas.rect.height / 4* toAngle.y;
    }
    else
    {
      canvasPos.y = -canvas.rect.height / 4 * -toAngle.y;
      angle *= -1;
    }
    GetComponent<RectTransform>().localPosition = canvasPos;
    GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,angle); ;
  }
}
