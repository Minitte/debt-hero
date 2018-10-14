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
	public CharacterInventory inventory;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
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
		goldText.text = inventory.gold + "g";
	}
}
