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
    /// Reference to the damage text prefab.
    /// </summary>
    public GameObject damageText;

    /// <summary>
    /// An offset to the damage text's position.
    /// </summary>
    public Vector3 textOffset;

    // Use this for initialization
    private void Start() {
        instance = this;
    }

    /// <summary>
    /// Creates a text object on the canvas showing a damage taken number.
    /// </summary>
    /// <param name="netDamage">The net amount of damage taken</param>
    /// <param name="victim">The gameobject that took damage</param>
    public void CreateDamageNumber(float netDamage, GameObject victim) {
        // Create the text object and move it onto the canvas
        GameObject textObject = Instantiate(damageText, transform);
        textObject.transform.position = Camera.main.WorldToScreenPoint(victim.transform.position + textOffset);

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
}
