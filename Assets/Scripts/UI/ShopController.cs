using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float closeShop = Input.GetAxis("System Menu Open");
		
		if (closeShop != 0) {
			this.gameObject.SetActive(false);
		}
	}
}
