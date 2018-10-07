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
    /// Creates a Quaternion rotation for a 4 way piece based on the orintenation of the neighbors
    /// </summary>
    /// <param name="neighbors"></param>
    /// <returns></returns>
    public Quaternion fourWayRotation(bool[] neighbors) {
        return Quaternion.identity;
    }

    /// <summary>
    /// Creates a Quaternion rotation for a 3 way piece based on the orintenation of the neighbors
    /// </summary>
    /// <param name="neighbors"></param>
    /// <returns></returns>
    public Quaternion threeWayRotation(bool[] neighbors) {
        if (neighbors[0] && neighbors[1] && neighbors[2]) {
            return Quaternion.AngleAxis(-90f, Vector3.up);
        }

        if (neighbors[1] && neighbors[2] && neighbors[3]) {
            return Quaternion.AngleAxis(0f, Vector3.up);
        }

        if (neighbors[2] && neighbors[3] && neighbors[0]) {
            return Quaternion.AngleAxis(90f, Vector3.up);
        }

        if (neighbors[3] && neighbors[0] && neighbors[1]) {
            return Quaternion.AngleAxis(180f, Vector3.up);
        }

        return Quaternion.identity;
    }

    /// <summary>
    /// Creates a Quaternion rotation for a hallway piece based on the orintenation of the neighbors
    /// </summary>
    /// <param name="neighbors"></param>
    /// <returns></returns>
    public Quaternion hallwayRotation(bool[] neighbors) {
        if (neighbors[1]) {
            return Quaternion.AngleAxis(90f, Vector3.up);
        }

        return Quaternion.identity;
    }

    /// <summary>
    /// Creates a Quaternion rotation for a corner piece based on the orintenation of the neighbors
    /// </summary>
    /// <param name="neighbors"></param>
    /// <returns></returns>
    public Quaternion cornerRotation(bool[] neighbors) {
        if (neighbors[0] && neighbors[1]) {
            return Quaternion.AngleAxis(180f, Vector3.up);
        }

        if (neighbors[1] && neighbors[2]) {
            return Quaternion.AngleAxis(-90f, Vector3.up);
        }

        if (neighbors[2] && neighbors[3]) {
            return Quaternion.AngleAxis(0, Vector3.up);
        }

        if (neighbors[3] && neighbors[0]) {
            return Quaternion.AngleAxis(90f, Vector3.up);
        }

        return Quaternion.identity;
    }

    /// <summary>
    /// Creates a Quaternion rotation for a dead end piece based on the orintenation of the neighbors
    /// </summary>
    /// <param name="neighbors"></param>
    /// <returns></returns>
    public Quaternion deadEndRotation(bool[] neighbors) {
        if (neighbors[0]) {
            return Quaternion.AngleAxis(180f, Vector3.up);
        }

        if (neighbors[1]) {
            return Quaternion.AngleAxis(-90f, Vector3.up);
        }

        if (neighbors[2]) {
            return Quaternion.AngleAxis(0, Vector3.up);
        }

        if (neighbors[3]) {
            return Quaternion.AngleAxis(90f, Vector3.up);
        }

        return Quaternion.identity;
    }

    /// <summary>
    /// returns a random 4 way room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a 4 way room piece prefab</returns>
    public GameObject getRandomFourWayPrefab(System.Random rand) {
        return fourWayPrefabs[rand.Next(fourWayPrefabs.Length)];
    }

    /// <summary>
    /// returns a random 3 way room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a 3 way room piece prefab</returns>
    public GameObject getRandomThreeWayPrefab(System.Random rand) {
        return threeWayPrefabs[rand.Next(threeWayPrefabs.Length)];
    }

    /// <summary>
    /// returns a random hall way room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a hall way room piece prefab</returns>
    public GameObject getRandomHallWayPrefab(System.Random rand) {
        return hallWayPrefabs[rand.Next(hallWayPrefabs.Length)];
    }
    
    /// <summary>
    /// returns a corner room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a corner room piece prefab</returns>
    public GameObject getRandomCornerPrefab(System.Random rand) {
        return cornerPrefabs[rand.Next(cornerPrefabs.Length)];
    }

    /// <summary>
    /// returns a dead end room piece
    /// </summary>
    /// <param name="rand">system default random object</param>
    /// <returns>a dead end room piece prefab</returns>
    public GameObject getRandomDeadEndPrefab(System.Random rand) {
        return deadEndPrefabs[rand.Next(deadEndPrefabs.Length)];
    }
}