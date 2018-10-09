using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorExitTrigger : MonoBehaviour {

	/// <summary>
	/// The target generator to use for the next floor
	/// </summary>
	public FloorGenerator TargetGenerator;

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other) {

		// check for player entry
		if (other.tag != "Player") {
			return;
		}

		// disable to avoid multiple calls
		enabled = false;

		// delete old floor and generate a new one
		TargetGenerator.GenerateNewFloor(new System.Random().Next(0, int.MaxValue - 1), true);
	}
}
