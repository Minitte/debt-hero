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

	/// <summary>
	/// TEST
	/// </summary>
	public GameObject TestPrefab; // TODO remove after testing

	[Header("Room Spec")]

	/// <summary>
	/// Size of the room
	/// </summary>
	public float roomSize;

	/// <summary>
	/// The min distance for exit
	/// </summary>
	public int minExitDistance;

	/// <summary>
	/// The max distance for exit
	/// </summary>
	public int maxExitDistance; 

	[Header("Others")]

	/// <summary>
	/// Prefab of a room entry for marking it's position
	/// </summary>
	public RoomEntry roomEntryPrefab;

	#endregion

	/// <summary>
	/// Room layout array
	/// </summary>
	private RoomEntry[,] _roomLayout;
	
	/// <summary>
	/// List of room entries generated
	/// </summary>
	private List<RoomEntry> _roomList;

	/// <summary>
	/// A dictionary containing room entries
	/// </summary>
	private Dictionary<XZCoordinate, RoomEntry> _roomDict;

	/// <summary>
	/// entrance 
	/// </summary>
	private RoomEntry _entrance;

	/// <summary>
	/// exit
	/// </summary>
	private RoomEntry _exit;

	/// <summary>
	/// Random object
	/// </summary>
	private System.Random _rand;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		_roomList = new List<RoomEntry>();

		_roomDict = new Dictionary<XZCoordinate, RoomEntry>();

		_rand = new System.Random();

		StartCoroutine(CoroutineGenerateFloor());
	}

	/// <summary>
	/// Generates the floor, this works between frames
	/// </summary>
	/// <returns></returns>
	private IEnumerator CoroutineGenerateFloor() {
		GenerateRoomExitEntrance();

		yield return GenerateRoomEntries(13);

		yield return generatePath(_entrance.coordinate, _exit.coordinate);

		yield return createRoomPieces();
	}

	/// <summary>
	/// Generates the entry for exit and entrance
	/// </summary>
	private void GenerateRoomExitEntrance() {
		// generate entrance
		_entrance = createRoomEntry(XZCoordinate.zero);
		_entrance.type = RoomEntry.RoomType.ENTRANCE;

		_exit = GenerateRandomRoomEntry(_entrance.coordinate, minExitDistance, maxExitDistance);
		_exit.type = RoomEntry.RoomType.EXIT;
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
		
		if (_roomDict.ContainsKey(coord)) {
			return null;
		}

		// ini room entry and set cords
		RoomEntry entry = Instantiate(roomEntryPrefab.gameObject).GetComponent<RoomEntry>();
		entry.coordinate = new XZCoordinate(coord);

		Vector3 pos = coord.toVector3();
		entry.transform.position = pos * roomSize;

		// add to room collections
		_roomList.Add(entry);
		_roomDict.Add(entry.coordinate, entry);

		return entry;
	}

	/// <summary>
	/// Generates a bunch of room entries
	/// </summary>
	/// <param name="maxDistance">max distance from the entrance</param>
	/// <returns></returns>
	private IEnumerator	GenerateRoomEntries(int maxDistance) {
		XZCoordinate[] nodes = new XZCoordinate[15];

		// create nodes
		for (int i = 0; i < nodes.Length; i++) {
			nodes[i] = generateRandomCord(_entrance.coordinate, 8, maxDistance);

			// nodes[0] = new Vector2(5, 3);

			createRoomEntry(nodes[i]);
		}

		// connect nodes to the entrance
		for (int i = 0; i < nodes.Length; i++) { 
			yield return generatePath(_entrance.coordinate, nodes[i]);
		}

		// connects the nodes to nearby nodes
		for (int i = 0; i < nodes.Length; i++) {
			XZCoordinate closest = findClosestCord(nodes[i], nodes);

			yield return generatePath(closest, nodes[i]);
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
		foreach (RoomEntry entry in _roomList) {
			GameObject testRoom = Instantiate(TestPrefab);

			testRoom.transform.SetParent(entry.transform, false);

			yield return new WaitForEndOfFrame();
		}
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
