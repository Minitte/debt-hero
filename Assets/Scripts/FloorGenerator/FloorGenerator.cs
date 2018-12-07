using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FloorGenerator : MonoBehaviour {

	// EVENT STUFF
	#region events

	/// <summary>
	/// Floor related event delegate/template
	/// </summary>
	/// <param name="floor">Floor that is related to the event trigger</param>
	/// /// <param name="random">Random uses to randomize rooms. Has the same seed</param>
	public delegate void FloorEvent(Floor floor, System.Random random);

	/// <summary>
	/// FloorEvent that is triggered when a floor begins to generate
	/// </summary>
	public static event FloorEvent OnBeginGeneration;

	/// <summary>
	/// FloorEvent that is triggered when the floor is done generating
	/// </summary>
	public static event FloorEvent OnFloorGenerated;

	#endregion

	#region public vars
	[Header("Floor Pieces")]

	/// <summary>
	/// A set of pieces for generating rooms
	/// </summary>
	public FloorPieceSet[] pieceSets;

	[Header("Room Spec")]

	/// <summary>
	/// Size of the room
	/// </summary>
	public float roomSize = 25f;

	/// <summary>
	/// Max range from zero
	/// </summary>
	public int maxFloorRadius = 13; 

	[Header("Generation Variables")]

	/// <summary>
	/// Number of node rooms in generating the floor
	/// </summary>
	[Range(2, 10)]
	public int numberOfNodes;

	/// <summary>
	/// Seed to be used in generating the floor
	/// </summary>
	public string floorSeed;

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
	/// Prefab of the floor's exit
	/// </summary>
	public GameObject exitPrefab;

	/// <summary>
	/// Current floor parent
	/// </summary>
	public Floor currentFloorParent;

	[Header("Loading Variables")]

	/// <summary>
	/// Number of entries generated per frame at max
	/// </summary>
	[Range(1, 1000)]
	public int entriesPerFrame = 3;

	/// <summary>
	/// Number of pieces generated per frame at max
	/// </summary>
	[Range(1, 1000)]
	public int piecesPerFrame = 4;

	#endregion

	/// <summary>
	/// Random object
	/// </summary>
	private System.Random _rand;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		GenerateNewFloor(false);
    }
	
	/// <summary>
	/// Increments the floor number and generates a new floor
	/// </summary>
	public void NextFloor() {
		PlayerProgress.currentFloor++;

		if (PlayerProgress.currentFloor > PlayerProgress.floorReached) {
			PlayerProgress.floorReached = PlayerProgress.currentFloor;
		}

		if (PlayerProgress.currentFloor % 3 == 0) {
			StartCoroutine(GenerateSafeZone());
		} else {
			GenerateNewFloor(true);
		}
	}

	/// <summary>
	/// Generates a safezone
	/// </summary>
	private IEnumerator GenerateSafeZone() {
			if (currentFloorParent != null) {
				Destroy(currentFloorParent.gameObject);
				currentFloorParent = null;
			}

		currentFloorParent = Instantiate(floorParentPrefab.gameObject).GetComponent<Floor>();
		currentFloorParent.floorNumber = PlayerProgress.currentFloor;

		if (OnBeginGeneration != null) {
			OnBeginGeneration(currentFloorParent, _rand);
		}

		RoomEntry entry = createRoomEntry(XZCoordinate.zero);
		entry.type = RoomEntry.RoomType.SAFE;
		currentFloorParent.entrance = entry;

		GameObject safeZone = Instantiate(GetCurrentSet().GetRandomSafeZonePrefab(_rand), entry.transform);

		safeZone.GetComponentInChildren<FloorExitTrigger>().TargetGenerator = this;

		yield return new WaitForEndOfFrame();

		currentFloorParent.GetComponent<NavMeshSurface>().BuildNavMesh();

		// trigger event if anything is listening to it
		if (OnFloorGenerated != null) {
			OnFloorGenerated(currentFloorParent, _rand);           
		}
	}

    /// <summary>
    /// Generates and replaces the current floor with a new one based on the seed
    /// </summary>
    /// <param name="floorSeedNumber"></param>
    /// <param name="destoryOldFloor">flag to destory the old floor</param>
    private void GenerateNewFloor(bool destoryOldFloor) {
		_rand = new System.Random(GenerateSeedNumber(floorSeed, PlayerProgress.currentFloor));

		if (destoryOldFloor) {
			if (currentFloorParent != null) {
				Destroy(currentFloorParent.gameObject);
				currentFloorParent = null;
			}
		}

		StartCoroutine(CoroutineGenerateFloor());
	}

	/// <summary>
	/// Generates the floor, this works between frames
	/// </summary>
	/// <returns></returns>
	private IEnumerator CoroutineGenerateFloor() {
		currentFloorParent = Instantiate(floorParentPrefab.gameObject).GetComponent<Floor>();

		currentFloorParent.floorNumber = PlayerProgress.currentFloor;

		if (OnBeginGeneration != null) {
			OnBeginGeneration(currentFloorParent, _rand);
		}

		yield return GenerateRoomEntries(maxFloorRadius);

		// pick a room to be the entrance
		RoomEntry entrance = GetRandomRoomEntry();
		entrance.type = RoomEntry.RoomType.ENTRANCE;
		entrance.gameObject.name = "Entrance Room Entry";
		currentFloorParent.entrance = entrance;

		// pick a room to be the exit
		RoomEntry exit = GetRandomRoomEntry();
		exit.type = RoomEntry.RoomType.EXIT;
		exit.gameObject.name = "Exit Room Entry";
		currentFloorParent.exit = exit;

        // place exit stairs
        FloorExitTrigger fet = Instantiate(exitPrefab, exit.transform).GetComponent<FloorExitTrigger>();

		fet.TargetGenerator = this;
        yield return CreateRoomPieces();

		currentFloorParent.GetComponent<NavMeshSurface>().BuildNavMesh();

        SoundManager.instance.PlayMusic(1);

		// trigger event if anything is listening to it
		if (OnFloorGenerated != null) {
			OnFloorGenerated(currentFloorParent, _rand);           
		}
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
		XZCoordinate coord = GenerateRandomCord(point, minDist, maxDist);
		
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

		Vector3 pos = coord.ToVector3();
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
		XZCoordinate[] nodes = new XZCoordinate[numberOfNodes];

		// create nodes
		for (int i = 0; i < nodes.Length; i++) {
			nodes[i] = GenerateRandomCord(XZCoordinate.zero, 2, maxDistance);

			// nodes[0] = new Vector2(5, 3);

			createRoomEntry(nodes[i]);
		}

		// connect node to others
		for (int i = 0; i < nodes.Length; i++) {
			for (int j = i; j < nodes.Length; j++) {
				yield return GeneratePath(nodes[i], nodes[j]);
			}
		}
	}

	/// <summary>
	/// Generates a path from start to end
	/// </summary>
	private IEnumerator GeneratePath(XZCoordinate start, XZCoordinate end) {
		XZCoordinate cur = new XZCoordinate(start);

		// directional sign
		int xSign = (int) end.x > cur.x ? 1 : -1;
		int zSign = (int) end.z > cur.z ? 1 : -1;

		int entriesGenerated = 0;

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

			entriesGenerated++;

			if (entriesGenerated % entriesPerFrame == 0) {
				yield return new WaitForEndOfFrame();
			}

			createRoomEntry(cur);
		}
	}

	/// <summary>
	/// Creates room pieces
	/// </summary>
	private IEnumerator CreateRoomPieces() {
		int piecesGenerated = 0;

		foreach (RoomEntry entry in currentFloorParent.roomList) {

			int numNeigbors = 0;

			bool[] neighbors = GetNeighborBools(entry, out numNeigbors);

			GameObject piecePrefab;

			GameObject piece;

			FloorPieceSet pieceSet = GetCurrentSet();

			switch (numNeigbors) {
				// dead end
				case 1:
					// get a random piece from the set
					piecePrefab = pieceSet.GetRandomDeadEndPrefab(_rand);

					// instantiate and set parent to the entry
					piece = Instantiate(piecePrefab, entry.transform);

					// set the rotation based on neighbor orientation
					piece.transform.rotation = pieceSet.DeadEndRotation(neighbors);

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
						piece.transform.rotation = pieceSet.HallwayRotation(neighbors);
					} else {
						// get a random piece from the set
						piecePrefab = pieceSet.GetRandomCornerPrefab(_rand);

						// instantiate and set parent to the entry
						piece = Instantiate(piecePrefab, entry.transform);

						// set the rotation based on neighbor orientation
						piece.transform.rotation = pieceSet.CornerRotation(neighbors);
					}
				
					break;

				// T 
				case 3:
					// get a random piece from the set
					piecePrefab = pieceSet.GetRandomThreeWayPrefab(_rand);

					// instantiate and set parent to the entry
					piece = Instantiate(piecePrefab, entry.transform);

					// set the rotation based on neighbor orientation
					piece.transform.rotation = pieceSet.ThreeWayRotation(neighbors);

					break;

				// cross
				case 4:
					// get a random piece from the set
					piecePrefab = pieceSet.GetRandomFourWayPrefab(_rand);

					// instantiate and set parent to the entry
					piece = Instantiate(piecePrefab, entry.transform);

					// set the rotation based on neighbor orientation
					piece.transform.rotation = pieceSet.FourWayRotation(neighbors);
					break;

				case 0:
					break;

				default:
					XZCoordinate coord = entry.coordinate;
					Debug.LogError("x:" + coord.x + " z:" + coord.z + " supposely has " + numNeigbors + " neighbors?");
					break;
			}

			piecesGenerated++;

			if (piecesGenerated % piecesPerFrame == 0) {
				yield return new WaitForEndOfFrame();
			}
		}
	}

	/// <summary>
	/// Creates an array of bool to show wether there is a neighbor.
	/// Clockwise
	/// </summary>
	/// <param name="entry"></param>
	/// <returns></returns>
	private bool[] GetNeighborBools(RoomEntry entry, out int numNeigbors) {
		bool[] n = new bool[4];

		int num = 0;

		XZCoordinate cord = entry.coordinate;

		if (currentFloorParent.roomDict.ContainsKey(cord.UpCoordinate().ToString())) {
			num++;
			n[0] = true;
		}

		if (currentFloorParent.roomDict.ContainsKey(cord.RightCoordinate().ToString())) {
			num++;
			n[1] = true;
		}

		if (currentFloorParent.roomDict.ContainsKey(cord.DownCoordinate().ToString())) {
			num++;
			n[2] = true;
		}

		if (currentFloorParent.roomDict.ContainsKey(cord.LeftCoordinate().ToString())) {
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
	public RoomEntry GetRandomRoomEntry() {
		return currentFloorParent.roomList[_rand.Next(currentFloorParent.roomList.Count)];
	}

	/// <summary>
	/// Generates a random cord around the point
	/// </summary>
	/// <param name="point">center point</param>
	/// <param name="minDist">min distance from the center point</param>
	/// <param name="maxDist">max distance from the center point</param>
	/// <returns></returns>
	private XZCoordinate GenerateRandomCord(XZCoordinate point, int minDist, int maxDist) {
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
	private XZCoordinate FindClosestCord(XZCoordinate goal, params XZCoordinate[] list) {
		XZCoordinate closest = goal;
		int minDist = int.MaxValue;

		for (int i = 0; i < list.Length; i++) {

			if (goal == list[i]) {
				continue;
			}

			// distance
			int dist = list[i].BlockDistance(goal);
			
			// compare
			if (dist < minDist) {
				closest = list[i];
				minDist = dist;
			}
		}

		return closest;
	}

	/// <summary>
	/// Gets the current floor set based on the floor number
	/// </summary>
	/// <returns></returns>
	private FloorPieceSet GetCurrentSet() {
		int index = PlayerProgress.currentFloor / 3;
		index = index >= pieceSets.Length ? pieceSets.Length - 1 : index;
		return pieceSets[index];
	}

	/// <summary>
	/// Generates a seed number based on seed string and floor
	/// </summary>
	/// <param name="seed">seed string</param>
	/// <param name="floor">floor number</param>
	/// <returns></returns>
	private int GenerateSeedNumber(string seed, int floor) {
		return (seed + "f:" + floor).GetHashCode();
	}
}
