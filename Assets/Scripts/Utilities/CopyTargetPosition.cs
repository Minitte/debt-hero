using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTargetPosition : MonoBehaviour {

	/// <summary>
	/// Target to follow
	/// </summary>
	public Transform target;

	/// <summary>
	/// The axis to follow.
	/// Example: (1, 0, 0) means follow x axis only
	/// </summary>
	public Vector3 axisToCopy = new Vector3(1, 1, 1);

	/// <summary>
	/// Offset from the target
	/// </summary>
	/// <returns></returns>
	public Vector3 offset = new Vector3(0, 30f, 0);

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
		if (target == null) {
			return;
		}

		Vector3 pos = target.transform.position;

		pos += offset;

		pos.Scale(axisToCopy);

		transform.position = pos;
	}
}
