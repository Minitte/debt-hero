using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
public class JournalPanel : MonoBehaviour {

    [Header("Text")]
    /// <summary>
    /// A text representing days in-game.
    /// </summary>
    public Text dayText;

    /// <summary>
    /// A text representing time in-game.
    /// </summary>
    public Text timeText;

    /// <summary>
    /// A text representing gold due in-game.
    /// </summary>
    public Text goldText;

    [Header("Button")]

    /// <summary>
    /// An image that acts as a menu button.
    /// When clicked it will lead to another menu ui.
    /// </summary>
    public Button menuButton;

    /// <summary>
    /// An image that acts as a save button.
    /// When clicked it will lead to another save ui.
    /// </summary>
    public Button saveButton;

    [Header("Game Object References")]

    /// <summary>
    /// This manager is used to get days and time in-game.
    /// </summary>
    public GameObject time;

    /// <summary>
    /// Panel containg save slots
    /// </summary>
    public GameObject saveSlotPanel;

    /// <summary>
    /// This manager is used to get gold amount of a player.
    /// </summary>
    private PlayerManager _playerManager;

    /// <summary>
    /// SElected menu
    /// </summary>
    private int _selected;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        _playerManager = PlayerManager.instance;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable() {
        SetJournalPanel();

        _selected = 0;
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        JournalPanelControls();
    }

    //Function used to set journal panel text data.
    void SetJournalPanel() {
        if (_playerManager == null) {
            _playerManager = PlayerManager.instance;
        }

        //Getting a reference to the player's inventory.
        CharacterInventory inventory = _playerManager.GetComponent<CharacterInventory>();

        //Getting a reference to the Time in-game.
        TimeManager timeManager = time.GetComponent<TimeManager>();

        EventManager eventManager = GameObject.Find("Event Manager").GetComponent<EventManager>();
        //Setting the text for day, time and gold due.
        dayText.text = "Day:\n" + timeManager.days;
        timeText.text = "Time:\n" + timeManager.currentTime;
        goldText.text = "Gold due:\n" + eventManager.DebtCurve(timeManager.days + 3) + "g";
    }

    private void JournalPanelControls() { 
        float vert = Input.GetAxis("Menu Vertical");

        if(vert < 0) {
            saveButton.Select();
            _selected = 1;

        } else if(vert > 0) {
            menuButton.Select();
            _selected = 2;
        }

        if(_selected == 2 && Input.GetAxis("Menu Confirm")!=0) {
            Debug.Log("MenuSelect");
            SceneManager.LoadScene("LandingMenu");
        } else if(_selected == 1 && Input.GetAxis("Menu Confirm")!=0) {
            Debug.Log("SaveSelect");
            saveSlotPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }

    /// <summary>
    /// Opens the save slot panel
    /// </summary>
    public void OpenSaveSlotPanel() {
        saveSlotPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OpenMainMenu() {
        SceneManager.LoadScene("LandingMenu");
    }

}
