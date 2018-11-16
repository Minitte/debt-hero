using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	/// <summary>
	/// Inital list of item entries
	/// </summary>
	public ItemBase[] initalEntries;

	/// <summary>
	/// Item dictionary for fast look up
	/// </summary>
	private Dictionary<int, ItemBase> _itemDict;

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

		ItemBase item = Instantiate(_itemDict[id].gameObject).GetComponent<ItemBase>();

        item.properties.quantity = qty;

		return item;	
	}

	/// <summary>
	/// Initalizes the dictionary database
	/// </summary>
	private void InitalizeDictionary() {
		_itemDict = new Dictionary<int, ItemBase>();

		foreach (ItemBase entry in initalEntries) {
			ItemProperties props = entry.properties;

			// test for duplicates
			Debug.Assert(!_itemDict.ContainsKey(props.itemID), 
				"Item database found two matching ids! Each item must have a unique id! " + entry.gameObject.name);
		
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
}
