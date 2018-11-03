using System;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InventoryPanel : MonoBehaviour {

	[Header("Text Displays")]

	/// <summary>
	/// Text showing the gold amount
	/// </summary>
	public TextMeshProUGUI goldText;

	[Header("Item Detail Text")]

	/// <summary>
	/// Text showing the item name
	/// </summary>
	public TextMeshProUGUI itemNameText;

	/// <summary>
	/// Text showing the item desc
	/// </summary>
	public TextMeshProUGUI itemDescText;

	/// <summary>
	/// Text showing the item quantity
	/// </summary>
	public TextMeshProUGUI itemQtyText;

	[Header("Items")]

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
	/// mouse item icon
	/// </summary>
	private GameObject _mouseItemIcon;

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

		ItemGridItemUI.OnHoverOver += ShowItemDetails;

		// assign slot references

		for (int row = 0; row < itemRows.Length; row++) {
			for(int col = 0; col < itemRows[0].items.Length; col++) {
				ItemSlot slot = new ItemSlot(row, col);
				GetGridSlot(slot).slot = slot;
			}
		}

		UpdateAllItemSlots();

		itemNameText.text = "";
		itemDescText.text = "";
	}
	
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if (_mouseItemIcon != null) {
			_mouseItemIcon.transform.position = Input.mousePosition;
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
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		_currentSlot = null;

		Destroy(_mouseItemIcon);

		_mouseItemIcon = null;
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
	/// Shows the item details in the slot if any
	/// </summary>
	/// <param name="slot"></param>
	private void ShowItemDetails(ItemSlot slot) {
		ItemBase item = _inventory.GetItem(slot);

		// display details
		if (item != null) {
			itemNameText.text = item.properties.name;
			itemDescText.text = item.properties.description;
			itemQtyText.text = "x " + item.properties.quantity;
		} else {
			itemNameText.text = "";
			itemDescText.text = "";
			itemQtyText.text = "";
		}
	}

	/// <summary>
	/// Selects the item slot for action
	/// </summary>
	/// <param name="slot"></param>
	private void SelectSlot(ItemSlot slot) {
		// begin item movement
		if (_currentSlot == null) {
			ItemBase item = _inventory.GetItem(slot);

			// Drag icon and stuff if there is actually an item
			if (item != null) {
				_currentSlot = slot;

				ItemUI iUI = GameDatabase.instance.itemDatabase.GetNewItemUI(item.properties.itemID);

				_mouseItemIcon = iUI.icon.gameObject;

				_mouseItemIcon.transform.SetParent(GameObject.Find("Canvas").transform, false);

				RectTransform rt = _mouseItemIcon.GetComponent<RectTransform>();

				rt.sizeDelta = new Vector2(100, 100);

				rt.anchorMin = new Vector2(0.5f, 0.5f);
				rt.anchorMax = new Vector2(0.5f, 0.5f);

				Destroy(iUI.gameObject);
			}
		}

		// end item movement
		else if (_currentSlot != null) {
			_inventory.SwapItems(_currentSlot, slot);

			UpdateItemSlot(_currentSlot);
			UpdateItemSlot(slot);

			_currentSlot = null;

			Destroy(_mouseItemIcon);

			_mouseItemIcon = null;
		}
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
