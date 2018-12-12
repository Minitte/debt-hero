using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    /// <summary>
    /// Game Over Text.
    /// </summary>
    public Text gameOver;

    /// <summary>
    /// The delayed timer for letters
    /// to show up on screen.
    /// </summary>
    public float delay;

    /// <summary>
    /// The Message to be displayed.
    /// </summary>
    public string message;

    /// <summary>
    /// Button used to continue/load game
    /// </summary>
    public Button continueGame;

    /// <summary>
    /// Button used to quit/return to menu
    /// </summary>
    public Button quitGame;

    /// <summary>
    /// Load Panel Used to load a save file.
    /// </summary>
    public GameObject LoadPanel;

    /// <summary>
    /// Used to handle Ps4 Inputs.
    /// </summary>
    public int _selected;
	// Use this for initialization

    [Header("Score Stuff")]

    /// <summary>
    /// Text representing 1st, 2nd... etc
    /// </summary>
    public Text placeText;

    /// <summary>
    /// Text representing score
    /// </summary>
    public Text scoreText;

	void Start () {
        //Set Text to blank
        gameOver.text = "";
        
        //Used to show a char one by one.
        StartCoroutine(DelayText());

        //Enabling the buttons.
        continueGame.gameObject.SetActive(true);
        quitGame.gameObject.SetActive(true);
        _selected = 0;

        List<ScoreEntry> scores = Scoreboard.GetScores();

        for (int i = 0; i < scores.Count; i++) {
            if (scores[i].name.Equals(PlayerProgress.name)) {
                
                placeText.text = "#" + (i + 1);

                scoreText.text = scores[i].score.ToString("n0") + "g";

                break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        PS4Controls();
	}

    /// <summary>
    /// IEnumerator that reads a char array
    /// and displays the msg on screen.
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayText() {
        foreach (char c in message.ToCharArray()) {
            gameOver.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
    
    /// <summary>
    /// Load to main menu
    /// </summary>
    public void QuitGame() {
        DestroyImmediate(PlayerManager.instance.gameObject);
        DestroyImmediate(EventManager.instance.gameObject);
        DestroyImmediate(GameDatabase.instance.gameObject);
        PlayerManager.instance = null;
        EventManager.instance = null;
        TimeManager.instance = null;
        GameDatabase.instance = null;
        SceneManager.LoadScene("LandingMenu");

    }

    /// <summary>
    /// Open Up LoadPanel.
    /// </summary>
    public void ContinueGame() {
        LoadPanel.SetActive(true);
    }

    public void PS4Controls() {
        float hori = Input.GetAxis("Menu Horizontal");

        if(hori < 0) {
            continueGame.Select();
            _selected = 1;
        }else if(hori > 1) {
            quitGame.Select();
            _selected = 2;
        }

        if(_selected == 1 && Input.GetAxis("Menu Confirm")!= 0) {
            this.ContinueGame();
        }else if(_selected == 2 && Input.GetAxis("Menu Confirm")!= 0) {
            this.QuitGame();
        }
    }
}
