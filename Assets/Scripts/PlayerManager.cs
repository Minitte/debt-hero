using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	/// <summary>
	/// Player prefab
	/// </summary>
	public GameObject playerPrefab;

	/// <summary>
	/// The current local player gameobject instance
	/// </summary>
	public GameObject localPlayer;

	/// <summary>
	/// Player's stats
	/// </summary>
	private CharacterStats _stats;

	/// <summary>
	/// Player's class
	/// </summary>
	private BaseClass _class;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		//localPlayer = Instantiate(playerPrefab);

		_stats = GetComponent<CharacterStats>();

		// _class = GetComponent<BaseClass>();

		FloorGenerator.onFloorGenerated += movePlayerToEntrance;

		FloorGenerator.onBeginGeneration += removePlayerOnNewFloor;
	}

	/// <summary>
	/// removes player on floor generation
	/// </summary>
	/// <param name="floor"></param>
	private void removePlayerOnNewFloor(Floor floor) {
		if (localPlayer != null) {
			Destroy(localPlayer);
			localPlayer = null;
		}
	}

	/// <summary>
	/// creates or replaces the player instance
	/// </summary>
	/// <param name="destoryExisting"></param>
	/// <param name="position"></param>
	private void createPlayer(bool destoryExisting, Vector3 position) {
		if (localPlayer != null && destoryExisting) {
			Destroy(localPlayer);
			localPlayer = null;
		}
		
		if (localPlayer == null) {
			localPlayer = Instantiate(playerPrefab, position, Quaternion.identity);
		}
	}

	/// <summary>
	/// moves the player to the entrance of the floor
	/// </summary>
	private void movePlayerToEntrance(Floor currentFloor) {
		if (localPlayer == null) {
			Vector3 entrancePos = currentFloor.entrance.transform.position;

			entrancePos.y += 5f;

			//localPlayer.transform.position = entrancePos;

			createPlayer(false, entrancePos);
		}
	}
}
