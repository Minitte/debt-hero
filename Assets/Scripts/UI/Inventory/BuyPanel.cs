using UnityEngine;

public class BuyPanel : InventoryPanel {
    
    [Header("Buy panel parameters")]

    public CharacterInventory sellerInventory;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    new void Start() {
        _inventory = sellerInventory;

        base.Start();
    }

    /// <summary>
    /// Override of UseSlot.
    /// 
    /// Sells 1 unit of the item for gold
    /// </summary>
    /// <param name="slot"></param>
    protected override void UseSlot(ItemSlot slot) {
        ItemBase item = _inventory.GetItem(slot);

        // check if any item is in the slot    
        if (item == null) {
            return;
        }

        int cost = item.properties.value * 2;

        // check gold
        if (_inventory.gold < cost) {
            return;
        }

        // deduct cost from player
        _inventory.gold -= cost;

        // create item
        ItemDatabase itemDB = GameDatabase.instance.GetComponent<ItemDatabase>();
        ItemBase boughtItem = itemDB.GetNewItem(item.properties.itemID, 1);

        // give item to player
        PlayerManager.instance.GetComponent<CharacterInventory>().AddItem(boughtItem);

        // update gold display
        UpdateGoldText();
    } 

    /// <summary>
    /// Override of SelectSlot
    /// 
    /// Overrides the functionality to nothing to disable moving items
    /// </summary>
    /// <param name="slot"></param>
    protected override void SelectSlot(ItemSlot slot) {
        // do nothing
    }

    /// <summary>
	/// Shows the item details in the slot if any
	/// </summary>
	/// <param name="slot"></param>
	protected override void ShowItemDetails(ItemSlot slot) {
        base.ShowItemDetails(slot);

		ItemBase item = _inventory.GetItem(slot);

		// display details
		if (item != null) {
			if (itemWorthText != null) {
				itemWorthText.text = item.properties.value * 2 + "g";
			}
		}
	}
}