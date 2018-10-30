using System;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InventoryPanel : MonoBehaviour {

	/// <summary>
	/// Text showing the gold amount
	/// </summary>
	public TextMeshProUGUI goldText;

	/// <summary>
	/// Rows of item ui slots
	/// </summary>
	public ItemRowUI[] itemRows;

	/// <summary>
	/// array of item uis
	/// </summary>
	private ItemUI[,] _items;

	/// <summary>
	/// target inventory to show
	/// </summary>
	private CharacterInventory _inventory;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_inventory = PlayerManager.instance.GetComponent<CharacterInventory>();

		_items = new ItemUI[itemRows.Length, itemRows[0].items.Length];

		_inventory.OnItemAdded += AddItemToDisplay;
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		UpdateGoldDisplay();
		UpdateInventorySlots();
	}

	/// <summary>
	/// Updatse the inventory panel
	/// </summary>
	public void UpdateGoldDisplay() {	
		if (_inventory != null) {	
			goldText.text = _inventory.gold + "g";
		}
	}

	public void UpdateInventorySlots() {
		if (_inventory == null) {
			return;
		}

		for (int row = 0; row < itemRows.Length; row++) {
			for (int col = 0; col < itemRows[0].items.Length; col++) {

				ItemSlot slot = new ItemSlot(col, row);

				ItemBase item = _inventory.GetItem(slot);
				
				if (item != null) {
					AddItemToDisplay(slot);
				}
			}
		}
	}

	/// <summary>
	/// Adds an item from inventory in the same slot to the display grid
	/// </summary>
	/// <param name="slot"></param>
	private void AddItemToDisplay(ItemSlot slot) {
		int id = _inventory.GetItem(slot).properties.itemID;

		ItemUI itemUI = _items[slot.row, slot.col];

		// destory current ui item if any
		if (itemUI != null) {
			Destroy(itemUI.gameObject);
		}

		itemUI = GameDatabase.instance.itemDatabase.GetNewItemUI(id);

		itemUI.transform.SetParent(GetGridSlot(slot).transform, false);

		_items[slot.row, slot.col] = itemUI;
	}

	private GameObject GetGridSlot(ItemSlot slot) {
		return itemRows[slot.row].items[slot.col];
	}

}

/// <summary>
/// Class used to get 2d array of ItemBases to appear in the unity inspector
/// </summary>
[Serializable]
public class ItemRowUI {
	public GameObject[] items;
}
