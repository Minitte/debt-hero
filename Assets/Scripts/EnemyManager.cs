using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    /// <summary>
    /// Prefab of the floor's enemies.
    /// </summary>
    public GameObject[] enemyType;
    public List<GameObject> enemies;

    /// <summary>
    /// spawnScalar is a number used to scale amount of enemies.
    /// </summary>
    public int spawnScalar;
    

    // Use this for initialization
    void Start () {
        FloorGenerator.OnFloorGenerated += SpawnEnemies;

        FloorGenerator.OnBeginGeneration += DespawnEnemies;
        spawnScalar = 2;
    }
	
	// Update is called once per frame
	void Update () {
        enemies.RemoveAll(item => item == null);
    }

    /// <summary>
    /// Destories all enemies 
    /// </summary>
    public void DestoryAllEnemies() {
        foreach (GameObject e in enemies) {
            Destroy(e);
        }
    }

    /// <summary>
    /// Destories all enemies when FloorGenerator.OnBeginGeneration is triggered
    /// </summary>
    /// <param name="floor"></param>
    /// <param name="random"></param>
    private void DespawnEnemies(Floor floor, System.Random random) {
        DestoryAllEnemies();
    }

    /// <summary>
    /// Spawn Enemies in a room.
    /// </summary>
    /// <param name="currentFloor">A floor that contains a room</param>
    /// <param name="rand"> Random used to determine which room to use.</param>
    void SpawnEnemies(Floor currentFloor, System.Random rand) {
       
        for (int i = 0; i <= rand.Next(5, 10); i++) {
            int roomSize = currentFloor.roomList.Count;

            RoomEntry room = currentFloor.roomList[rand.Next(0, roomSize)];
             if (room.type == RoomEntry.RoomType.SAFE) {
                continue;
            }
            
            Vector3 roomyPos = room.transform.position;
            Vector3 enemyPos = new Vector3(roomyPos.x, roomyPos.y + .85f, roomyPos.z);

            EnemyGroups(currentFloor.floorNumber, enemyPos, rand);
        }
    }

    /// <summary>
    /// EnemyGroups handles getting references to the an enemy.
    /// This function also handles spawning a group of enemies.
    /// Lots of code duplication unfortunately.
    /// </summary>
    /// <param name="floor">Current floor number.</param>
    /// <param name="enemyPos">Vector to spawn enemy.</param>
    /// <param name="rand">Random used to spawn randomly.</param>
    void EnemyGroups(int floor, Vector3 enemyPos, System.Random rand) {
        //Cave
        if (floor >= 0 && floor <= 3) {
            for(int i = 0; i < spawnScalar; i++) {
                GameObject spawned = Instantiate(enemyType[rand.Next(0, 3)], enemyPos, Quaternion.identity);
                enemies.Add(spawned);
            }
        }
    }
}
