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
    public Text value;

    /// <summary>
    /// An offset for the health bar.
    /// </summary>
    public Vector3 offset;

    /// <summary>
    /// The owner of the health bar.
    /// </summary>
    private GameObject _owner;

    /// <summary>
    /// Property variable for owner.
    /// </summary>
    public GameObject owner {
        get {
            return _owner;
        }
        set {
            _owner = value;
        }
    }

    // Use this for initialization
    private void Start() {
        BarColor(176, 25, 5, 255); // Default health bar colour
    }

    // Update is called once per frame
    private void Update()
    {
        // Stay next to the owner
        if (_owner != null) {
            transform.Find("HealthBarColor").position = Camera.main.WorldToScreenPoint(owner.transform.position + offset);
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
