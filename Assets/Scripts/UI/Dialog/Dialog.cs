using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {
    public List<string> dialogLines;

    public DialogManager dialogManager;

    public int dialogIndex;

    public bool inRange;
      
    private void Update() {
        if (inRange) {
            if (Input.GetKeyDown(KeyCode.X)) {

                if (dialogIndex >= dialogLines.Count) {
                    dialogManager.CloseDialog();
                    dialogIndex = 0;
                    return;
                }

                dialogManager.ReadDialog(dialogLines[dialogIndex]);

                dialogIndex++;
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        dialogManager.CloseDialog();
        inRange = false;
    }

    public void OnTriggerEnter(Collider other) {
        inRange = true;
    }
}
