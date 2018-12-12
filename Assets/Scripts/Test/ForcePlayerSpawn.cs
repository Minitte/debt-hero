using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ForcePlayerSpawn : MonoBehaviour {

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		PlayerManager.instance.CreatePlayer(true);

		PlayerManager.instance.localPlayer.GetComponent<NavMeshAgent>().Warp(Vector3.zero);
	}
}
