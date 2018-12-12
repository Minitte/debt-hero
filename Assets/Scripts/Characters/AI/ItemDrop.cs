using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

    /// <summary>
    /// Prefab of the item to drop
    /// </summary>
	public ItemBase itemPrefabToDrop;

    /// <summary>
    /// The amount of the item to drop
    /// </summary>
    public int qty;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PickupRadius") {

            ItemBase newItem = GameObject.Instantiate(itemPrefabToDrop).GetComponent<ItemBase>();

            newItem.properties.quantity = qty;

            PlayerManager.instance.GetComponent<CharacterInventory>().AddItem(newItem);
            DestroyObject(gameObject);
        }
    }
}
