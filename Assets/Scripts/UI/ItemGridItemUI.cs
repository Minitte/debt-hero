using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemGridItemUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {

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
	/// mouse hover over event
	/// </summary>
	public static event ItemGridEvent OnHoverOver;

	/// <summary>
	/// row col slot of this
	/// </summary>
	public ItemSlot slot;

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

	/// <summary>
	/// Mouse hover over event
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData) {
		if (OnHoverOver != null) {
			OnHoverOver(slot);
		}
	}
}
