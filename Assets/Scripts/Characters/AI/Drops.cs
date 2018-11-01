using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {
    public int gold;
    public float exp;
    public List<ItemDrop> DropList;
    public GameObject goldItem;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<CharacterStats>().OnDeath += Drop;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Drop()
    {
        Quaternion temp = Quaternion.EulerRotation(-90,0,0);
        GameObject goldTemp = (GameObject) Instantiate(goldItem, transform.position, temp);
        goldTemp.GetComponent<Gold>().gold = gold;

        PlayerManager.instance.localPlayer.GetComponent<BaseCharacter>().characterStats.GainExp(exp);
    }
}
