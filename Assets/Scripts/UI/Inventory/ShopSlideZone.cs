using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlideZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public InventoryPanel itemPanel;

	public Animator animator;

	public bool sellerSide;

	/// <summary>
	/// Pointer enter event
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData) {
		animator.SetBool("Seller", sellerSide);
		itemPanel.enabled = true;
	}

	/// <summary>
	/// Pointer enter Exit
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerExit(PointerEventData eventData) {
		itemPanel.enabled = false;
	} 

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float lrswitch = Input.GetAxis("Switch Game Menu");

		if (sellerSide) {
			if (lrswitch > 0) {
				animator.SetBool("Seller", sellerSide);
				itemPanel.enabled = true;
			} else if (lrswitch < 0) {
				itemPanel.enabled = false;
			}
		} else {
			if (lrswitch < 0) {
				animator.SetBool("Seller", sellerSide);
				itemPanel.enabled = true;
			} else if (lrswitch > 0) {
				itemPanel.enabled = false;
			}
		}
	}
}
