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

		_class = GetComponent<BaseClass>();
	}
}
