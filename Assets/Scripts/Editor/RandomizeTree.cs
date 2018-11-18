using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomizeTree : MonoBehaviour {

	public static System.Random rand = new System.Random();

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		RandomScale();
	}

	void RandomScale() {
		Vector3 scale = this.transform.localScale;

		scale.y = rand.Next(11, 15) + (rand.Next(0, 100) / 100f);

		this.transform.localScale = scale;
	}
}
