using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridItemUI : MonoBehaviour {

	/// <summary>
	/// Event template
	/// </summary>
	/// <param name="slot"></param>
	public delegate void ItemGridEvent(ItemSlot slot);

	/// <summary>
	/// Select event
	/// </summary>
	public static event ItemGridEvent OnLeftClick;

	public static event ItemGridEvent OnRightClick;

	/// <summary>
	/// row col slot of this
	/// </summary>
	public ItemSlot slot;

	/// <summary>
	/// If this is hovered over on
	/// </summary>
	private bool _hoverOver;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if (_hoverOver) {
			if (Input.GetMouseButtonDown(0)) {
				if (OnLeftClick != null) {
					OnLeftClick(slot);
				}
			} else if(Input.GetMouseButtonDown(1)) {
				if (OnRightClick != null) {
					OnRightClick(slot);
				}
			}
		}
	}

	/// <summary>
	/// Called every frame while the mouse is over the GUIElement or Collider.
	/// </summary>
	void OnMouseOver() {
		_hoverOver = true;
	}

	/// <summary>
	/// Called when the mouse is not any longer over the GUIElement or Collider.
	/// </summary>
	void OnMouseExit() {
		_hoverOver = false;
	}

	/// <summary>
	/// OnMouseDown is called when the user has pressed the mouse button while
	/// over the GUIElement or Collider.
	/// </summary>
	void OnMouseDown() {
		if (OnLeftClick != null) {
			OnLeftClick(slot);
		}
	}
}
