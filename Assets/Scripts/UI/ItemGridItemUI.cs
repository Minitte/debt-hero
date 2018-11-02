using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemGridItemUI : MonoBehaviour, IPointerClickHandler {

	/// <summary>
	/// Event template
	/// </summary>
	/// <param name="slot"></param>
	public delegate void ItemGridEvent(ItemSlot slot);

	/// <summary>
	/// left click event
	/// </summary>
	public static event ItemGridEvent OnLeftClick;

	/// <summary>
	/// Right click event
	/// </summary>
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
	/// Click event
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Left) {
			if (OnLeftClick != null) {
				OnLeftClick(slot);
			}
		} else if (eventData.button == PointerEventData.InputButton.Right) {
			if (OnRightClick != null) {
				OnRightClick(slot);
			}
		}
	}
}
