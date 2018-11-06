using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class InGameMenuController : MonoBehaviour {

	public delegate void MenuShownEvent(bool shown);

	public static event MenuShownEvent OnMenuShown;

	[Header("Menu Panels")]
	/// <summary>
	/// All of the panels in the menu
	/// </summary>
	public GameObject[] menuPanels;

	public string[] panelNames;

	[Header("Buttons")]

	/// <summary>
	/// Button that goes to the next menu
	/// </summary>
	public Button nextButton;

	/// <summary>
	/// Button that goes to the previous menu
	/// </summary>
	public Button prevButton;

	[Header("Timing")]

	/// <summary>
	/// Delay before accepting the next esc call
	/// </summary>
	public float delay = 0.2f;

	/// <summary>
	/// Time for delay tracking
	/// </summary>
	private float _time;

	/// <summary>
	/// if menu key is on cooldown / blocked
	/// </summary>
	private bool _menuKeyBlocked;

	/// <summary>
	/// if menu key is hold down
	/// </summary>
	private bool _MenuKeyDown;

	/// <summary>
	/// Flag for the state of the whole menu
	/// </summary>
	private bool _hideMenu;

	/// <summary>
	/// current index of the current panel
	/// </summary>
	private int _currentIndex;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		HideMenu();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float start = Input.GetAxis("Start");

		float leftMenu = Input.GetAxis("Left Menu");

		float rightMenu = Input.GetAxis("Right Menu");

		float keySum = (start + leftMenu + rightMenu);

		// reset if no key down
		if (_MenuKeyDown && keySum == 0) {
			_MenuKeyDown = false;
		}

		// counter for menu key blocking
		if (_menuKeyBlocked) {
			_time += Time.deltaTime;

			if (_time > delay) {
				_time = 0f;
				_menuKeyBlocked = false;
			}
		}

		// detecting a new key
		if (keySum != 0 && !_menuKeyBlocked && !_MenuKeyDown) {
			_menuKeyBlocked = true;
			_MenuKeyDown = true;
			
			if (start != 0) {
				ToggleMenu();
			} else if (leftMenu != 0) {
				PrevPanel();
			} else if (rightMenu != 0) {
				NextPanel();
			}
		}
	}

	/// <summary>
	/// Toggle between hiding and showing the menu
	/// </summary>
	public void ToggleMenu() {
		_hideMenu = !_hideMenu;

		if (_hideMenu) {
			HideMenu();
		} else {
			OpenMenu();
		}
	}

	/// <summary>
	/// Shows the current panel
	/// </summary>
	public void OpenMenu() {
		_hideMenu = false;

		ShowPanel(_currentIndex);

		nextButton.gameObject.SetActive(true);
		prevButton.gameObject.SetActive(true);

		if (OnMenuShown != null) {
			OnMenuShown(true);
		}
	}

	/// <summary>
	/// Hides all of the panels
	/// </summary>
	public void HideMenu() {
		_hideMenu = true;

		ShowPanel(-1);

		nextButton.gameObject.SetActive(false);
		prevButton.gameObject.SetActive(false);

		if (OnMenuShown != null) {
			OnMenuShown(false);
		}
	}

	/// <summary>
	/// Shows the desired panel and disables the rest
	/// </summary>
	/// <param name="index">Index of the panel. values outside of the length of the list and nagative will hide all</param>
	public void ShowPanel(int index) {

		if (index < menuPanels.Length && index >= 0) {
			_currentIndex = index;
		}

		for (int i = 0; i < menuPanels.Length; i++) {
			menuPanels[i].SetActive(i == index);
		}

		UpdateButtonNames();
	}

	/// <summary>
	/// Shows the next panel
	/// </summary>
	public void NextPanel() {
		_currentIndex += 1 + menuPanels.Length;
		_currentIndex = _currentIndex % menuPanels.Length;

		ShowPanel(_currentIndex);
	}

	/// <summary>
	/// Shows the previous panel
	/// </summary>
	public void PrevPanel() {
		_currentIndex += menuPanels.Length - 1;
		_currentIndex = _currentIndex % menuPanels.Length;

		ShowPanel(_currentIndex);
	}

	/// <summary>
	/// Updates the next/prev button text to match the panel names
	/// </summary>
	private void UpdateButtonNames() {
		TextMeshProUGUI nextText = nextButton.transform.GetComponentInChildren<TextMeshProUGUI>();
		TextMeshProUGUI prevText = prevButton.transform.GetComponentInChildren<TextMeshProUGUI>();

		int nextIndex = _currentIndex + 1 + menuPanels.Length;
		nextIndex = nextIndex % menuPanels.Length;
	
		int prevIndex = _currentIndex + menuPanels.Length - 1;
		prevIndex = prevIndex % menuPanels.Length;

		nextText.text = panelNames[nextIndex];
		prevText.text = panelNames[prevIndex];
	}
}
