using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	/// <summary>
	/// Inital list of item entries
	/// </summary>
	public ItemEntry[] initalEntries;

	/// <summary>
	/// Item dictionary for fast look up
	/// </summary>
	private Dictionary<int, ItemEntry> _itemDict;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		InitalizeDictionary();	
	}

	/// <summary>
	/// Returns a new instance of an item with the matching id
	/// </summary>
	/// <param name="id"></param>
	/// <param name="qty"></param>
	/// <returns></returns>
	public ItemBase GetNewItem(int id, int qty) {
		Debug.Assert(_itemDict.ContainsKey(id), "Item database does not contain an item with an id of " + id);

		ItemBase item = Instantiate(_itemDict[id].itemPrefab.gameObject).GetComponent<ItemBase>();

		return item;	
	}

	/// <summary>
	/// returns a new instances 
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public ItemUI GetNewItemUI(int id) {
		Debug.Assert(_itemDict.ContainsKey(id), "Item database does not contain an item with an id of " + id);

		return Instantiate(_itemDict[id].itemUIPrefab).GetComponent<ItemUI>();
	}

	/// <summary>
	/// Initalizes the dictionary database
	/// </summary>
	private void InitalizeDictionary() {
		_itemDict = new Dictionary<int, ItemEntry>();

		foreach (ItemEntry entry in initalEntries) {
			ItemProperties props = entry.itemPrefab.properties;

			// test for duplicates
			Debug.Assert(!_itemDict.ContainsKey(props.itemID), 
				"Item database found two matching ids! Each item must have a unique id! " + entry.itemPrefab.gameObject.name);
		
			_itemDict.Add(props.itemID, entry);
		}
	}
}

[Serializable]
public class ItemEntry {

	/// <summary>
	/// item prefab
	/// </summary>
	public ItemBase itemPrefab;

	/// <summary>
	/// Item display UI prefab
	/// </summary>
	public ItemUI itemUIPrefab;
}
