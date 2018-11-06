using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for the player's resource bars.
/// </summary>
public class PlayerResourceBars : MonoBehaviour {

    /// <summary>
    /// HP Text portion of the Image.
    /// </summary>
    public Text hpValue;

    /// <summary>
    /// MP Text portion of the Image.
    /// </summary>
    public Text mpValue;

    /// <summary>
    /// EXP Text portion of the Image.
    /// </summary>
    public Text expValue;

    /// <summary>
    /// Reads info from a CharacterStats object.
    /// </summary>
    private CharacterStats _characterStats;

    /// <summary>
    /// Property variable for character stats.
    /// </summary>
    public CharacterStats CharacterStats {
        set {
            _characterStats = value;
        }
    }


    // Use this for initialization
    private void Start()
    {
        //BarColor(255, 25, 5, 255, hpBar);
        //BarColor(107, 114, 255, 255, mpBar);
        hpValue = transform.Find("HP").Find("HPText").GetComponent<Text>();
        mpValue = transform.Find("MP").Find("MPText").GetComponent<Text>();
        expValue = transform.Find("EXP").Find("ExpText").GetComponent<Text>();

        // Makes the slider no interactable
        transform.Find("HP").GetComponent<Slider>().interactable = false;
        transform.Find("MP").GetComponent<Slider>().interactable = false;
        transform.Find("EXP").GetComponent<Slider>().interactable = false;

        // Initial update for the bars
        UpdateHealth();
        UpdateMana();
        UpdateExp();

        InGameMenuController.OnMenuShown += HideBars;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_characterStats != null) {
            DrawBar(_characterStats); // Draw the bar
        }
    }

    /// <summary>
    /// Reads a characterStats and sets the information to the text object.
    /// </summary>
    /// <param name="characterStats">Reading stats from a chracterStats object.</param>
    private void DrawBar(CharacterStats characterStats)
    {
        hpValue.text = "HP:" + characterStats.currentHp + "/" + characterStats.maxHp;
        mpValue.text = "MP:" + characterStats.currentMp + "/" + characterStats.maxMp;
        expValue.text = "EXP:" + characterStats.exp + "/" + characterStats.maxExp;
    }

    /// <summary>
    /// Setting the color of the bar
    /// </summary>
    /// <param name="Red"> Red value of a color.</param>
    /// <param name="Green">Green value of a color.</param>
    /// <param name="Blue">Blue value of a color.</param>
    /// <param name="Alpha">Alpha of a color.</param>
    /// <param name="Bar">The image bar to change.</param>
    private void BarColor(byte Red, byte Green, byte Blue, byte Alpha, Image Bar)
    {
        Bar.color = new Color32(Red, Green, Blue, Alpha);
    }

    /// <summary>
    /// Updates the player's health bar.
    /// </summary>
    public void UpdateHealth() {
        // Calculate health percentage
        float percentage = _characterStats.currentHp
            / _characterStats.maxHp * 100f;

        transform.Find("HP").GetComponent<Slider>().value = percentage; // Update health bar
    }

    /// <summary>
    /// Updates the player's mana bar.
    /// </summary>
    public void UpdateMana() {
        // Calculate mana percentage
        float percentage = _characterStats.currentMp
            / _characterStats.maxMp * 100f;

        transform.Find("MP").GetComponent<Slider>().value = percentage; // Update mana bar
    }

    /// <summary>
    /// Updates the player's EXP bar.
    /// </summary>
    public void UpdateExp() {
        // Calculate exp percentage
        float percentage = _characterStats.exp
            / _characterStats.maxExp * 100f;
        transform.Find("EXP").GetComponent<Slider>().value = percentage; // Update Exp bar
    }
    /// Hides or shows the bars
    /// </summary>
    /// <param name="hide"></param>
    private void HideBars(bool hide) {
        this.gameObject.SetActive(!hide);
    }
}
