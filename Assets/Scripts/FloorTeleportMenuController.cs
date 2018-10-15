using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloorTeleportMenuController : MonoBehaviour {

	public Button[] floorButtons;

	public int[] floorDestinations;

	public PlayerManager playerManager;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
		updateButtons();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		float cancel = Input.GetAxis("Cancel");

		if (cancel != 0) {
			this.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Updates button visability based on the floor reached
	/// </summary>
	private void updateButtons() {
		for (int i = 0; i < floorButtons.Length; i++) {
			floorButtons[i].interactable = floorDestinations[i] <= playerManager.floorReached;
		}
	}

	/// <summary>
	/// Changes the scene to tower
	/// </summary>
	public void ChangeSceneToTower() {
		SceneManager.LoadScene("Tower");
	}
	
	/// <summary>
	/// Changes the scene to town
	/// </summary>
	public void ChangeSceneToTown() {
		SceneManager.LoadScene("Town");
	}
}
