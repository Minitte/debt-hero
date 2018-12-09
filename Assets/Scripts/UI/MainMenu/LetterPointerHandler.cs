using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	private LetterController _letterController;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_letterController = GetComponent<LetterController>();
	}

	//Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData) {
        _letterController.ShowLetterButtons();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData) {
        _letterController.HideLetterButtons();
    }
}
