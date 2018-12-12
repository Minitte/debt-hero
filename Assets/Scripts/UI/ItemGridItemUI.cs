using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemGridItemUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	/// <summary>
	/// Event template
	/// </summary>
	/// <param name="slot"></param>
	public delegate void ItemGridEvent(ItemSlot slot);

	/// <summary>
	/// left click event
	/// </summary>
	public event ItemGridEvent OnLeftClick;

	/// <summary>
	/// Right click event
	/// </summary>
	public event ItemGridEvent OnRightClick;

	/// <summary>
	/// mouse hover over event
	/// </summary>
	public event ItemGridEvent OnHoverOver;

	/// <summary>
	/// mouse hover off event
	/// </summary>
	public event ItemGridEvent OnHoverOff;

	/// <summary>
	/// row col slot of this
	/// </summary>
	public ItemSlot slot;

	/// <summary>
	/// Animator reference
	/// </summary>
	private Animator _animator;

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
		if (slot == null) {
			Debug.Log("Slot is null?");
		}

		if (OnHoverOver != null) {
			OnHoverOver(slot);
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		if (slot == null) {
			Debug.Log("Slot is null?");
		}
		
		if (OnHoverOff != null) {
			OnHoverOff(slot);
		}
	}

	/// <summary>
	/// Shows or hides the border
	/// </summary>
	public void SetBorderVisiblity(bool show) {
		if (_animator == null) {
			_animator = GetComponent<Animator>();
		}

		_animator.SetBool("Border Visible", show);

		// if (!show) {
		// 	_animator.SetBool("Border Flashing", false);
		// }
	}

	/// <summary>
	/// Makes the border flash
	/// </summary>
	/// <param name="flash"></param>
	public void SetBorderFlash(bool flash) {
		if (_animator == null) {
			_animator = GetComponent<Animator>();
		}

		_animator.SetBool("Border Flashing", flash);
	
		if (flash) {
			_animator.SetBool("Border Visible", true);
		}
	}

	/// <summary>
	/// Set border to equip
	/// </summary>
	/// <param name="equip"></param>
	public void SetBorderEquip(bool equip) {
		if (_animator == null) {
			_animator = GetComponent<Animator>();
		}

		_animator.SetBool("Border Equipped", equip);
		_animator.SetBool("Border Visible", equip);

		if (!equip) {
			_animator.SetBool("Border Flashing", false);
		}
	}
}
