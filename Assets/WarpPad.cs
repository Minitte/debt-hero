using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPad : MonoBehaviour {

	public GameObject teleportMenuPrefab;

	private GameObject _teleportMenu;

	private GameObject _canvas;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_canvas = GameObject.Find("Canvas");

		_teleportMenu = Instantiate(teleportMenuPrefab, _canvas.transform);
		_teleportMenu.SetActive(false);
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			_teleportMenu.SetActive(true);
		}
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _teleportMenu.SetActive(false);
        }
    }
}
