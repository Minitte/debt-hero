using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

	#region public vars
	[Header("Floor Pieces")]

	/// <summary>
	/// A set of pieces for generating rooms
	/// </summary>
	public FloorPieceSet pieceSet;

	[Header("Room Spec")]

	/// <summary>
	/// Size of the room
	/// </summary>
	public float roomSize = 25f;

	/// <summary>
	/// Max range from zero
	/// </summary>
	public int maxFloorRadius = 13; 

	/// <summary>
	/// Seed to be used in generating the floor
	/// </summary>
	public int floorSeed;

	[Header("Game Objects and Prefabs")]

	/// <summary>
	/// Prefab of a room entry for marking it's position
	/// </summary>
	public RoomEntry roomEntryPrefab;

	/// <summary>
	/// Prefab of a floor parent
	/// </summary>
	public Floor floorParentPrefab;

	/// <summary>
	/// Current floor parent
	/// </summary>
	public Floor currentFloorParent;

	#endregion

	/// <summary>
	/// Random object
	/// </summary>
	private System.Random _rand;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		GenerateNewFloor(floorSeed);
	}

	/// <summary>
	/// Generates and replaces the current floor with a new one based on the seed
	/// </summary>
	/// <param name="floorSeed"></param>
	public void GenerateNewFloor(int floorSeed) {
		_rand = new System.Random(floorSeed);

		StartCoroutine(CoroutineGenerateFloor());
	}

	/// <summary>
	/// Generates the floor, this works between frames
	/// </summary>
	/// <returns></returns>
	private IEnumerator CoroutineGenerateFloor() {
		currentFloorParent = Instantiate(floorParentPrefab.gameObject).GetComponent<Floor>();

		yield return GenerateRoomEntries(maxFloorRadius);

		RoomEntry entrance = getRandomRoomEntry();
		entrance.type = RoomEntry.RoomType.ENTRANCE;
		entrance.gameObject.name = "Entrance Room Entry";

		RoomEntry exit = getRandomRoomEntry();
		exit.type = RoomEntry.RoomType.EXIT;
		exit.gameObject.name = "Exit Room Entry";

		yield return createRoomPieces();
	}

	/// <summary>
	/// Generates a random room entry 
	/// </summary>
	/// <param name="point">the center point to generate from</param>
	/// <param name="minDist">the min distance from the center point</param>
	/// <param name="maxDist">the max distance from the center point</param>
	/// <returns>A randomly generated room entry</returns>
	private RoomEntry GenerateRandomRoomEntry(XZCoordinate point, int minDist, int maxDist) {
		// generate random cord
		XZCoordinate coord = generateRandomCord(point, minDist, maxDist);
		
		RoomEntry room = createRoomEntry(coord);

		return room;
	}

	/// <summary>
	/// Creates a room entry.
	/// </summary>
	/// <param name="x">x cord</param>
	/// <param name="z">y cord</param>
	/// <returns>The created room entry. If room already exist on the cord, null is returned</returns>
	private RoomEntry createRoomEntry(XZCoordinate coord) {
		
		if (currentFloorParent.roomDict.ContainsKey(coord.ToString())) {
			return null;
		}

		// ini room entry and set cords
		RoomEntry entry = Instantiate(roomEntryPrefab.gameObject).GetComponent<RoomEntry>();

		entry.transform.SetParent(currentFloorParent.transform, false);

		entry.coordinate = new XZCoordinate(coord);

		Vector3 pos = coord.toVector3();
		entry.transform.position = pos * roomSize;

		// add to room collections
		currentFloorParent.roomList.Add(entry);
		currentFloorParent.roomDict.Add(entry.coordinate.ToString(), entry);

		return entry;
	}

	/// <summary>
	/// Generates a bunch of room entries
	/// </summary>
	/// <param name="maxDistance">max distance from the entrance</param>
	/// <returns></returns>
	private IEnumerator	GenerateRoomEntries(int maxDistance) {
		XZCoordinate[] nodes = new XZCoordinate[6];

		// create nodes
		for (int i = 0; i < nodes.Length; i++) {
			nodes[i] = generateRandomCord(XZCoordinate.zero, 5, maxDistance);

			// nodes[0] = new Vector2(5, 3);

			createRoomEntry(nodes[i]);
		}

		// connect nodes to the entrance
		// for (int i = 0; i < nodes.Length; i++) { 
		// 	yield return generatePath(_entrance.coordinate, nodes[i]);
		// }

		// connects the nodes to nearby nodes
		// for (int i = 0; i < nodes.Length; i++) {
		// 	XZCoordinate closest = findClosestCord(nodes[i], nodes);

		// 	yield return generatePath(closest, nodes[i]);
		// }

		// connect node to others
		for (int i = 0; i < nodes.Length; i++) {
			for (int j = i; j < nodes.Length; j++) {
				yield return generatePath(nodes[i], nodes[j]);
			}
		}
	}

	/// <summary>
	/// Generates a path from start to end
	/// </summary>
	private IEnumerator generatePath(XZCoordinate start, XZCoordinate end) {
		XZCoordinate cur = new XZCoordinate(start);

		// directional sign
		int xSign = (int) end.x > cur.x ? 1 : -1;
		int zSign = (int) end.z > cur.z ? 1 : -1;

		// move towards the end cord
		while (cur != end) {
			// decide on which axis to move
			int dir = _rand.Next(0, 2);

			if (dir == 1 && cur.x != end.x) {
				cur.x += xSign;
			} else if (dir == 0 && cur.z != end.z){
				cur.z += zSign;
			} else {
				if (cur.x != end.x) {
					cur.x += xSign;
				} else {
					cur.z += zSign;
				}
			}

			yield return new WaitForEndOfFrame();

			createRoomEntry(cur);
		}
	}

	/// <summary>
	/// Creates room pieces
	/// </summary>
	private IEnumerator createRoomPieces() {
		foreach (RoomEntry entry in currentFloorParent.roomList) {

			int numNeigbors = 0;

			bool[] neighbors = getNeighborBools(entry, out numNeigbors);

			GameObject piecePrefab;

			GameObject piece;

			switch (numNeigbors) {
				// dead end
				case 1:
					// get a random piece from the set
					piecePrefab = pieceSet.GetRandomDeadEndPrefab(_rand);

					// instantiate and set parent to the entry
					piece = Instantiate(piecePrefab, currentFloorParent.transform);

					// set the rotation based on neighbor orientation
					piece.transform.rotation = pieceSet.deadEndRotation(neighbors);

					break;

				// corner or hall
				case 2:
					// check if hallway
					if ((neighbors[0] && neighbors[2]) || (neighbors[1] && neighbors[3])) {
						// get a random piece from the set
						piecePrefab = pieceSet.GetRandomHallWayPrefab(_rand);

						// instantiate and set parent to the entry
						piece = Instantiate(piecePrefab, entry.transform);

						// set the rotation based on neighbor orientation
						piece.transform.rotation = pieceSet.hallwayRotation(neighbors);
					} else {
						// get a random piece from the set
						piecePrefab = pieceSet.GetRandomCornerPrefab(_rand);

						// instantiate and set parent to the entry
						piece = Instantiate(piecePrefab, entry.transform);

						// set the rotation based on neighbor orientation
						piece.transform.rotation = pieceSet.cornerRotation(neighbors);
					}
				
					break;

				// T 
				case 3:
					// get a random piece from the set
					piecePrefab = pieceSet.GetRandomThreeWayPrefab(_rand);

					// instantiate and set parent to the entry
					piece = Instantiate(piecePrefab, entry.transform);

					// set the rotation based on neighbor orientation
					piece.transform.rotation = pieceSet.threeWayRotation(neighbors);

					break;

				// cross
				case 4:
					// get a random piece from the set
					piecePrefab = pieceSet.GetRandomFourWayPrefab(_rand);

					// instantiate and set parent to the entry
					piece = Instantiate(piecePrefab, entry.transform);

					// set the rotation based on neighbor orientation
					piece.transform.rotation = pieceSet.fourWayRotation(neighbors);
					break;

				case 0:
					break;

				default:
					XZCoordinate coord = entry.coordinate;
					Debug.LogError("x:" + coord.x + " z:" + coord.z + " supposely has " + numNeigbors + " neighbors?");
					break;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	/// <summary>
	/// Creates an array of bool to show wether there is a neighbor.
	/// Clockwise
	/// </summary>
	/// <param name="entry"></param>
	/// <returns></returns>
	private bool[] getNeighborBools(RoomEntry entry, out int numNeigbors) {
		bool[] n = new bool[4];

		int num = 0;

		XZCoordinate cord = entry.coordinate;

		if (currentFloorParent.roomDict.ContainsKey(cord.up().ToString())) {
			num++;
			n[0] = true;
		}

		if (currentFloorParent.roomDict.ContainsKey(cord.right().ToString())) {
			num++;
			n[1] = true;
		}

		if (currentFloorParent.roomDict.ContainsKey(cord.down().ToString())) {
			num++;
			n[2] = true;
		}

		if (currentFloorParent.roomDict.ContainsKey(cord.left().ToString())) {
			num++;
			n[3] = true;
		}

		numNeigbors = num;

		return n;
	}

	/// <summary>
	/// Gets a random room entry 
	/// </summary>
	/// <returns></returns>
	public RoomEntry getRandomRoomEntry() {
		return currentFloorParent.roomList[_rand.Next(currentFloorParent.roomList.Count)];
	}

	/// <summary>
	/// Generates a random cord around the point
	/// </summary>
	/// <param name="point">center point</param>
	/// <param name="minDist">min distance from the center point</param>
	/// <param name="maxDist">max distance from the center point</param>
	/// <returns></returns>
	private XZCoordinate generateRandomCord(XZCoordinate point, int minDist, int maxDist) {
		// random distance
		int dist = _rand.Next(minDist, maxDist);
		
		// random angle
		float angle = _rand.Next(0, 360) * Mathf.Deg2Rad;

		// create cord
		XZCoordinate translateCord = new XZCoordinate(Mathf.Sin(angle) * dist, Mathf.Cos(angle) * dist);

		return point + translateCord;
	}

	/// <summary>
	/// Finds the closest cord to the goal. Matching cord in the list are ignored
	/// </summary>
	/// <param name="goal"></param>
	/// <param name="list"></param>
	/// <returns></returns>
	private XZCoordinate findClosestCord(XZCoordinate goal, params XZCoordinate[] list) {
		XZCoordinate closest = goal;
		int minDist = int.MaxValue;

		for (int i = 0; i < list.Length; i++) {

			if (goal == list[i]) {
				continue;
			}

			// distance
			int dist = list[i].blockDistance(goal);
			
			// compare
			if (dist < minDist) {
				closest = list[i];
				minDist = dist;
			}
		}

		return closest;
	}
}
