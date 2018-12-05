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
	/// Shop UI animator
	/// </summary>
	private Animator _animator;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		_animator = GetComponent<Animator>();
	}

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

	/// <summary>
	/// Shows the sellers side
	/// </summary>
	public void ShowSeller() {
		buyer.enabled = false;
		seller.enabled = true;

		_animator.SetBool("Seller", true);
	}

	/// <summary>
	/// Shows the buyer side
	/// </summary>
	public void ShowBuyer() {
		buyer.enabled = true;
		seller.enabled = false;

		_animator.SetBool("Seller", false);
	}
}
