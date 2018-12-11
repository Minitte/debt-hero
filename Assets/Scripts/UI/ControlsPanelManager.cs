using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPanelManager : MonoBehaviour {
    /// <summary>
    /// Reference to the exit button.
    /// Used to close control screen.
    /// </summary>
    public Button backButton;
    /// <summary>
    /// Used to go to next page.
    /// Because there is only two pages
    /// It will just toggle between Page 1 and 2
    /// </summary>
    public Button setPageButton;

    /// <summary>
    /// Used To handle setting the text.
    /// </summary>
    public ControlUIText controlUI;

    /// <summary>
    /// Index is used to represent which button the user is selecting.
    /// </summary>
    public int index;

    /// <summary>
    /// Cool down flag
    /// </summary>
    private bool _cooldown;

    private float _time;
    // Use this for initialization
    void Start () {
        index = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (_cooldown) {
            _time += Time.deltaTime;

            if (_time >= 0.15f) {
                _cooldown = false;
                _time = 0f;
            }

            return;
        }
        PS4controls();
	}

    public void CloseControlPanel() {
        this.gameObject.SetActive(false);
    }

    public void OpenControlPanel() {
        this.gameObject.SetActive(true);
    }

    public void SetPage() {
        if (controlUI.index == 0) {
            controlUI.index = 1;
        } else if (controlUI.index == 1) {
            controlUI.index = 0;
        }
        controlUI.controlUI.text = controlUI.controls[controlUI.index];
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

    }

    /// <summary>
    /// Ps4 controls for Control Panel.
    /// </summary>
    public void PS4controls() {

        //Left and Right Controls.
        float hori = Input.GetAxis("Menu Horizontal");

        if(hori < 0) {
            index = 0;
            _cooldown = true;
            // return;
        } else if (hori > 0) {
            index = 1;
            _cooldown = true;
            // return;
        } else if (hori == 0) {
            //index = -1;
            //_cooldown = true;
            // return;
        }

        if (index == 0) {
            backButton.Select();
        } else if (index == 1) {
            setPageButton.Select();
        }

        //Pressing Button.
        if (Input.GetAxis("Menu Confirm") != 0) {
            if (index == 0) {
                CloseControlPanel();
            } else if (index == 1) {
                SetPage();
            }

            _cooldown = true;
        }
    }
}
