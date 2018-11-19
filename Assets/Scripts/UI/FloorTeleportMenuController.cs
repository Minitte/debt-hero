using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloorTeleportMenuController : MonoBehaviour {

	/// <summary>
	/// Floor button list
	/// </summary>
	public Button[] floorButtons;

	/// <summary>
	/// Floor destination level list
	/// </summary>
	public int[] floorDestinations;

	/// <summary>
	/// Player manager reference
	/// </summary>
	private PlayerManager _playerManager;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_playerManager = PlayerManager.instance;
		
		UpdateButtons();
	}

	/// <summary>
	/// Updates button visability based on the floor reached
	/// </summary>
	private void UpdateButtons() {
		for (int i = 0; i < floorButtons.Length; i++) {
			floorButtons[i].interactable = floorDestinations[i] <= GameState.floorReached;
		}
	}

	/// <summary>
	/// Changes the scene to tower
	/// </summary>
	public void ChangeSceneToTower(int floor) {
		GameState.currentFloor = floor;
		SceneManager.LoadScene("The Tower");
	}
	
	/// <summary>
	/// Changes the scene to town
	/// </summary>
	public void ChangeSceneToTown() {
		GameState.currentFloor = 0;
		SceneManager.LoadScene("TownScene");
	}
}
