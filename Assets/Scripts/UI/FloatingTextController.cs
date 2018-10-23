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
        textObject.transform.Find("FloatingText").GetComponent<FloatingText>().Owner = victim.transform;
        textObject.transform.position = victim.transform.position;

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

        // Have it respond to health change event
        owner.GetComponent<BaseCharacter>().characterStats.OnHealthChanged += hpObject.GetComponent<HealthBar>().UpdateHealth;
        hpObject.GetComponent<HealthBar>().UpdateHealth(); // Initial health update
        return hpObject.GetComponent<HealthBar>();
    }
}
