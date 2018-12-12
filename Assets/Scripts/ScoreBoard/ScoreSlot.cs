using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreSlot : MonoBehaviour {

	/// <summary>
	/// Text for displaying name
	/// </summary>
	public TextMeshProUGUI nameText;

	/// <summary>
	/// Text for displaying score
	/// </summary>
	public TextMeshProUGUI scoreText;

	/// <summary>
	/// The default name value
	/// </summary>
	private string _defaultName;

	/// <summary>
	/// The default score value
	/// </summary>
	private string _defaultScore;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		_defaultName = nameText.text;

		_defaultScore = scoreText.text;
	}

	/// <summary>
	/// Sets this score slot's name and score text with the given entry
	/// </summary>
	/// <param name="score"></param>
	public void SetEntry(ScoreEntry score) {
		nameText.text = score.name;

		scoreText.text = score.score.ToString("n0") + "g";
	}

	/// <summary>
	/// Resets the name and score text to default values
	/// </summary>
	public void ResetToDefault() {
		nameText.text = _defaultName;

		scoreText.text = _defaultScore;
	}
}
