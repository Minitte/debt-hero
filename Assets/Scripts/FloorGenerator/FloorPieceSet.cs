using System;
using UnityEngine;

[Serializable]
public class FloorPieceSet { 

    /// <summary>
    /// An array of four way room prefabs. Multiple pieces can be added for variety 
    /// </summary>
    public GameObject[] fourWayPrefabs;

    /// <summary>
    /// An array of three way or T room prefabs. Multiple pieces can be added for variety
    /// </summary>
    public GameObject[] threeWayPrefabs;

    /// <summary>
    /// An array of hallway room or straight prefabs. Multiple pieces can be added for variety
    /// </summary>
    public GameObject[] hallWayPrefabs;

    /// <summary>
    /// An array of corner room prefabs. Multiple pieces can be added for variety
    /// </summary>
    public GameObject[] cornerPrefabs;

    /// <summary>
    /// An array of dead end or cap room prefabs. Multiple pieces can be added for variety
    /// </summary>
    public GameObject[] deadEndPrefabs;

    /// <summary>
    /// returns a random 4 way room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a 4 way room piece prefab</returns>
    public GameObject GetRandomFourWayPrefab(System.Random rand) {
        return fourWayPrefabs[rand.Next(fourWayPrefabs.Length)];
    }

    /// <summary>
    /// returns a random 3 way room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a 3 way room piece prefab</returns>
    public GameObject GetRandomThreeWayPrefab(System.Random rand) {
        return threeWayPrefabs[rand.Next(threeWayPrefabs.Length)];
    }

    /// <summary>
    /// returns a random hall way room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a hall way room piece prefab</returns>
    public GameObject GetRandomHallWayPrefab(System.Random rand) {
        return hallWayPrefabs[rand.Next(hallWayPrefabs.Length)];
    }
    
    /// <summary>
    /// returns a corner room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a corner room piece prefab</returns>
    public GameObject GetRandomCornerPrefab(System.Random rand) {
        return cornerPrefabs[rand.Next(cornerPrefabs.Length)];
    }

    /// <summary>
    /// returns a dead end room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a dead end room piece prefab</returns>
    public GameObject GetRandomDeadEndPrefab(System.Random rand) {
        return deadEndPrefabs[rand.Next(deadEndPrefabs.Length)];
    }
}