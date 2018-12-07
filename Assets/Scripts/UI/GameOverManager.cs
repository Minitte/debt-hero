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

	// Use this for initialization
	void Start () {
        //Set Text to blank
        gameOver.text = "";
        
        //Used to show a char one by one.
        StartCoroutine(DelayText());

        //Enabling the buttons.
        continueGame.gameObject.SetActive(true);
        quitGame.gameObject.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
		
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

    public void QuitGame() {
        SceneManager.LoadScene("LandingMenu");

    }

    public void ContinueGame() {
        
        LoadPanel.SetActive(true);
    }
}
