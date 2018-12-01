using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIText : MonoBehaviour {
    public List<string> controls;
    public Text controlUI;
    public int index;
   

    // Use this for initialization
    void Start() {
        SetControls();
        index = 0;
        controlUI.text = controls[0];
    }
	// Update is called once per frame
	void Update () {
		
	}

    void SetControls() {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
             controls[0] = "Menu Controls \n"
                 + "Open System Menu: Press ESC Key \n"
                 + "Open In-game Menu: Press TAB Key \n"
                 + "Menu Navigation: Move Mouse Cursor or D/W Key \n"
                 + "Menu Confirm: Left Click or Press Enter Key \n"
                 + "Menu Cancel: Left Click or Press ESC Key \n "
                 + "Inventory Use: Right Click \n"
                 + "Page 1/2";

            controls[1] = "Combat Controls \n"
                + "Movement: Press of Hold down Left Click \n"
                + "Dash: Press Q to dash towards mouse cursor \n"
                + "Basic Attack: Press Left Click on the enemy \n"
                + "First Skill: Press W to activate skill 1 \n"
                + "Second Skill: Press E to activate skill 2 \n"
                + "Thrid Skill: Press R to activate skill 3 \n"
                + "Page 2/2";
        }else if(Application.platform == RuntimePlatform.PS4) {
            controls[0] = "Menu Controls \n"
                + "Open System Menu: Press R2 \n"
                + "Open In-game Menu: Press TouchPad \n"
                + "Menu Navigation: Use Left Analog Stick \n"
                + "Menu Confirm: Press Circle \n"
                + "Menu Cancel: Press Cross \n"
                + "Inventory use: Press Triangle \n"
                + "Page 1/2";

            controls[1] = "Combat Controls \n"
                + "Movement Horizontal: Left Analog \n"
                + "Move Camera: Right Analog \n"
                + "Dash: L1 \n"
                + "Basic Attack: Cross \n"
                + "First Skill: Circle \n"
                + "Second Skill: Triangle \n"
                + "Third Skill: Square \n"
                + "Page 2/2";
        }
    }
}
