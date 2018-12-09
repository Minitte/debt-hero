using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClassCardController : MonoBehaviour {

	/// <summary>
	/// Border of the class card
	/// </summary>
	public Image border;

	/// <summary>
	/// Class Image
	/// </summary>
	public Image classImage;

	/// <summary>
	/// Name of the class
	/// </summary>
	public string className;

	/// <summary>
	/// Colour of the class
	/// </summary>
	public Color classColour;

	/// <summary>
	/// highlights the class image
	/// </summary>
	public void ShowHighlight() {
		classImage.color = classColour;
	}

	/// <summary>
	/// unhighlights the class image
	/// </summary>
	public void HideHighlight() {
		classImage.color = Color.white;
	}

	/// <summary>
	/// Shows the border
	/// </summary>
	public void ShowBorder() {
		border.gameObject.SetActive(true);
	}

	/// <summary>
	/// Hides the border
	/// </summary>
	public void HideBorder() {
		border.gameObject.SetActive(false);
	}

}
