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
    /// Owner of the item
    /// </summary>
    public BaseCharacter owner;

    /// <summary>
    /// Uses the item aslong the quantity is above 0 and is usable
    /// </summary>
    public void Use() {
        // check qty
        if (properties.quantity <= 0 || properties.usable || owner == null) {
            return;
        }

        properties.quantity--;

        if (OnUse != null) {
            OnUse();
        }

        // destory item if qualtity is <= 0
        if (properties.destoryOnNone && properties.quantity <= 0) {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy() {

        // trigger event for listener
        if (OnDisposal != null) {
            OnDisposal(this);
        }
    }
}
