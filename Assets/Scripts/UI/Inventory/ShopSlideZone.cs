using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlideZone : MonoBehaviour, IPointerEnterHandler {

	public ShopController shopController;

	public bool sellerSide;

	/// <summary>
	/// Pointer enter event
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData) {
		if (sellerSide) {
			shopController.ShowSeller();
		} else {
			shopController.ShowBuyer();
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float lrswitch = Input.GetAxis("Switch Game Menu");

		if (sellerSide) {
			if (lrswitch > 0) {
				shopController.ShowSeller();
			}
		} else {
			if (lrswitch < 0) {
				shopController.ShowBuyer();
			}
		}
	}
}
