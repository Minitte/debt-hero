using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for the player's resource bars.
/// </summary>
public class PlayerResourceBars : MonoBehaviour
{
    /// <summary>
    /// Image used to represent HP.
    /// </summary>
    public Image hpBar;

    /// <summary>
    /// Image used to represent MP.
    /// </summary>
    public Image mpBar;

    /// <summary>
    /// HP Text portion of the Image.
    /// </summary>
    public Text hpValue;

    /// <summary>
    /// MP Text portion of the Image.
    /// </summary>
    public Text mpValue;

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
        BarColor(255, 25, 5, 255, hpBar);
        BarColor(107, 114, 255, 255, mpBar);
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
}
