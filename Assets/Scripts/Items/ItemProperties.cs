using System;

[Serializable]
public class ItemProperties {
	
    public enum Type {
        MISC,
        CONSUMABLE,
        EQUIPMENT,
        QUEST
    }

    /// <summary>
    /// Item unique identifier
    /// </summary>
    public int itemID;

    /// <summary>
    /// Item name
    /// </summary>
    public string name = "ITEM_NAME";

    /// <summary>
    /// Description of the item
    /// </summary>
    public string description = "ITEM_DESCRIPTION";

    /// <summary>
    /// Type of item
    /// </summary>
    public Type type;

    /// <summary>
    /// boolean if item is stackable
    /// </summary>
    public bool stackable = true;

    /// <summary>
    /// Destory item when quantity = 0
    /// </summary>
    public bool destoryOnNone = true;

    /// <summary>
    /// if the item can be discard
    /// </summary>
    public bool disposable = true;

    /// <summary>
    /// if the item can be used
    /// </summary>
    public bool usable;

    /// <summary>
    /// item's gold value
    /// </summary>
    public int value;

    /// <summary>
    /// Item quantity
    /// </summary>
    public int quantity = 1;
}