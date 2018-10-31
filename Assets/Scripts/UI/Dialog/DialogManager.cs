using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour {
    /// <summary>
    /// Background image of the dialog prefab.
    /// </summary>
    public Image dialogBackground;

    /// <summary>
    /// Text component of the dialog prefab.
    /// </summary>
    public Text dialogText;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReadDialog(string line) {
        ToggleDialog(true);
        dialogText.text = line;
    }

    public void CloseDialog() {
        ToggleDialog(false);
    }

    public void ToggleDialog(bool flag) {
        dialogBackground.enabled = flag;
        dialogText.text = "";
        dialogText.enabled = flag;
    }
}
