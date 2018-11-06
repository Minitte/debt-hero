using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {
    /// <summary>
    /// Dialog lines.
    /// </summary>
    public List<string> dialogLines;

    /// <summary>
    /// Dialog manager object.
    /// </summary>
    public DialogManager dialogManager;

    /// <summary>
    /// Dialog index.
    /// </summary>
    public int dialogIndex;

    /// <summary>
    /// in Range.
    /// </summary>
    public bool inRange;
    
    /// <summary>
    /// Update
    /// </summary>
    private void Update() {
        //Check if other object is in range of this object's collider
        if (inRange) {
            //If the user hits x, open a dialog box.
            if (Input.GetKeyDown(KeyCode.X)) {

                if (dialogIndex >= dialogLines.Count) {
                    dialogManager.CloseDialog();
                    dialogIndex = 0;
                    return;
                }
                //Read dialog
                dialogManager.ReadDialog(dialogLines[dialogIndex]);
                //increment index
                dialogIndex++;
            }
        }
    }

    //Close dialog when player leaves area.
    public void OnTriggerExit(Collider other) {
       if(other.tag == "Player") {
            dialogManager.CloseDialog();
            inRange = false;
        }
       
    }

    //Setting the range is true.
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            inRange = true;
        }
       
    }
}
