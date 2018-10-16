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
    /// Reference to the health bar prefab.
    /// </summary>
    public GameObject healthBar;

    /// <summary>
    /// Reference to the damage text prefab.
    /// </summary>
    public GameObject textPrefab;

    /// <summary>
    /// An offset to the damage text's position.
    /// </summary>
    public Vector3 textOffset;

    // Use this for initialization
    private void Awake() {
        instance = this;
    }

    /// <summary>
    /// Creates a text object on the canvas showing a damage or healing taken number.
    /// </summary>
    /// <param name="value">The value of the damage or healing taken</param>
    /// <param name="damage">True if its a damage event, false if its a healing event</param>
    /// <param name="victim">The gameobject that took damage or healing</param>
    public void CreateCombatNumber(float value, bool damage, GameObject victim) {
        // Create the text object and move it onto the canvas
        GameObject textObject = Instantiate(textPrefab, transform);
        textObject.transform.position = Camera.main.WorldToScreenPoint(victim.transform.position + textOffset);

        // Get the actual text field
        Text text = textObject.transform.Find("FloatingText").GetComponent<Text>();
        text.text = value.ToString(); // Change the text to the net damage taken

        if (damage) {
            // Change the text color depending on if its a player or mob that took damage
            if (victim.tag == "Player") {
                text.color = Color.red; // Player took damage
            } else {
                text.color = Color.yellow; // Mob took damage
            }
        } else {
            text.color = Color.green; // Gameobject took healing
        }
    }

    /// <summary>
    /// Creates a health bar on the canvas for the input gameobject.
    /// </summary>
    /// <param name="owner">The owner of the health bar</param>
    public HealthBar CreateHealthBar(GameObject owner) {
        // Create the health bar object as a child
        GameObject hpObject = Instantiate(healthBar, transform);
        hpObject.GetComponent<HealthBar>().owner = owner;
        return hpObject.GetComponent<HealthBar>();
    }
}
