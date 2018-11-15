using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOpenShopPanel : MonoBehaviour {
	
	public Dialog dialog;

	public GameObject shopPanel;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		dialog.OnEndOfDialog += EnableShopPanel;
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy() {
		dialog.OnEndOfDialog -= EnableShopPanel;
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		dialog.OnEndOfDialog += EnableShopPanel;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		dialog.OnEndOfDialog -= EnableShopPanel;
	}

	/// <summary>
	/// Enables the shop panel
	/// </summary>
	private void EnableShopPanel() {
		shopPanel.SetActive(true);
	}
}
