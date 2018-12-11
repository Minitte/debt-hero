using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnGameStateIsUI : MonoBehaviour {

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		GameState.OnStateChanged += HandleGameStateChange;
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy() {
		GameState.OnStateChanged -= HandleGameStateChange;
	}

	/// <summary>
	/// Handles game state changed event
	/// </summary>
	private void HandleGameStateChange() {
		if (GameState.InAnyUIState()) {
			this.gameObject.SetActive(false);
		} else {
			this.gameObject.SetActive(true);
		}
	}
}
