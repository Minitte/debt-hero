using UnityEngine;

public class SellPanel : InventoryPanel {
    
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

        // decrement qty of item
        item.properties.quantity--;

        // grant player gold based on value
        _inventory.gold += item.properties.value;

        // desotry if zero qty of item
        if (item.properties.quantity <= 0) {
            _inventory.RemoveItemAt(slot);
            Destroy(item.gameObject);

            // remove the item info
            ResetItemDetails();
        } else {
            // update item info
            ShowItemDetails(slot);
        }

        // update slot
        UpdateItemSlot(slot);

        // update gold
        UpdateGoldText();
    } 
}