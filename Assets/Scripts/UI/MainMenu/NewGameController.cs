using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewGameController : MonoBehaviour {

	/// <summary>
	/// Array of letters for the name creation
	/// Top Row
	/// </summary>
	public LetterController[] letters;

	/// <summary>
	/// Array of classes for the class selection
	/// Middle Row
	/// </summary>
	public ClassCardController[] classes;

	/// <summary>
	/// Buttons at the bottom
	/// Buttom Row
	/// </summary>
	public Button[] buttons;

	/// <summary>
	/// Class card selected
	/// </summary>
	private int _selectedClassIndex;

	/// <summary>
	/// Which level/row the ui control is at
	/// </summary>
	private int _uiLevel;

	/// <summary>
	/// UI level horzintal
	/// </summary>
	private int _uiIndex;

	/// <summary>
	/// State of picking a letter
	/// </summary>
	private bool _pickingLetter;

	/// <summary>
	/// Cool down flag
	/// </summary>
	private bool _cooldown;

	/// <summary>
	/// Time for cooldown
	/// </summary>
	private float _time;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		letters[0].HighLight();
		
		for (int i = 0; i < classes.Length; i++) {
			classes[i].HideHighlight();
			classes[i].HideBorder();
		}

		classes[0].ShowHighlight();
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		_cooldown = true;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if (_cooldown) {
			_time += Time.deltaTime;

			if (_time > 0.15f) {
				_time = 0;
				_cooldown = false;
			} else {
				return;
			}
		}

		CategoryControls();

		if (_uiLevel == 0) {
			NameControls();
		} else if (_uiLevel == 1) {
			ClassControls();
		} else if (_uiLevel == 2) {
			ButtonControls();
		}
	}

	/// <summary>
	/// Handles controls for going between the categories
	/// </summary>
	private void CategoryControls() {
		if (!_pickingLetter) {
			float vert = Input.GetAxis("Menu Vertical");

			// up and down menu control
			if (vert < 0) {
				if (_uiLevel < 2) {
					
					// clean up before leaving the category
					if (_uiLevel == 0) {
						letters[_uiIndex].UnHighLight();
					} else if (_uiLevel == 1) {
						classes[_uiIndex].HideBorder();
					}

					_uiLevel++;

					_uiIndex = 0;

					// setup moving on to a category
					if (_uiLevel == 1) {
						_uiIndex = _selectedClassIndex;
						classes[_uiIndex].ShowBorder();
					} else if (_uiLevel == 2) {
						EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
					}
				}
				_cooldown = true;

			} else if (vert > 0) {
				if (_uiLevel > 0) {

					// clean up before leaving the category
					if (_uiLevel == 1) {
						classes[_uiIndex].HideBorder();
					} else if (_uiLevel == 2) {
						EventSystem.current.SetSelectedGameObject(null);
					}

					_uiLevel--;

					_uiIndex = 0;

					// setup moving on to a category
					if (_uiLevel == 0) {
						letters[0].HighLight();
					} else if (_uiLevel == 1) {
						_uiIndex = _selectedClassIndex;
						classes[_uiIndex].ShowBorder();
					}
				}
				_cooldown = true;
			}
		}
	}

	/// <summary>
	/// Controls for the name section
	/// </summary>
	private void NameControls() {
		float horz = Input.GetAxis("Menu Horizontal");
		float confirm = Input.GetAxis("Menu Confirm");

		if (!_pickingLetter) {
			if (horz > 0) {
				_uiIndex++;
				_uiIndex = _uiIndex >= letters.Length ? letters.Length - 1 : _uiIndex;

				letters[_uiIndex - 1].UnHighLight();
				letters[_uiIndex].HighLight();

				_cooldown = true;
			} else if (horz < 0) {
				_uiIndex--;
				_uiIndex = _uiIndex < 0 ? 0 : _uiIndex;

				letters[_uiIndex + 1].UnHighLight();
				letters[_uiIndex].HighLight();

				_cooldown = true;
			}
		}

		if (confirm != 0) {
			_pickingLetter = !_pickingLetter;

			if (_pickingLetter) {
				letters[_uiIndex].ShowLetterButtons();
				letters[_uiIndex].enabled = true;
			} else {
				letters[_uiIndex].HideLetterButtons();
				letters[_uiIndex].enabled = false;
			}

			_cooldown = true;
		}

	}

	/// <summary>
	/// Handles the class card selection controls
	/// </summary>
	public void ClassControls() {
		float horz = Input.GetAxis("Menu Horizontal");

		if (horz > 0) {
			if (_uiIndex < classes.Length - 1) {
				_uiIndex++;
				_selectedClassIndex = _uiIndex;

				classes[_uiIndex - 1].HideHighlight();
				classes[_uiIndex - 1].HideBorder();
				classes[_uiIndex].ShowHighlight();
				classes[_uiIndex].ShowBorder();
			}

			_cooldown = true;
		} else if (horz < 0) {
			if (_uiIndex > 0) {
				_uiIndex--;
				_selectedClassIndex = _uiIndex;

				classes[_uiIndex + 1].HideHighlight();
				classes[_uiIndex + 1].HideBorder();
				classes[_uiIndex].ShowHighlight();
				classes[_uiIndex].ShowBorder();
			}

			_cooldown = true;
		}
	}

	/// <summary>
	/// Handles button controls
	/// </summary>
	public void ButtonControls() {
		float horz = Input.GetAxis("Menu Horizontal");
		float confirm = Input.GetAxis("Menu Confirm");

		if (horz > 0) {
			if (_uiIndex < buttons.Length - 1) {
				_uiIndex++;

				EventSystem.current.SetSelectedGameObject(buttons[_uiIndex].gameObject);
			}

			_cooldown = true;
		} else if (horz < 0) {
			if (_uiIndex > 0) {
				_uiIndex--;

				EventSystem.current.SetSelectedGameObject(buttons[_uiIndex].gameObject);
			}

			_cooldown = true;
		}

		if (confirm != 0) {
			buttons[_uiIndex].onClick.Invoke();
		}
	}

	/// <summary>
	/// Assembles the name from the letter
	/// </summary>
	/// <returns></returns>
	public string AssembleName() {
		string name = "";

		foreach (LetterController l in letters) {
			name += l.letter;
		}

		return name;
	}

	/// <summary>
	/// Starts the game
	/// </summary>
	public void StartNewGame() {

		PlayerProgress.name = AssembleName();
		PlayerProgress.className = classes[_selectedClassIndex].className;
        /*DestroyImmediate(PlayerManager.instance.gameObject);
        DestroyImmediate(EventManager.instance.gameObject);
        DestroyImmediate(GameDatabase.instance.gameObject);
       
        PlayerManager.instance = null;
        EventManager.instance = null;
        TimeManager.instance = null;
        GameDatabase.instance = null;
        */

		PlayerProgress.currentFloor = 0;

		PlayerProgress.floorReached = 0;

		TimeManager.instance.currentHour = 0;
		TimeManager.instance.currentMinute = 0;
		TimeManager.instance.currentTime = 0;
		TimeManager.instance.days = 0;

        SceneManager.LoadScene("TownScene");
	}

	/// <summary>
	/// Sets the current class index
	/// </summary>
	/// <param name="index"></param>
	public void SetClass(int index) {
		classes[_selectedClassIndex].HideHighlight();
		classes[index].ShowHighlight();
		
		_selectedClassIndex = index;

	}
}
