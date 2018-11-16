using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalPanel : MonoBehaviour {

    /// <summary>
    /// The image box that contains day text.
    /// </summary>
    public Image dayImage;

    /// <summary>
    /// A text representing days in-game.
    /// </summary>
    public Text dayText;

    /// <summary>
    /// The image box that contains time text.
    /// </summary>
    public Image timeImage;

    /// <summary>
    /// A text representing time in-game.
    /// </summary>
    public Text timeText;

    /// <summary>
    /// The image box that contains gold text.
    /// </summary>
    public Image goldImage;

    /// <summary>
    /// A text representing gold due in-game.
    /// </summary>
    public Text goldText;

    /// <summary>
    /// An image that acts as a menu button.
    /// When clicked it will lead to another menu ui.
    /// </summary>
    public Image menuImage;

    /// <summary>
    /// An image that acts as a save button.
    /// When clicked it will lead to another save ui.
    /// </summary>
    public Image saveImage;

    /// <summary>
    /// This manager is used to get gold amount of a player.
    /// </summary>
    public PlayerManager playerManager;

    /// <summary>
    /// This manager is used to get days and time in-game.
    /// </summary>
    public GameObject time;



    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Function used to set journal panel text data.
    void SetJournalPanel() {
        //Getting a reference to the player's inventory.
        CharacterInventory inventory = playerManager.GetComponent<CharacterInventory>();

        //Getting a reference to the Time in-game.
        TimeManager timeManager = time.GetComponent<TimeManager>();
        
        //Setting the text for day, time and gold due.
        dayText.text = "Day:\n" + timeManager.dayNumber;
        timeText.text = "Time:\n" + timeManager.currentTime;
        goldText.text = "Gold due:\n" + "$" + inventory.gold;
    }

    void JournalPanelControls() {
        //Input
        Button saveButton = saveImage.GetComponent<Button>();
        Button menuButton = menuImage.GetComponent<Button>();
        bool select = false;
        float vert = Input.GetAxis("Menu Vertical");

        if(vert < 0) {
            saveButton.Select();
            select = !select;

        }else if(vert > 0) {
            menuButton.Select();
            select = !select;
        }

        if(select && Input.GetButtonDown("Menu Confirm") == true) {
            GameObject.Find("Menu").SetActive(true);
        }else if(!select && Input.GetButtonDown("Menu Confirm") == true) {
            GameObject.Find("SaveSlotPanel").SetActive(true);
        }
        
    }

    public void ClosePanel() {
        GameObject.Find("Journal Panel").SetActive(false);
    }

    public void OpenSaveSlotPanel() {
        GameObject.Find("SaveSlotPanel").SetActive(true);
    }

}
