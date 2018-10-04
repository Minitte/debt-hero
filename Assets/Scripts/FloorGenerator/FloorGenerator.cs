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
	private Dictionary<Vector3, RoomEntry> _roomDict;

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

		_roomDict = new Dictionary<Vector3, RoomEntry>();

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

		yield return generateStraightPath(_entrance.cordV2, _exit.cordV2);

		yield return createRoomPieces();
	}

	/// <summary>
	/// Generates the entry for exit and entrance
	/// </summary>
	private void GenerateRoomExitEntrance() {
		// generate entrance
		_entrance = createRoomEntry(0, 0);
		_entrance.type = RoomEntry.RoomType.ENTRANCE;

		_exit = GenerateRandomRoomEntry(_entrance.cordV2, minExitDistance, maxExitDistance);
		_exit.type = RoomEntry.RoomType.EXIT;
	}

	/// <summary>
	/// Generates a random room entry 
	/// </summary>
	/// <param name="point">the center point to generate from</param>
	/// <param name="minDist">the min distance from the center point</param>
	/// <param name="maxDist">the max distance from the center point</param>
	/// <returns>A randomly generated room entry</returns>
	private RoomEntry GenerateRandomRoomEntry(Vector2 point, int minDist, int maxDist) {
		// generate random cord
		Vector2 cord = generateRandomCord(point, minDist, maxDist);
		
		RoomEntry room = createRoomEntry((int)cord.x, (int)cord.y);

		return room;
	}

	/// <summary>
	/// Creates a room entry.
	/// </summary>
	/// <param name="x">x cord</param>
	/// <param name="z">y cord</param>
	/// <returns>The created room entry. If room already exist on the cord, null is returned</returns>
	private RoomEntry createRoomEntry(int x, int z) {
		
		if (_roomDict.ContainsKey(new Vector2(x, z))) {
			return null;
		}

		// ini room entry and set cords
		RoomEntry entry = Instantiate(roomEntryPrefab.gameObject).GetComponent<RoomEntry>();
		entry.xCord = x;
		entry.zCord = z;

		Vector3 pos = new Vector3(x, 0f, z);
		entry.transform.position = pos * roomSize;

		// add to room collections
		_roomList.Add(entry);
		_roomDict.Add(entry.cordV2, entry);

		return entry;
	}

	/// <summary>
	/// Generates a bunch of room entries
	/// </summary>
	/// <param name="maxDistance">max distance from the entrance</param>
	/// <returns></returns>
	private IEnumerator	GenerateRoomEntries(int maxDistance) {
		Vector2[] nodes = new Vector2[15];

		// create nodes
		for (int i = 0; i < nodes.Length; i++) {
			nodes[i] = generateRandomCord(_entrance.cordV2, 8, maxDistance);

			// nodes[0] = new Vector2(5, 3);

			createRoomEntry((int)nodes[i].x, (int)nodes[i].y);
		}

		// connect nodes to the entrance
		for (int i = 0; i < nodes.Length; i++) { 
			yield return generateStraightPath(_entrance.cordV2, nodes[i]);
		}

		// connects the nodes to nearby nodes
		for (int i = 0; i < nodes.Length; i++) {
			Vector2 closest = findClosestCord(nodes[i], nodes);

			yield return generateStraightPath(closest, nodes[i]);
		}
	}

	/// <summary>
	/// Generates a straight path from start to end
	/// </summary>
	private IEnumerator generateStraightPath(Vector2 start, Vector2 end) {
		int curXCord = (int) start.x;
		int curZCord = (int) start.y;

		// directional sign
		int xSign = (int) end.x > curXCord ? 1 : -1;
		int zSign = (int) end.y > curZCord ? 1 : -1;

		// move towards the end cord
		while (curZCord != end.y || curXCord != end.x) {
			// decide on which axis to move
			int dir = _rand.Next(0, 2);

			if (dir == 1 && curXCord != end.x) {
				curXCord += xSign;
			} else if (dir == 0 && curZCord != end.y){
				curZCord += zSign;
			} else {
				if (curXCord != end.x) {
					curXCord += xSign;
				} else {
					curZCord += zSign;
				}
			}

			yield return new WaitForEndOfFrame();

			createRoomEntry(curXCord, curZCord);
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
	private Vector2 generateRandomCord(Vector2 point, int minDist, int maxDist) {
		// random distance
		int dist = _rand.Next(minDist, maxDist);
		
		// random angle
		float angle = _rand.Next(0, 360) * Mathf.Deg2Rad;

		// create cord
		Vector2 translateCord = new Vector2(Mathf.Sin(angle) * dist, Mathf.Cos(angle) * dist);
		
		translateCord.x = (int) translateCord.x;
		translateCord.y = (int) translateCord.y;

		return point + translateCord;
	}

	/// <summary>
	/// Finds the closest cord to the goal. Matching cord in the list are ignored
	/// </summary>
	/// <param name="goal"></param>
	/// <param name="list"></param>
	/// <returns></returns>
	private Vector2 findClosestCord(Vector2 goal, params Vector2[] list) {
		Vector2 closest = goal;
		float minDist = float.MaxValue;

		for (int i = 0; i < list.Length; i++) {

			if (goal == list[i]) {
				continue;
			}

			// distance
			float dist = (list[i] - goal).sqrMagnitude;
			
			// compare
			if (dist < minDist) {
				closest = list[i];
				minDist = dist;
			}
		}

		return closest;
	}
	

}
