using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for the player's resource bars.
/// </summary>
public class PlayerResources : MonoBehaviour {
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

    /// <summary>
    /// Reference to the player's character stats.
    /// </summary>
    private CharacterStats _playerStats;

    // Use this for initialization
    private void Start() {
        _playerStats = PlayerManager.instance.GetComponent<CharacterStats>();

        // Initial update for the bars
        UpdateHealth();
        UpdateMana();
        UpdateExp();

        // Setup events
        _playerStats.OnHealthChanged += UpdateHealth;
        _playerStats.OnManaChanged += UpdateMana;
        _playerStats.OnExpChanged += UpdateExp;
    }

    /// <summary>
    /// Updates the player's health bar.
    /// </summary>
    public void UpdateHealth() {
        // Calculate health percentage
        float percentage = _playerStats.currentHp
            / _playerStats.maxHp * 100f;

        // Update health information
        healthSlider.value = percentage;
        healthValue.text = "HP:" + _playerStats.currentHp + "/" + _playerStats.maxHp;
    }

    /// <summary>
    /// Updates the player's mana bar.
    /// </summary>
    public void UpdateMana() {
        // Calculate mana percentage
        float percentage = _playerStats.currentMp
            / _playerStats.maxMp * 100f;

        // Update mana information
        manaSlider.value = percentage;
        manaValue.text = "MP:" + _playerStats.currentMp + "/" + _playerStats.maxMp;
    }

    /// <summary>
    /// Updates the player's experience bar.
    /// </summary>
    public void UpdateExp() {
        // Calculate exp percentage
        float percentage = _playerStats.exp
            / _playerStats.maxExp * 100f;

        // Update experience information
        expSlider.value = percentage; // Update Exp bar
        expValue.text = "EXP:" + _playerStats.exp + "/" + _playerStats.maxExp;
    }

    /// <summary>
    /// Additional stuff to do when this object is destroyed.
    /// </summary>
    private void OnDestroy() {
        // Unsubscribe from events
        _playerStats.OnHealthChanged -= UpdateHealth;
        _playerStats.OnManaChanged -= UpdateMana;
        _playerStats.OnExpChanged -= UpdateExp;
    }
}
