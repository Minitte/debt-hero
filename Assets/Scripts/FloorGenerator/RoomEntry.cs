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
		EXIT
	}

	/// <summary>
	/// this room's type
	/// </summary>
	public RoomType type;

	/// <summary>
	/// x cordinate
	/// </summary>
	public int xCord;

	/// <summary>
	/// z cordinate
	/// </summary>
	public int zCord;

	/// <summary>
	/// Combines x and z into a Vector2 object.
	/// Vector2's x = x while Vector2's y = z
	/// </summary>
	/// <value></value>
	public Vector2 cordV2 {
		get {
			return new Vector2(xCord, zCord);
		}
	}
}
