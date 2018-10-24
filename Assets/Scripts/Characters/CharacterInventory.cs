using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {

	#region Events and Delegates
	
	public delegate void SingleSlotEvent(ItemSlot slot);

	public delegate void DoubleSlotEvent(ItemSlot slotA, ItemSlot slotB);

	public event SingleSlotEvent OnItemRemoved;

	public event SingleSlotEvent OnItemAdded;

	public event DoubleSlotEvent OnItemsSwapped;

	#endregion

	/// <summary>
	/// Rows of items
	/// </summary>
	public ItemRow[] itemRows;

	/// <summary>
	/// Amount of gold this character has
	/// </summary>
	public int gold;

	/// <summary>
	/// Dictionary/Map containing vectors xy for fast look ups
	/// </summary>
	private Dictionary<ItemBase, ItemSlot> _itemSlotsMap;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		_itemSlotsMap = new Dictionary<ItemBase, ItemSlot>();
		AddExistingToDictionary();
	}

	/// <summary>
	/// Adds the inital items found in itemRows to the dictionary
	/// </summary>
	private void AddExistingToDictionary() {
		for (int row = 0; row < itemRows.Length; row++) {
			for (int col = 0; col < itemRows[0].items.Length; col++) {
				ItemBase item = itemRows[row].items[col];

				if (item != null) {
					_itemSlotsMap.Add(item, new ItemSlot(row, col));
				}
			}
		}
	}

	/// <summary>
	/// Gets the item from the slot
	/// </summary>
	/// <param name="slot"></param>
	/// <returns></returns>
	public ItemBase GetItem(ItemSlot slot) {
		return GetItem(slot.row, slot.col);
	}

	/// <summary>
	/// Gets the item from the slot
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <returns></returns>
	public ItemBase GetItem(int row, int col) {
		return itemRows[row].items[col];	
	}

	/// <summary>
	/// Removes the item at the slot
	/// </summary>
	/// <param name="slot"></param>
	/// <returns>null if removal was unsuccessful</returns>
	public ItemBase RemoveItemAt(ItemSlot slot) {
		return RemoveItemAt(slot.row, slot.col);
	}

	/// <summary>
	/// Removes the item at the slot
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <returns>null if removal was unsuccessful</returns>
	public ItemBase RemoveItemAt(int row, int col) {
		// check row
		if (row >= itemRows.Length || row < 0) {
			return null;
		}

		// check col
		if (col >= itemRows[row].items.Length || col < 0) {
			return null;
		}

		ItemBase item = GetItem(row, col);

		// no item to remove
		if (item == null) {
			return null;
		}

		// remove owner reference
		item.owner = null;

		// remove item reference
		itemRows[row].items[col] = null;

		// remove from map
		_itemSlotsMap.Remove(item);

		if (OnItemRemoved != null) {
			OnItemRemoved(new ItemSlot(row, col));
		}

		return item;
	}

	/// <summary>
	/// Removes the item from the inventory
	/// </summary>
	/// <param name="itemToRemove">desired item to be removed</param>
	/// <returns>If removal was successful</returns>
	public bool RemoveItem(ItemBase itemToRemove) {
		ItemSlot slot = _itemSlotsMap[itemToRemove];

		// did not find the item
		if (slot == null) {
			return false;
		}

		// remove owner reference
		itemToRemove.owner = null;

		// remove item reference
		itemRows[slot.row].items[slot.col] = null;

		// remove from map
		_itemSlotsMap.Remove(itemToRemove);

		if (OnItemRemoved != null) {
			OnItemRemoved(slot);
		}

		return true;
	}

	/// <summary>
	/// Adds the item in the first open inventory slot
	/// </summary>
	/// <param name="itemToAdd"></param>
	/// <returns></returns>
	public bool AddItem(ItemBase itemToAdd) {
		ItemSlot slot = FindEmptySlot();

		if (slot == null) {
			return false;
		}

		// determine the owner
		BaseCharacter chara = GetComponent<BaseCharacter>();

		if (chara == null) {
			PlayerManager pm = GetComponent<PlayerManager>();

			if (pm == null) {
				Debug.Log("Inventory on " + this.gameObject.name + " didn't have BaseCharacter or PlayerManager");

				return false;
			} else {
				chara = pm.localPlayer.GetComponent<BaseCharacter>();
			}
		}

		itemToAdd.owner = chara;

		itemRows[slot.row].items[slot.col] = itemToAdd;

		_itemSlotsMap.Add(itemToAdd, slot);

		if (OnItemAdded != null) {
			OnItemAdded(slot);
		}

		return true;
	}

	/// <summary>
	/// Swaps the two items in the two positions
	/// </summary>
	/// <param name="slotA"></param>
	/// <param name="slotB"></param>
	public void SwapItems(ItemSlot slotA, ItemSlot slotB) {
		ItemBase itemA = GetItem(slotA);
		ItemBase itemB = GetItem(slotB);

		// update map
		if (itemA != null) {
			_itemSlotsMap[itemA] = slotB;
		}

		if (itemB != null) {
			_itemSlotsMap[itemB] = slotA;
		}

		// update row/col
		itemRows[slotA.row].items[slotA.col] = itemB;
		itemRows[slotB.row].items[slotB.col] = itemA;

		if (OnItemsSwapped != null) {
			OnItemsSwapped(slotA, slotB);
		}
	}

	/// <summary>
	/// Finds the first empty slot
	/// </summary>
	/// <returns>a null if no slots was found</returns>
	public ItemSlot FindEmptySlot() {
		for (int row = 0; row < itemRows.Length; row++) {
			for (int col = 0; col < itemRows[0].items.Length; col++) {
				if (itemRows[row].items[col] == null) {
					return new ItemSlot(row, col);
				}
			}
		}

		return null;
	}
}

/// <summary>
/// Class used to get 2d array of ItemBases to appear in the unity inspector
/// </summary>
[Serializable]
public class ItemRow {
	public ItemBase[] items;
}