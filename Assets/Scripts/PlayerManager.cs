using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	[Header("Player")]
	/// <summary>
	/// Player prefab
	/// </summary>
	public GameObject playerPrefab;

    /// <summary>
    /// Reference to the health bar prefab.
    /// </summary>
    public GameObject healthBarPrefab;

	/// <summary>
	/// The current local player gameobject instance
	/// </summary>
	public GameObject localPlayer;

	[Header("Camera")]
	/// <summary>
	/// the following camera
	/// </summary>
	public CopyTargetPosition followingCamera;

	/// <summary>
	/// Player's stats
	/// </summary>
	private CharacterStats _stats;

	/// <summary>
	/// Player's class
	/// </summary>
	private BaseClass _class;

    private StationaryResourcesUI _healthbar;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		//localPlayer = Instantiate(playerPrefab);

		_stats = GetComponent<CharacterStats>();

        Instantiate(healthBarPrefab, transform);

		// _class = GetComponent<BaseClass>();

		FloorGenerator.OnFloorGenerated += MovePlayerToEntrance;

		FloorGenerator.OnBeginGeneration += RemovePlayerOnNewFloor;
	}

	/// <summary>
	/// removes player on floor generation
	/// </summary>
	/// <param name="floor"></param>
	private void RemovePlayerOnNewFloor(Floor floor) {
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
	private void CreatePlayer(bool destoryExisting, Vector3 position) {
		if (localPlayer != null && destoryExisting) {
			Destroy(localPlayer);
			localPlayer = null;
		}
		
		if (localPlayer == null) {
			localPlayer = Instantiate(playerPrefab, position, Quaternion.identity);
			followingCamera.target = localPlayer.transform;
		}
	}

	/// <summary>
	/// moves the player to the entrance of the floor
	/// </summary>
	private void MovePlayerToEntrance(Floor currentFloor) {
		if (localPlayer == null) {
			Vector3 entrancePos = currentFloor.entrance.transform.position;

			entrancePos.y += 5f;

			//localPlayer.transform.position = entrancePos;

			CreatePlayer(false, entrancePos);
		}
	}
}
