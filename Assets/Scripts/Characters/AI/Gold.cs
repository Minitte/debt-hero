using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int gold;

    private void Start()
    {
        
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