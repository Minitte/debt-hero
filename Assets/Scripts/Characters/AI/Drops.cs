using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {
    public int gold;
    public float exp;
    public List<GameObject> DropList;
    public GameObject goldItem;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<CharacterStats>().OnDeath += Drop;
    }

    public void Drop()
    {
        //Quaternion temp = goldItem.transform.rotation;
        Transform tempTrans = transform;
        tempTrans.position = new Vector3(transform.position.x,transform.position.y+5, transform.position.z);
        GameObject goldTemp = Instantiate(goldItem, tempTrans.position, goldItem.transform.rotation);
        goldTemp.GetComponent<Gold>().gold = gold;
        DropForce(goldTemp);

        for(int i=0; i<DropList.Count; i++) {
            GameObject tempDrop = Instantiate(DropList[i], tempTrans.position, DropList[i].transform.rotation);
            DropForce(tempDrop);
        }

        PlayerManager.instance.localPlayer.GetComponent<BaseCharacter>().characterStats.GainExp(exp);
    }

    private void DropForce(GameObject drop) {
        drop.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0, 360), transform.eulerAngles.z);
        float speed = 100;
        Vector3 force = transform.forward;
        force = new Vector3(force.x, 1, force.z);
        drop.GetComponent<Rigidbody>().AddForce(force * speed);
    }
}
