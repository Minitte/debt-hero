using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPanelManager : MonoBehaviour {
    public Button backButton;
    public Button setPage;
    public ControlUIText controlUI;
    public int index;
	// Use this for initialization
	void Start () {
        index = -1;
	}
	
	// Update is called once per frame
	void Update () {
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

    public void PS4controls() {
        if (Application.platform == RuntimePlatform.PS4) {
            float hori = Input.GetAxis("Menu Horizontal");

            if(hori < 0) {
                index = 0;
            }else if(hori > 0) {
                index = 1;
            }else if(hori == 0) {
                index = -1;
            }

            if (index == 0) {
                backButton.Select();
            }else if(index == 1) {
                setPage.Select();
            }

            if (Input.GetAxis("Menu Confirm") != 0) {
                if(index == 0) {
                    CloseControlPanel();
                }else if(index == 1) {
                    SetPage();
                }
            }

        }
    }

}
