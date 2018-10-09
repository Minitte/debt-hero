using System.Collections;
using System.Collections.Generic;
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
    /// The owner of the health bar.
    /// </summary>
    private GameObject _owner;

    /// <summary>
    /// An offset for the health bar.
    /// </summary>
    private Vector3 _offset;

    /// <summary>
    /// Whether the health bar is ready or not
    /// </summary>
    private bool ready;

    /// <summary>
    /// Initializes the health bar.
    /// </summary>
    /// <param name="owner">The owner of the health bar</param>
    /// <param name="offset">An offset for the position of the health bar</param>
    public void Initialize(GameObject owner, Vector3 offset) {
        _owner = owner;
        _offset = offset;
        ready = true;
    }

    private void Update()
    {
        if (ready)
        {
            transform.position = FloatingTextController.instance.Convert3DTo2D(_owner.transform.position + _offset);
        }

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
