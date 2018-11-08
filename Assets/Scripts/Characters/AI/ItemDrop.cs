using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PickupRadius") {

            ItemBase item = GetComponent<ItemBase>();

            ItemBase newItem = GameDatabase.instance.GetComponent<ItemDatabase>().GetNewItem(item.properties.itemID, 1);

            PlayerManager.instance.GetComponent<CharacterInventory>().AddItem(newItem);
            DestroyObject(gameObject);
        }
    }
}
