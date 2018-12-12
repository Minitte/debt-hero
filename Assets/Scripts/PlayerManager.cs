﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;

	[Header("Player")]
	/// <summary>
	/// Player prefab
	/// </summary>
	public GameObject playerPrefab;

    /// <summary>
    /// The current local player gameobject instance
    /// </summary>
    public GameObject localPlayer;
	
	/// <summary>
	/// Player's inventory
	/// </summary>
	private CharacterInventory _inventory;

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

		if (instance == null) {
			instance = this;
		} else {
			Debug.Log("Found two PlayerManager Instances.. Destorying new one");
			Destroy(this.gameObject);
			return;
		}

		//localPlayer = Instantiate(playerPrefab);

		_stats = GetComponent<CharacterStats>();

		_inventory = GetComponent<CharacterInventory>();

		_class = GetComponent<BaseClass>();

		FloorGenerator.OnFloorGenerated += MovePlayerToEntrance;
		FloorGenerator.OnFloorGenerated += RebindItemReferences;

		FloorGenerator.OnBeginGeneration += RemovePlayerOnNewFloor;

		DontDestroyOnLoad(this.gameObject);
    }

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy() {
		FloorGenerator.OnFloorGenerated -= MovePlayerToEntrance;
		FloorGenerator.OnFloorGenerated -= RebindItemReferences;

		FloorGenerator.OnBeginGeneration -= RemovePlayerOnNewFloor;
	}

	/// <summary>
	/// Resets the owner of items
	/// </summary>
	/// <param name="currentFloor">Unused, delegate requirements</param>
	/// <param name="rand">Unused, delegate requirements</param>
	private void RebindItemReferences(Floor currentFloor, System.Random rand) {
		if (localPlayer == null) {
			Debug.Log("Missing local player reference. Skipping rebinding");
			return;
		}
		
		ItemBase[] items = _inventory.GetAllItems();

		foreach (ItemBase item in items) {
			item.owner = localPlayer.GetComponent<BaseCharacter>();
		}
	}

	/// <summary>
	/// removes player on floor generation
	/// </summary>
	/// <param name="floor"></param>
	private void RemovePlayerOnNewFloor(Floor floor, System.Random rand) {
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
	public void CreatePlayer(bool destoryExisting) {
		if (localPlayer != null && destoryExisting) {
			Destroy(localPlayer);
			localPlayer = null;
		}
		
		if (localPlayer == null) {
			localPlayer = Instantiate(playerPrefab);
			Camera.main.GetComponent<CopyTargetPosition>().target = localPlayer.transform;
        }
    }

	/// <summary>
	/// moves the player to the entrance of the floor
	/// </summary>
	private void MovePlayerToEntrance(Floor currentFloor, System.Random rand) {
		if (localPlayer == null) {
			Vector3 entrancePos = currentFloor.entrance.transform.position;

			entrancePos.y += 5f;

			CreatePlayer(true);

			localPlayer.GetComponent<NavMeshAgent>().Warp(entrancePos);
		}
	}
}
