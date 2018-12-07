using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    /// <summary>
    /// Prefab of the floor's enemies.
    /// </summary>
    public GameObject[] caveEnemyPrefabs;

    public GameObject[] forestEnemyPrefabs;

    public GameObject[] fireEnemyPrefabs;

    public List<GameObject> enemies;

    // Debt collector enemy
    public GameObject debtCollector;

    /// <summary>
    /// max number of enemies oer group
    /// </summary>
    public int minGroupSize = 2;

    /// <summary>
    /// min number of enemies per group
    /// </summary>
    public int maxGroupSize = 4;
    

    // Use this for initialization
    void Start () {
        FloorGenerator.OnFloorGenerated += SpawnEnemies;

        FloorGenerator.OnBeginGeneration += DespawnEnemies;
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
       
        for (int i = 0; i <= rand.Next(6, 10); i++) {
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
    /// <param name="roomPos">position of the room</param>
    /// <param name="rand">Random used to spawn randomly.</param>
    void EnemyGroups(int floor, Vector3 roomPos, System.Random rand) {
        int groupSize = rand.Next(minGroupSize, maxGroupSize);
        //Cave
        if (floor >= 0 && floor <= 3) {
            for (int i = 0; i < groupSize; i++) {

                Vector3 enemyPos = RandomRoomPos(roomPos, rand);  

                GameObject spawned = Instantiate(caveEnemyPrefabs[rand.Next(0, caveEnemyPrefabs.Length)], enemyPos, Quaternion.identity);
                enemies.Add(spawned);
            }
        }

        // forest
        else if (floor >= 4 && floor <= 7) {
            for (int i = 0; i < groupSize; i++) {

                Vector3 enemyPos = RandomRoomPos(roomPos, rand);

                GameObject spawned = Instantiate(forestEnemyPrefabs[rand.Next(0, forestEnemyPrefabs.Length)], enemyPos, Quaternion.identity);
                enemies.Add(spawned);
            }
        }

        // fire
        else if (floor >= 8 && floor <= 11) {
            for (int i = 0; i < groupSize; i++) {

                Vector3 enemyPos = RandomRoomPos(roomPos, rand);

                GameObject spawned = Instantiate(fireEnemyPrefabs[rand.Next(0, fireEnemyPrefabs.Length)], enemyPos, Quaternion.identity);
                enemies.Add(spawned);
            }
        }

        if (EventManager.instance.spawnDebtCollectors) {
            for (int i = 0; i < spawnScalar; i++) {
                GameObject spawned = Instantiate(debtCollector, enemyPos, Quaternion.identity);
                enemies.Add(spawned);
            }
        }
    }

    /// <summary>
    /// Gerneates a random position on the given floor position
    /// </summary>
    /// <param name="roomPos"></param>
    /// <param name="rand"></param>
    /// <returns></returns>
    public Vector3 RandomRoomPos(Vector3 roomPos, System.Random rand) {
        int xVar = rand.Next(-13, 13);
        xVar = xVar != 0 ? 2 : xVar;

        int zVar = rand.Next(-13, 13);
        zVar = zVar != 0 ? 2 : zVar;

        return new Vector3(roomPos.x + xVar , roomPos.y, roomPos.z + zVar);
        // return roomPos;
    }
}
