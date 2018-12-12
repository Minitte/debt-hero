using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour {

	/// <summary>
	/// Button to go back with
	/// </summary>
	public Button backButton;

	/// <summary>
	/// List of score slots
	/// </summary>
	public ScoreSlot[] scores;
	
	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		UpdateScoreSlots();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if (Input.GetAxis("Menu Cancel") != 0) {
			backButton.onClick.Invoke();
		}
	}

	/// <summary>
	/// Updates the score with the interal scoreboard
	/// </summary>
	public void UpdateScoreSlots() {
		List<ScoreEntry> s = Scoreboard.GetScores();

		for (int i = 0; i < scores.Length; i++) {
			if (i < s.Count) {
				scores[i].SetEntry(s[i]);
			} else {
				scores[i].ResetToDefault();
			}
		}
	}
}
