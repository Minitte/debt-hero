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

	[Header("Others")]

	/// <summary>
	/// Parent for map objects
	/// </summary>
	public Transform mapObjectParent;

	/// <summary>
	/// A list of map objects
	/// </summary>
	public List<GameObject> mapObjects;

	 /// <summary>
	 /// Awake is called when the script instance is being loaded.
	 /// </summary>
	void Awake() {
		mapObjects = new List<GameObject>();

		FloorGenerator.OnFloorGenerated += GenerateMapObjects;
	}


	/// <summary>
	/// Generates a map object for each room in the floor
	/// </summary>
	/// <param name="floor"></param>
	/// <param name="random"></param>
	private void GenerateMapObjects(Floor floor, System.Random random) {
		foreach (RoomEntry room in floor.roomList) {
			GameObject mapobject = Instantiate(mapObjectPrefab, mapObjectParent);

			mapobject.transform.localPosition = room.coordinate.ToVector2() * (mapObjectSize + mapObjectPadding);
		}
	}
}
