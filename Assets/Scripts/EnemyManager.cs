﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    /// <summary>
    /// Prefab of the floor's enemies.
    /// </summary>
    public GameObject[] enemyList;

    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        FloorGenerator.OnEnemySpawn += SpawnEnemies;
    }

    void SpawnEnemies(Floor currentFloor, System.Random rand) {
        Debug.Log("Event Fired");
        int roomSize = currentFloor.roomList.Count;
        
        Vector3 roomyPos = currentFloor.roomList[rand.Next(0, roomSize)].transform.position;
        Vector3 enemyPos = new Vector3(roomyPos.x, roomyPos.y + .85f, roomyPos.z);
        Instantiate(enemyList[0].gameObject, enemyPos, Quaternion.identity);
    }

}
