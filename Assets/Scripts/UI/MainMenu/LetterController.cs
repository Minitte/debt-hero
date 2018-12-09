using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using TMPro;

public class LetterController : MonoBehaviour {

	[Header("Component references")]
	/// <summary>
	/// Text Component representing the letter
	/// </summary>
	public TextMeshProUGUI letterText;

	/// <summary>
	/// Button to increment the text
	/// </summary>
	public Button incLetter;

	/// <summary>
	/// Button to decrement the text
	/// </summary>
	public Button decLetter;

	[Header("Letter")]

	/// <summary>
	/// Letter
	/// </summary>
	public char letter;

	/// <summary>
	/// Cool down flag
	/// </summary>
	private bool _cooldown;

	/// <summary>
	/// Time for cooldown
	/// </summary>
	private float _time;

	/// <summary>
	/// Original font size
	/// </summary>
	private float _fontSize;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		letter = letterText.text.ToCharArray()[0];
		letterText.text = letter + "";
		_fontSize = letterText.fontSize;

		enabled = false;

		HideLetterButtons();
	}

	/// <summary>
	/// Called when the mouse enters the GUIElement or Collider.
	/// </summary>
	void OnMouseEnter() {
		ShowLetterButtons();
	}

	/// <summary>
	/// Called when the mouse is not any longer over the GUIElement or Collider.
	/// </summary>
	void OnMouseExit() {
		HideLetterButtons();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {

		if (_cooldown) {
			_time += Time.deltaTime;

			if (_time > 0.15f) {
				_time = 0;
				_cooldown = false;
			} else {
				return;
			}
		}

		float vert = Input.GetAxis("Menu Vertical");

		if (vert > 0) {
			IncLetter();
			_cooldown = true;
		} else if (vert < 0) {
			DecLetter();
			_cooldown = true;
		}
	}

	/// <summary>
	/// Increments the letter
	/// </summary>
	public void IncLetter() {
		int nextLetter = (int)letter + 1;

		// dot (45)
		// go to the numbers (48 - 57)
		if (nextLetter == 47) {
			nextLetter = 48;
		}

		// numbers (48 - 57)
		// go to the alphabet (65 - 90)
		else if (nextLetter == 58) {
			nextLetter = 65;
		} 

		// alphabet (65 - 90)
		// wrap around to dot (46)
		else if (nextLetter == 91) {
			nextLetter = 46;
		}

		letter = (char)nextLetter;
		letterText.text = letter + "";
	}

	/// <summary>
	/// Decerements the letter
	/// </summary>
	public void DecLetter() {
		int nextLetter = (int)letter - 1;

		// dot (46)
		// wrap to alphabets (65 - 90)
		if (nextLetter == 45) {
			nextLetter = 90;
		}

		// numbers (48 - 57)
		// go to dot (47)
		else if (nextLetter == 47) {
			nextLetter = 46;
		} 

		// alphabet (65 - 90)
		// go to numbers (48 - 57)
		else if (nextLetter == 64) {
			nextLetter = 57;
		}

		letter = (char)nextLetter;
		letterText.text = letter + "";
	}

	/// <summary>
	/// Shows the letter buttons
	/// </summary>
	public void ShowLetterButtons() {
		incLetter.gameObject.SetActive(true);
		decLetter.gameObject.SetActive(true);
	}

	/// <summary>
	/// Hides the letter buttons
	/// </summary>
	public void HideLetterButtons() {
		incLetter.gameObject.SetActive(false);
		decLetter.gameObject.SetActive(false);	
	}

	/// <summary>
	/// Highlights the letter
	/// </summary>
	public void HighLight() {
		letterText.fontSize = 60f;
		letterText.fontStyle = FontStyles.Underline;
	}

	/// <summary>
	/// unhighlights the letter
	/// </summary>
	public void UnHighLight() {
		letterText.fontSize = _fontSize;
		letterText.fontStyle = FontStyles.Normal;
	}
}
