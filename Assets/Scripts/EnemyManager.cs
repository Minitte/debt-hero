using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    /// <summary>
    /// Prefab of the floor's enemies.
    /// </summary>
    public GameObject[] enemyList;

    

    // Use this for initialization
    void Start () {
        FloorGenerator.OnFloorGenerated += SpawnEnemies;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    /// <summary>
    /// Spawn Enemies in a room.
    /// </summary>
    /// <param name="currentFloor">A floor that contains a room</param>
    /// <param name="rand"> Random used to determine which room to use.</param>
    void SpawnEnemies(Floor currentFloor, System.Random rand) {
        
        for (int i = 0; i <= rand.Next(5, 10); i++) {
            int roomSize = currentFloor.roomList.Count;
            Debug.Log("Event Fired");
            Vector3 roomyPos = currentFloor.roomList[rand.Next(0, roomSize)].transform.position;
            Vector3 enemyPos = new Vector3(roomyPos.x, roomyPos.y + .85f, roomyPos.z);
            Instantiate(enemyList[0].gameObject, enemyPos, Quaternion.identity);
        }
    }

}
