using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int gold;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (this.transform.position.y < -50) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickupRadius")
        {
            PlayerManager.instance.GetComponent<CharacterInventory>().gold += gold;
            DestroyObject(gameObject);
        }
    }

}