using System;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {

    #region Events and Delegates

    /// <summary>
    /// delegate of a simple itemevent
    /// </summary>
    public delegate void SimpleItemEvent();

    /// <summary>
    /// delegate that requires a refernece of the current item 
    /// </summary>
    /// <param name="item">Current item</param>
    public delegate void ReferenceItemEvent(ItemBase item);

    /// <summary>
    /// Event triggered when this item is used
    /// </summary>
    public event SimpleItemEvent OnUse;

    /// <summary>
    /// Event triggered when this item is about to be destoryed by disposal or zero qualitity
    /// </summary>
    public event ReferenceItemEvent OnDisposal;

    #endregion

    /// <summary>
    /// properties of the item
    /// </summary>
	public ItemProperties properties;

    /// <summary>
    /// Item UI Prefab
    /// </summary>
    public ItemUI itemUIPrefab;

    /// <summary>
    /// Owner of the item
    /// </summary>
    public BaseCharacter owner;

    /// <summary>
    /// Uses the item aslong the quantity is above 0 and is usable
    /// </summary>
    public void Use() {
        // check if usable
        if (!properties.usable) {
            return;
        }

        properties.quantity--;

        if (OnUse != null) {
            OnUse();
        }

        // destory item if qualtity is <= 0
        if (properties.destoryOnNone && properties.quantity <= 0) {
            if (OnDisposal != null) {
                OnDisposal(this);
            }
            
            Destroy(this.gameObject);
        }
    }

    // override object.Equals
    public override bool Equals(object obj) {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //
        
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }
        
        ItemBase other = (ItemBase) obj;
        
        return this.GetHashCode() == other.GetHashCode();
    }

    // override object.GetHashCode
    public override int GetHashCode() {
        string s =  properties.name + "/" + 
            properties.itemID + "/" + 
            properties.type;

        return s.GetHashCode();
    }
}
