using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
    public Text gameOver;
    public float delay;
    public string message;

	// Use this for initialization
	void Start () {
        gameOver.text = "";
        StartCoroutine(DelayText());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DelayText() {
        foreach (char c in message.ToCharArray()) {
            gameOver.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}
