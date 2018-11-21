using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for the player's resource bars.
/// </summary>
public class PlayerResources : MonoBehaviour {

    [Header("Player stats")]
    /// <summary>
    /// Reference to the player's character stats.
    /// </summary>
    public CharacterStats characterStats;

    [Header("Resource texts")]
    /// <summary>
    /// Health text value.
    /// </summary>
    public Text healthValue;

    /// <summary>
    /// Mana text value.
    /// </summary>
    public Text manaValue;

    /// <summary>
    /// Experience text value.
    /// </summary>
    public Text expValue;

    [Header("Resource sliders")]
    /// <summary>
    /// Health bar slider reference.
    /// </summary>
    public Slider healthSlider;

    /// <summary>
    /// Mana bar slider reference.
    /// </summary>
    public Slider manaSlider;

    /// <summary>
    /// Experience bar slider reference.
    /// </summary>
    public Slider expSlider;

    // Use this for initialization
    private void Start() {
        // Initial update for the bars
        UpdateHealth();
        UpdateMana();
        UpdateExp();
    }

    /// <summary>
    /// Updates the player's health bar.
    /// </summary>
    public void UpdateHealth() {
        // Calculate health percentage
        float percentage = characterStats.currentHp
            / characterStats.maxHp * 100f;

        // Update health information
        healthSlider.value = percentage;
        healthValue.text = "HP:" + characterStats.currentHp + "/" + characterStats.maxHp;
    }

    /// <summary>
    /// Updates the player's mana bar.
    /// </summary>
    public void UpdateMana() {
        // Calculate mana percentage
        float percentage = characterStats.currentMp
            / characterStats.maxMp * 100f;

        // Update mana information
        manaSlider.value = percentage;
        manaValue.text = "MP:" + characterStats.currentMp + "/" + characterStats.maxMp;
    }

    /// <summary>
    /// Updates the player's experience bar.
    /// </summary>
    public void UpdateExp() {
        // Calculate exp percentage
        float percentage = characterStats.exp
            / characterStats.maxExp * 100f;

        // Update experience information
        expSlider.value = percentage; // Update Exp bar
        expValue.text = "EXP:" + characterStats.exp + "/" + characterStats.maxExp;
    }

    private void OnDestroy() {
        // Unsubscribe from events when this object is destroyed
        //PlayerManager.instance.GetComponent<CharacterStats>().OnHealthChanged -= UpdateHealth;
        //PlayerManager.instance.GetComponent<CharacterStats>().OnExpChanged -= UpdateExp;
    }
}
