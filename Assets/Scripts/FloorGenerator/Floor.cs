using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Floor : MonoBehaviour {

	[Header("Room")]

	/// <summary>
	/// List of room entries generated
	/// </summary>
	public List<RoomEntry> roomList;

	/// <summary>
	/// A dictionary containing room entries
	/// </summary>
	public Dictionary<string, RoomEntry> roomDict;

	/// <summary>
	/// A room that act as the floor's entrance
	/// </summary>
	public RoomEntry entrance;

	/// <summary>
	/// A room that act as the floor's exit
	/// </summary>
	public RoomEntry exit;

	[Header("Others")]

	public int floorNumber;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		roomList = new List<RoomEntry>();

		roomDict = new Dictionary<string, RoomEntry>();
	}
}
