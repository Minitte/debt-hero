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
	/// Currente selected item slot
	/// </summary>
	private ItemSlot _currentSlot;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_inventory = PlayerManager.instance.GetComponent<CharacterInventory>();

		_items = new ItemUI[itemRows.Length, itemRows[0].items.Length];

		_inventory.OnItemAdded += UpdateItemSlot;

		ItemGridItemUI.OnLeftClick += SelectSlot;

		ItemGridItemUI.OnRightClick += UseSlot;

		// assign slot references

		for (int row = 0; row < itemRows.Length; row++) {
			for(int col = 0; col < itemRows[0].items.Length; col++) {
				ItemSlot slot = new ItemSlot(row, col);
				GetGridSlot(slot).slot = slot;
			}
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {	
		if (_inventory == null) {
			return;
		}

		goldText.text = _inventory.gold + "g";
		UpdateAllItemSlots();
	}

	/// <summary>
	/// Updates all item slots
	/// </summary>
	public void UpdateAllItemSlots() {
		for (int row = 0; row < itemRows.Length; row++) {
			for (int col = 0; col < itemRows[0].items.Length; col++) {

				ItemSlot slot = new ItemSlot(row, col);

				UpdateItemSlot(slot);
			}
		}
	}

	/// <summary>
	/// Uses the item in the slot
	/// </summary>
	/// <param name="slot"></param>
	private void UseSlot(ItemSlot slot) {
		ItemBase item = _inventory.GetItem(slot);

		if (item != null) {
			item.Use();
		}

		UpdateItemSlot(slot);
	}

	/// <summary>
	/// Selects the item slot for action
	/// </summary>
	/// <param name="slot"></param>
	private void SelectSlot(ItemSlot slot) {
		_currentSlot = slot;
	}

	/// <summary>
	/// Adds an item from inventory in the same slot to the display grid
	/// </summary>
	/// <param name="slot"></param>
	private void UpdateItemSlot(ItemSlot slot) {
		ItemBase item = _inventory.GetItem(slot);

		ItemUI itemUI = _items[slot.row, slot.col];

		// no item in slot but ui exist... remove ui
		if (item == null && itemUI != null) {
			Destroy(itemUI.gameObject);
		}

		// item exist in slot but no ui... create ui
		else if (item != null && itemUI == null) {
			itemUI = GameDatabase.instance.itemDatabase.GetNewItemUI(item.properties.itemID);

			itemUI.transform.SetParent(GetGridSlot(slot).transform, false);

			_items[slot.row, slot.col] = itemUI;

			itemUI.stackText.text = item.properties.quantity + "";
		}

		// item and ui exist... update ui
		else if (item != null && itemUI != null) {
			itemUI.stackText.text = item.properties.quantity + "";
		}
	}

	private ItemGridItemUI GetGridSlot(ItemSlot slot) {
		return itemRows[slot.row].items[slot.col];
	}

}

/// <summary>
/// Class used to get 2d array of ItemBases to appear in the unity inspector
/// </summary>
[Serializable]
public class ItemRowUI {
	public ItemGridItemUI[] items;
}
