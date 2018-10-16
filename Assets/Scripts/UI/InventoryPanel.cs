using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InventoryPanel : MonoBehaviour {

	/// <summary>
	/// Text showing the gold amount
	/// </summary>
	public TextMeshProUGUI goldText;

	/// <summary>
	/// target inventory to show
	/// </summary>
	private CharacterInventory _inventory;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		UpdateInventoryDisplay();
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		UpdateInventoryDisplay();
	}

	/// <summary>
	/// Updatse the inventory panel
	/// </summary>
	public void UpdateInventoryDisplay() {
		if (_inventory == null) {
			_inventory = PlayerManager.instance.GetComponent<CharacterInventory>();
		}
		
		goldText.text = _inventory.gold + "g";
	}
}
