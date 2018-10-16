using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    /// <summary>
    /// Prefab of the floor's enemies.
    /// </summary>
    public GameObject[] enemyType;
    public List<GameObject> enemies;

    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        FloorGenerator.OnFloorGenerated += SpawnEnemies;
        enemies.RemoveAll(item => item == null);
    }

    /// <summary>
    /// Spawn Enemies in a room.
    /// </summary>
    /// <param name="currentFloor">A floor that contains a room</param>
    /// <param name="rand"> Random used to determine which room to use.</param>
    void SpawnEnemies(Floor currentFloor, System.Random rand) {
        //Debug.Log("Event Fired");
        int roomSize = currentFloor.roomList.Count;
        
        Vector3 roomyPos = currentFloor.roomList[rand.Next(0, roomSize)].transform.position;
        Vector3 enemyPos = new Vector3(roomyPos.x, roomyPos.y + .85f, roomyPos.z);
        GameObject temp = enemyType[0].gameObject;
        Instantiate(temp, enemyPos, Quaternion.identity);
        enemies.Add(temp);
    }
}
