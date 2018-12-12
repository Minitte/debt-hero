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

    /// <summary>
    /// Base scalar for the enemy stats based on floor level.
    /// e.g. 1.2 means the enemies become 20% stronger each floor.
    /// </summary>
    public float enemyPowerGrowth = 1.2f;

    // Use this for initialization
    void Start () {
        FloorGenerator.OnFloorGenerated += SpawnEnemies;

        FloorGenerator.OnBeginGeneration += DespawnEnemies;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy() {
        FloorGenerator.OnFloorGenerated -= SpawnEnemies;

        FloorGenerator.OnBeginGeneration -= DespawnEnemies;
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
            e.GetComponent<EnemyCharacter>().Die();
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
       
        int numGroups = rand.Next(6, 10);

        for (int i = 0; i <= numGroups; i++) {
            int roomSize = currentFloor.roomList.Count;

            RoomEntry room = currentFloor.roomList[rand.Next(0, roomSize)];

            if (room.type == RoomEntry.RoomType.SAFE) {
                continue;
            }

            // don't spawn ontop of player or entrance
            if (room.type == RoomEntry.RoomType.ENTRANCE || room.type == RoomEntry.RoomType.EXIT) {
                i--;
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
        GameObject[] enemyPrefabs = null;

        // Select enemy prefab list based on floor level
        if (FloorTheme.IsCurrentlyCave()) {
            enemyPrefabs = caveEnemyPrefabs; // Cave
        } else if (FloorTheme.IsCurrentlyForest()) {
            enemyPrefabs = forestEnemyPrefabs; // Forest
        } else if (FloorTheme.IsCurrentlyFire()) {
            enemyPrefabs = fireEnemyPrefabs; // Fire
        }

        // Spawn enemies
        for (int i = 0; i < groupSize; i++) {
                Vector3 enemyPos = RandomRoomPos(roomPos, rand);  
                GameObject spawned = Instantiate(enemyPrefabs[rand.Next(0, enemyPrefabs.Length)], enemyPos, Quaternion.identity);

                // Scale the enemy stats based on floor level
                if (floor > 1) {
                    ScaleEnemy(spawned, floor);
                }
                enemies.Add(spawned);
            }

        if (EventManager.instance.spawnDebtCollectors) {
            for (int i = 0; i < groupSize; i++) {
                Vector3 enemyPos = RandomRoomPos(roomPos, rand);
                GameObject spawned = Instantiate(debtCollector, enemyPos, Quaternion.identity);
                enemies.Add(spawned);
            }
        }
    }

    /// <summary>
    /// Scale an enemy's stats based on floor level.
    /// </summary>
    /// <param name="enemy">The enemy to scale</param>
    /// <param name="floor">The floor level</param>
    private void ScaleEnemy(GameObject enemy, int floor) {
        CharacterStats stats = enemy.GetComponent<CharacterStats>();

        // Scalar = base ^ floor level - 1
        float scalar = Mathf.Pow(enemyPowerGrowth, floor - 1);

        // Scale the stats
        stats.maxHp = Mathf.Round(stats.maxHp * scalar);
        stats.currentHp = stats.maxHp;
        stats.physAtk = Mathf.Round(stats.physAtk * scalar);
        stats.magicAtk = Mathf.Round(stats.magicAtk * scalar);

        // scale exp and gold
        Drops drop = enemy.GetComponent<Drops>();

        float dropScale = Mathf.Pow(enemyPowerGrowth * 0.5f, floor - 1);

        drop.exp = drop.exp * dropScale;
        drop.gold = (int) (drop.gold * dropScale);
    } 

    /// <summary>
    /// Gerneates a random position on the given floor position
    /// </summary>
    /// <param name="roomPos"></param>
    /// <param name="rand"></param>
    /// <returns></returns>
    public Vector3 RandomRoomPos(Vector3 roomPos, System.Random rand) {
        int xVar = rand.Next(-9, 9);
        xVar = xVar == 0 ? 2 : xVar;

        int zVar = rand.Next(-13, 13);
        zVar = zVar == 0 ? 2 : zVar;

        return new Vector3(roomPos.x + xVar , roomPos.y, roomPos.z + zVar);
        // return roomPos;
    }
}
