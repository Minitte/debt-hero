using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntry : MonoBehaviour {

	/// <summary>
	/// Room type enum
	/// </summary>
	public enum RoomType {
		NORMAL,
		ENTRANCE,
		EXIT,
		SAFE
	}

	/// <summary>
	/// this room's type
	/// </summary>
	public RoomType type;

	/// <summary>
	/// coordinate of the room entry
	/// </summary>
	public XZCoordinate coordinate;
}
