using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	// Use this for initialization
	void Start () {
        gameOver.text = "";
        StartCoroutine(DelayText());
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
}
