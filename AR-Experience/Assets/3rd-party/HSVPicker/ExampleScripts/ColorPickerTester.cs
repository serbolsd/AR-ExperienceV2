using UnityEngine;
namespace HSVPicker.Examples
{
  public class ColorPickerTester : MonoBehaviour
  {

    public new Renderer renderer = null;
    public ColorPicker picker;

    public Color Color = Color.red;
    public bool SetColorOnStart = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onStart()
    {
      if (renderer == null)
      {
        return;
      }
      //picker.onValueChanged.AddListener(color =>
      //{
      //  renderer.material.color = color;
      //  Color = color;
      //});

      renderer.material.color = picker.CurrentColor;
      if (SetColorOnStart)
      {
        picker.CurrentColor = Color;
        //renderer.material.color = Color;
      }
    }
  }
}