using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    /// <summary>
    /// The title of the user.
    /// </summary>
    public Text title;

    /// <summary>
    /// The color of the bar.
    /// </summary>
    public Image bar;

    /// <summary>
    /// The health value of a user.
    /// </summary>
    /// 
    public Text value;

    /// <summary>
    /// An offset for the health bar.
    /// </summary>
    public Vector3 offset;

    /// <summary>
    /// An offset for the health bar.
    /// </summary>
    private Vector3 _offset;

    private void Update()
    {
        // Stay next to the character
        transform.Find("HealthBarColor").position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    /// <summary>
    /// Setting the name of the bar.
    /// </summary>
    /// <param name="Name"> Name of the bar</param>
    public void BarGenerateName(string title)
    {
        this.title.text = title;
    }

    /// <summary>
    /// Setting the position of the bar. 
    /// </summary>
    /// <param name="Position"></param>
    public void BarPosition(Vector3 Position)
    {
        this.transform.position = new Vector3(Position.x, Position.y + 0.8f, Position.z);

    }

    /// <summary>
    /// Setting the color of the bar
    /// </summary>
    /// <param name="Red"> Red value of a color</param>
    /// <param name="Green">Green value of a color</param>
    /// <param name="Blue">Blue value of a color</param>
    /// <param name="Alpha">Alpha of a color</param>
    public void BarColor(byte Red, byte Green, byte Blue, byte Alpha)
    {
        bar.color = new Color32(Red,Green,Blue,Alpha);
    }

    public void BarValue(string value)
    {
        this.value.text = value;
    }
}
