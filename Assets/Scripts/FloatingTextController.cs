using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for handling damage numbers.
/// </summary>
public class FloatingTextController : MonoBehaviour {

    /// <summary>
    /// Singleton object for FloatingTextController.
    /// </summary>
    public static FloatingTextController instance;

    /// <summary>
    /// Reference to the healthbar prefab.
    /// </summary>
    public GameObject healthBar;

    /// <summary>
    /// Reference to the damage text prefab.
    /// </summary>
    public GameObject damageText;

    /// <summary>
    /// An offset to the damage text's position.
    /// </summary>
    public Vector3 textOffset;

    /// <summary>
    /// An offset to the health bar's position.
    /// </summary>
    public Vector3 healthBarOffset;

    /// <summary>
    /// Reference to the game's camera.
    /// </summary>
    private Camera _camera;

    // Use this for initialization
    private void Start() {
        instance = this;
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    /// <summary>
    /// Creates a text object on the canvas showing a damage taken number.
    /// </summary>
    /// <param name="netDamage">The net amount of damage taken</param>
    /// <param name="victim">The gameobject that took damage</param>
    public void CreateDamageNumber(float netDamage, GameObject victim) {
        // Create the text object and move it onto the canvas
        GameObject textObject = Instantiate(damageText, transform);
        textObject.transform.position = _camera.WorldToScreenPoint(victim.transform.position + textOffset);

        // Get the actual text field
        Text text = textObject.transform.Find("DamageText").GetComponent<Text>();
        text.text = netDamage.ToString(); // Change the text to the net damage taken

        // Change the text color depending on if its a player or mob that took damage
        if (victim.tag == "Player") {
            text.color = Color.red; // Player took damage
        } else {
            text.color = Color.yellow; // Mob took damage
        }
    }

    /// <summary>
    /// Creates a health bar for the input gameobject.
    /// </summary>
    /// <param name="owner">The owner of the healthbar</param>
    public HealthBar CreateHealthBar(GameObject owner)
    {
        // Create the health bar gameobject
        GameObject hpObject = Instantiate(healthBar, transform);
        hpObject.transform.position = _camera.WorldToScreenPoint(owner.transform.position + textOffset);
        hpObject.GetComponent<HealthBar>().Initialize(owner, textOffset);

        // Set the colour
        HealthBar hp = hpObject.GetComponent<HealthBar>();
        hp.BarColor(176, 25, 5, 255);

        return hp;
    }

    /// <summary>
    /// Helper function to convert a position from 3D space to 2D space.
    /// </summary>
    /// <param name="position">The position in 3D space</param>
    /// <returns>tHe position in 2D space</returns>
    public Vector3 Convert3DTo2D(Vector3 position)
    {
        return _camera.WorldToScreenPoint(position);
    }
}
