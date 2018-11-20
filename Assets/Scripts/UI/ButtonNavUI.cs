using UnityEngine;
using UnityEngine.UI;

public class ButtonNavUI : MonoBehaviour {

	/// <summary>
	/// Buttons
	/// </summary>
	public Button[] buttons;

	/// <summary>
	/// Selection indicator
	/// </summary>
	public GameObject indicator;

	/// <summary>
	/// Delay for input
	/// </summary>
	[Range(0.05f, 0.5f)]
	public float delay = 0.15f;

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
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		ButtonConfirmUpdate();

		ButtonNavigationUpdate();
	}

	/// <summary>
	/// Update loop section for navigation
	/// </summary>
	private void ButtonNavigationUpdate() {
		float vert = Input.GetAxis("Menu Vertical");
		
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
	/// Update loop section for submit
	/// </summary>
	private void ButtonConfirmUpdate() {
		float submit = Input.GetAxis("Menu Confirm");

		if (submit != 0) {
			buttons[_currentIndex].onClick.Invoke();
		}
	}

	/// <summary>
	/// Shows the next panel
	/// </summary>
	public void NextButton() {
		int index = _currentIndex;

		for (int i = 0; i < buttons.Length - 1; i++) {
			index += 1 + buttons.Length;
			index = index % buttons.Length;

			if (buttons[index].interactable) { 
				break;
			}
		}

		_currentIndex = index;
		SetIndicatorPosition(_currentIndex);
	}

	/// <summary>
	/// Shows the previous panel
	/// </summary>
	public void PrevButton() {
		int index = _currentIndex;
		
		for (int i = 0; i < buttons.Length - 1; i++) {
			index += buttons.Length - 1;
			index = index % buttons.Length;

			if (buttons[index].interactable) { 
				break;
			}
		}

		_currentIndex = index;
		SetIndicatorPosition(_currentIndex);
	}

	/// <summary>
	/// Sets the indicator position
	/// </summary>
	/// <param name="i"></param>
	private void SetIndicatorPosition(int i) {
		indicator.transform.SetParent(buttons[i].transform, false);
	}
}
