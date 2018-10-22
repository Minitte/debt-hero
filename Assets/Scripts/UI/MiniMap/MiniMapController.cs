using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour {

	[Header("Map Object Spec")]
	/// <summary>
	/// prefab of a map object representing a room
	/// </summary>
	public GameObject mapObjectPrefab;

	/// <summary>
	/// size of a map object
	/// </summary>
	public float mapObjectSize;

	/// <summary>
	/// The space between map objects
	/// </summary>
	public float mapObjectPadding;

	/// <summary>
	/// Offset for the map. Usually half of the width and height
	/// </summary>
	public Vector2 mapOffset;

	[Header("Others")]

	/// <summary>
	/// Parent for map objects
	/// </summary>
	public Transform mapObjectParent;

	/// <summary>
	/// A dictionary of map objects
	/// </summary>
	private Dictionary<XZCoordinate, GameObject> mapObjects;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		mapObjects = new Dictionary<XZCoordinate, GameObject>();

		FloorGenerator.OnFloorGenerated += GenerateMapObjects;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {

		if (PlayerManager.instance == null || PlayerManager.instance.localPlayer == null) {
			return;
		}

		Vector3 playerPos = PlayerManager.instance.localPlayer.transform.position;

		float x = -playerPos.x;
		float y = -playerPos.z;

		// by room piece size
		x /= 25f;
		y /= 25f;

		x *= mapObjectSize + mapObjectPadding;
		y *= mapObjectSize + mapObjectPadding;

		x -= mapOffset.x;
		y -= mapOffset.y;

		mapObjectParent.GetComponent<RectTransform>().localPosition = new Vector2(x, y);
 
	}

	/// <summary>
	/// Generates a map object for each room in the floor
	/// </summary>
	/// <param name="floor"></param>
	/// <param name="random"></param>
	private void GenerateMapObjects(Floor floor, System.Random random) {
		foreach (RoomEntry room in floor.roomList) {
			GameObject mapobject = Instantiate(mapObjectPrefab, mapObjectParent);

			mapobject.transform.localPosition = (room.coordinate.ToVector2() * (mapObjectSize + mapObjectPadding));

			mapObjects.Add(new XZCoordinate(room.coordinate), mapobject);
		}
	}
}
