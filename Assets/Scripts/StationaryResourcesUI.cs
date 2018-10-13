using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationaryResourcesUI : MonoBehaviour
{
    /// <summary>
    /// Reads info from a CharacterStats object.
    /// </summary>
    public CharacterStats characterStats;

    /// <summary>
    /// Setting the instance.
    /// </summary>
    private CharacterStats characterStatsIntance;

    /// <summary>
    /// Image use to represent HP.
    /// </summary>
    public Image hpBar;

    /// <summary>
    /// Image use to represent MP.
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

    // Use this for initialization
    void Start()
    {
        characterStats = transform.parent.GetComponent<CharacterStats>();
        BarColor(255, 25, 5, 255, hpBar);
        BarColor(107, 114, 255, 255, mpBar);
    }

    // Update is called once per frame
    void Update()
    {
        DrawBar(characterStats);
    }

    /// <summary>
    /// Reads a characterStats and sets the information to the text object.
    /// </summary>
    /// <param name="characterStats">Reading stats from a chracterStats object.</param>
    void DrawBar(CharacterStats characterStats)
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
    public void BarColor(byte Red, byte Green, byte Blue, byte Alpha, Image Bar)
    {
        Bar.color = new Color32(Red, Green, Blue, Alpha);
    }

    /// <summary>
    /// Setting the position of the bars.
    /// </summary>
    /// <param name="position"> A vector use to set the location of the stationary ui object.</param>
    public void BarPosition(Vector3 position)
    {
        
        this.transform.position = new Vector3(position.x, position.y - 2.5f, position.z + 1f);
    }
}
