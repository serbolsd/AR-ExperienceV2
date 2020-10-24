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
  public float width;
  public float height;
  public float angle;

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
    Vector3 goscreen = Camera.main.WorldToScreenPoint(objetToFollow.transform.position);
    Debug.Log("GoPos " + goscreen);

    float distX = Vector3.Distance(new Vector3(Screen.width/2, 0f, 0f), new Vector3(goscreen.x, 0f, 0f));
    Debug.Log("distX " + distX);

    float distY = Vector3.Distance(new Vector3(0f, Screen.height/2, 0f), new Vector3(0f, goscreen.y, 0f));
    Debug.Log("distY " + distY);

    if (distX > Screen.width || distY > Screen.height/2)
    {
      GetComponent<Image>().enabled = true;
    }
    else
    {
      GetComponent<Image>().enabled = false;
    }
    dir = objetToFollow.transform.position- Camera.main.transform.position;
    dir.z = 0;
    dir.Normalize();
    width = canvas.rect.width;
    height = canvas.rect.height;
    angle = Vector3.Angle(Vector3.right, dir);
    if (dir.x>0)
    {
      canvasPos.x = ((canvas.rect.width / 2) - GetComponent<Image>().sprite.rect.width) * dir.x;
    }
    else
    {
      canvasPos.x = (-canvas.rect.width / 2  + GetComponent<Image>().sprite.rect.width) * -dir.x;
    }
    if (dir.y > 0)
    {
      canvasPos.y = canvas.rect.height / 4*dir.y;
    }
    else
    {
      canvasPos.y = -canvas.rect.height / 4 * -dir.y;
      angle *= -1;
    }
    GetComponent<RectTransform>().localPosition = canvasPos;
    GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,angle); ;
  }
}
