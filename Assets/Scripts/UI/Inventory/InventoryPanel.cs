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

	/// <summary>
	/// Area for big item icon
	/// </summary>
	public Transform itemIconArea;

	/// <summary>
	/// Item value/worth in gold
	/// </summary>
	public TextMeshProUGUI itemWorthText;

	[Header("Item Slots")]

	/// <summary>
	/// Rows of item ui slots
	/// </summary>
	public ItemRowUI[] itemRows;

	[Header("Inventory Panel Settings")]

	/// <summary>
	/// Delay between slots
	/// </summary>
	public float slotDelay = 0.1f;

	/// <summary>
	/// array of item uis
	/// </summary>
	private ItemUI[,] _items;

	/// <summary>
	/// target inventory to show
	/// </summary>
	protected CharacterInventory _inventory;

	/// <summary>
	/// Currente selected item slot for moving items
	/// </summary>
	private ItemSlot _currentMoveSlot;

	/// <summary>
	/// mouse item icon
	/// </summary>
	private GameObject _mouseItemIcon;

	/// <summary>
	/// Item details icon
	/// </summary>
	private GameObject _itemDetailIcon;

	/// <summary>
	/// current hovered or selected slot 
	/// </summary>
	protected ItemSlot _currentSelectSlot;

	/// <summary>
	/// Time for cool down
	/// </summary>
	private float _cooldownTime;

	/// <summary>
	/// cool down flag
	/// </summary>
	private bool _onCooldown;

	/// <summary>
	/// Flag for moving items
	/// </summary>
	private bool _moving;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	public void Start() {
		if (_inventory == null) {
			_inventory = PlayerManager.instance.GetComponent<CharacterInventory>();
		}

		_items = new ItemUI[itemRows.Length, itemRows[0].items.Length];

		_inventory.OnItemAdded += UpdateItemSlot;

		// assign slot references
		for (int row = 0; row < itemRows.Length; row++) {
			for(int col = 0; col < itemRows[0].items.Length; col++) {
				ItemSlot slot = new ItemSlot(row, col);

				ItemGridItemUI igiu = GetGridSlot(slot);

				// assign slot
				igiu.slot = slot;

				// setup event listeners
				igiu.OnLeftClick += SelectSlot;

				igiu.OnRightClick += UseSlot;

				igiu.OnHoverOver += ShowItemDetails;

				igiu.OnHoverOff += ResetItemDetails;
			}
		}

		UpdateAllItemSlots();

		ResetItemDetails(null);

		_currentSelectSlot = new ItemSlot(0, 0);

		if (itemWorthText == null) {
			Debug.Log("itemWorthText is missing/null! Updates will ignore itemWorthText.");
		}
	}
	
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if (_mouseItemIcon != null) {
			_mouseItemIcon.transform.position = Input.mousePosition;
		}

		if (_onCooldown) {
			_cooldownTime += Time.deltaTime;

			if (_cooldownTime >= slotDelay) {
				_onCooldown = false;
				_cooldownTime = 0;
			}

			return;
		}

		NavControls();
	}

	/// <summary>
	/// Navigation Key controls for inventory
	/// </summary>
	private void NavControls() {
		// input
		float vert = Input.GetAxis("Menu Vertical");
		float horz = Input.GetAxis("Menu Horizontal");

		ResetItemDetails();

		GetGridSlot(_currentSelectSlot).SetBorderVisiblity(false);

		// row
		if (vert != 0) {
			int rowAfter = vert == 0 ? 0 : (vert > 0 ? -1 : 1);
			rowAfter += _currentSelectSlot.row;

			if (rowAfter >= 0 && rowAfter < itemRows.Length) {
				_currentSelectSlot.row = rowAfter;
				_onCooldown = true;
			}
		}

		// col
		if (horz != 0) {
			int colAfter = horz == 0 ? 0 : (horz > 0 ? 1 : -1);
			colAfter += _currentSelectSlot.col;

			if (colAfter >= 0 && colAfter < itemRows[0].items.Length) {
				_currentSelectSlot.col = colAfter;
				_onCooldown = true;
			}
		}

		ShowItemDetails(_currentSelectSlot);

		GetGridSlot(_currentSelectSlot).SetBorderVisiblity(true);

		ActionControls();
	}

	/// <summary>
	/// Inventory Action keys
	/// </summary>
	private void ActionControls() {
		// input
		float use = Input.GetAxis("Inventory Use");
		float move = Input.GetAxis("Inventory Move");

		if (use != 0) {
			UseSlot(_currentSelectSlot);
			_onCooldown = true;
		}

		if (_onCooldown) {
			return;
		}

		if (move != 0) {
			SelectSlot(_currentSelectSlot);
			_onCooldown = true;
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {	
		if (_inventory == null) {
			return;
		}

		UpdateGoldText();
		UpdateAllItemSlots();

		_currentSelectSlot = new ItemSlot(0, 0);
	
		ResetItemDetails(null);
		ShowItemDetails(_currentSelectSlot);
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		_currentMoveSlot = null;

		Destroy(_mouseItemIcon);

		_mouseItemIcon = null;

		ResetItemDetails(null);

		HideAllBorders();
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
	/// hides all borders
	/// </summary>
	private void HideAllBorders() {
		for (int row = 0; row < itemRows.Length; row++) {
			for (int col = 0; col < itemRows[0].items.Length; col++) {

				ItemSlot slot = new ItemSlot(row, col);

				ItemGridItemUI igiu = GetGridSlot(slot);

				igiu.SetBorderEquip(false);
				igiu.SetBorderVisiblity(false);
				igiu.SetBorderFlash(false);
			}
		}
	}

	/// <summary>
	/// Uses the item in the slot
	/// </summary>
	/// <param name="slot"></param>
	protected virtual void UseSlot(ItemSlot slot) {
		ItemBase item = _inventory.GetItem(slot);

		if (item != null) {
			// ensure the player is set as the owner
			item.owner = PlayerManager.instance.localPlayer.GetComponent<BaseCharacter>();
			
			item.Use();
		}

		// UpdateItemSlot(slot);
		UpdateAllItemSlots();
	}

	/// <summary>
	/// Shows the item details in the slot if any
	/// </summary>
	/// <param name="slot"></param>
	protected virtual void ShowItemDetails(ItemSlot slot) {
		ItemBase item = _inventory.GetItem(slot);

		GetGridSlot(_currentSelectSlot).SetBorderVisiblity(false);
		GetGridSlot(slot).SetBorderVisiblity(true);

		_currentSelectSlot = slot;

		ResetItemDetails();

		// display details
		if (item != null) {
			itemNameText.text = item.properties.name;
			itemDescText.text = item.properties.description;
			itemQtyText.text = "x " + item.properties.quantity;

			if (itemWorthText != null) {
				itemWorthText.text = item.properties.value + "g";
			}

			ItemUI iUI = Instantiate(item.itemUIPrefab).GetComponent<ItemUI>();

			_itemDetailIcon = iUI.icon.gameObject;

			_itemDetailIcon.transform.SetParent(itemIconArea, false);

			Destroy(iUI.gameObject);
		}
	}

	/// <summary>
	/// Resets the item details area to blank
	/// </summary>
	/// <param name="slot">Unused and optional. Satifies delegate template</param>
	protected void ResetItemDetails(ItemSlot slot = null) {
		itemNameText.text = "";
		itemDescText.text = "";
		itemQtyText.text = "";

		if (itemWorthText) {
			itemWorthText.text = "";
		}

		if (_itemDetailIcon != null) {
			Destroy(_itemDetailIcon);
			_itemDetailIcon = null;
		}

		if (slot != null) {
			if (_currentMoveSlot == null || !_currentMoveSlot.Equals(slot)) {
				GetGridSlot(slot).SetBorderVisiblity(false);
			}
		}
	}

	/// <summary>
	/// Selects the item slot for action
	/// </summary>
	/// <param name="slot"></param>
	protected virtual void SelectSlot(ItemSlot slot) {
		// begin item movement
		if (!_moving) {
			ItemBase item = _inventory.GetItem(slot);

			// Drag icon and stuff if there is actually an item
			if (item != null) {
				_moving = true;
				
				_currentMoveSlot = new ItemSlot(slot);

				ItemUI iUI = Instantiate(item.itemUIPrefab).GetComponent<ItemUI>();

				_mouseItemIcon = iUI.icon.gameObject;

				_mouseItemIcon.transform.SetParent(GameObject.Find("Canvas").transform, false);

				RectTransform rt = _mouseItemIcon.GetComponent<RectTransform>();

				rt.sizeDelta = new Vector2(100, 100);

				rt.anchorMin = new Vector2(0.5f, 0.5f);
				rt.anchorMax = new Vector2(0.5f, 0.5f);

				Destroy(iUI.gameObject);

				GetGridSlot(_currentMoveSlot).SetBorderFlash(true);
			}
		}

		// end item movement
		else if (_moving) {
			_inventory.SwapItems(_currentMoveSlot, slot);

			UpdateAllItemSlots();

			GetGridSlot(_currentMoveSlot).SetBorderFlash(false);
			GetGridSlot(slot).SetBorderFlash(false);

			_moving = false;
			_currentMoveSlot = null;

			Destroy(_mouseItemIcon);

			_mouseItemIcon = null;
		}
	}

	/// <summary>
	/// Updates gold text
	/// </summary>
	public void UpdateGoldText() {
		goldText.text = _inventory.gold + "g";
	}

	/// <summary>
	/// Adds an item from inventory in the same slot to the display grid
	/// </summary>
	/// <param name="slot"></param>
	public void UpdateItemSlot(ItemSlot slot) {
		ItemBase item = _inventory.GetItem(slot);

		ItemUI itemUI = _items[slot.row, slot.col];

		if (item == null) {
			GetGridSlot(slot).SetBorderVisiblity(false);
			GetGridSlot(slot).SetBorderEquip(false);
		}

		// no item in slot but ui exist... remove ui
		if (itemUI != null) {
			Destroy(itemUI.gameObject);
			itemUI = null;
		}

		// item exist in slot but no ui... create ui
		if (item != null && itemUI == null) {
			itemUI = Instantiate(item.itemUIPrefab).GetComponent<ItemUI>();

			itemUI.transform.SetParent(GetGridSlot(slot).transform, false);

			_items[slot.row, slot.col] = itemUI;

			itemUI.stackText.text = item.properties.quantity + "";
		}

		// special case for equipment
		if (item != null && item.properties.IsTypeEquipment()) {
			ItemEquipment equip = (ItemEquipment)item;

			GetGridSlot(slot).SetBorderEquip(equip.equiped);
		}
	}

	protected ItemGridItemUI GetGridSlot(ItemSlot slot) {
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
