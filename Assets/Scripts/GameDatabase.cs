using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : MonoBehaviour {

	/// <summary>
	/// Singleton/easy access
	/// </summary>
	public static GameDatabase instance;

	/// <summary>
	/// Item database
	/// </summary>
	public ItemDatabase itemDatabase;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		} else {
			Debug.Log("Found two GameDatabase Instances.. Destorying new one");
			Destroy(this.gameObject);
			return;
		}


	}
}
