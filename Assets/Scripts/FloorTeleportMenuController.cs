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
	/// Selection indicator
	/// </summary>
	public GameObject indicator;

	/// <summary>
	/// Delay for input
	/// </summary>
	[Range(0.05f, 0.5f)]
	public float delay;

	/// <summary>
	/// Player manager reference
	/// </summary>
	private PlayerManager _playerManager;

	/// <summary>
	/// current menu index
	/// </summary>
	private int _currentIndex;

	/// <summary>
	/// menu is blocked from input
	/// </summary>
	private bool _menuBlocked;

	/// <summary>
	/// Time var for counting delay
	/// </summary>
	private float _time;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_playerManager = PlayerManager.instance;
		
		UpdateButtons();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update(){
		float vert = Input.GetAxis("Menu Nav Vertical");
		
		if (Mathf.Abs(vert) > 0.1) {
			vert = Mathf.Sign(vert);
		} else {
			vert = 0f;
			_menuBlocked = false;
			_time = 0;
		}

		// menu cooldown
		if (_menuBlocked) {
			_time += Time.deltaTime;

			if (_time > delay) {
				_menuBlocked = false;
				_time = 0;
			}

			return;
		}

		if (vert == 1) {
			_menuBlocked = true;
			NextButton();
		} else if (vert == -1) {
			_menuBlocked = true;
			PrevButton();
		}
	}

	/// <summary>
	/// Shows the next panel
	/// </summary>
	public void NextButton() {
		_currentIndex += 1 + floorButtons.Length;
		_currentIndex = _currentIndex % floorButtons.Length;

		SetIndicatorPosition(_currentIndex);
	}

	/// <summary>
	/// Shows the previous panel
	/// </summary>
	public void PrevButton() {
		_currentIndex += floorButtons.Length - 1;
		_currentIndex = _currentIndex % floorButtons.Length;
	
		SetIndicatorPosition(_currentIndex);
	}

	/// <summary>
	/// Sets the indicator position
	/// </summary>
	/// <param name="i"></param>
	private void SetIndicatorPosition(int i) {
		indicator.transform.SetParent(floorButtons[i].transform, false);
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
		SceneManager.LoadScene("Tower");
	}
	
	/// <summary>
	/// Changes the scene to town
	/// </summary>
	public void ChangeSceneToTown() {
		GameState.currentFloor = 0;
		SceneManager.LoadScene("Town");
	}
}
