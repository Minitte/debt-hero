using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

	/// <summary>
	/// buyer panel
	/// </summary>
	public InventoryPanel buyer; 

	/// <summary>
	/// seller panel
	/// </summary>
	public InventoryPanel seller;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float closeShop = Input.GetAxis("System Menu Open");
		
		if (closeShop != 0) {
			GameState.SetState(GameState.PLAYING);
			this.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		GameState.SetState(GameState.MENU_SHOP);
		buyer.enabled = false;
		seller.enabled = true;
	}
}
