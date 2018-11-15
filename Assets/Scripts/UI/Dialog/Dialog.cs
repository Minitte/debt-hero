using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

    /// <summary>
    /// Delegate for basic dialog events
    /// </summary>
    public delegate void DialogEvent();

    /// <summary>
    /// Event fired after the last line of the dialog
    /// </summary>
    public event DialogEvent OnEndOfDialog;

    /// <summary>
    /// List of dialog lines
    /// </summary>
    public string[] lines;

    /// <summary>
    /// Dialog manager object.
    /// </summary>
    public DialogManager dialogManager;

    /// <summary>
    /// Index of the set of lines
    /// </summary>
    // private int _currentDialogIndex;

    /// <summary>
    /// line index.
    /// </summary>
    private int _lineIndex;

    /// <summary>
    /// in Range.
    /// </summary>
    private bool _inRange;

    /// <summary>
    /// If the dialog is currently running
    /// </summary>
    private bool _dialogRunning;

    /// <summary>
    /// Time tracking
    /// </summary>
    private float _time;

    /// <summary>
    /// Next line cool down flag
    /// </summary>
    private bool _cooldown;

    /// <summary>
    /// Block flag to prevent dialog cycling
    /// </summary>
    private bool _blocked;

    /// <summary>
    /// Update
    /// </summary>
    private void Update() {
        //Check if other object is in range of this object's collider
        if (_inRange) {
            
            // a dialog is currently running
            if (_dialogRunning) {

                // cool down blocking
                if (_cooldown) {
                    _time += Time.deltaTime;

                    if (_time > 0.15f) {
                        _time = 0;
                        _cooldown = false;
                    }

                    return;
                }

                // no on cool down, check for input 
                float nextLineInput = Input.GetAxis("Dialog Next");

                if (nextLineInput != 0) {
                    NextDialogLine();
                    _cooldown = true;
                }
            }

            // dialog is not running
            else {
                float nextLineInput = Input.GetAxis("Dialog Start");

                if (nextLineInput != 0 && !_blocked) {
                    StartDialog();
                }
            }

        }
    }

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    void OnMouseUp() {
        if (_inRange && !_dialogRunning) {
            StartDialog();
        }
    }

    /// <summary>
    /// Starts the dialog
    /// </summary>
    public void StartDialog() {
        if (_blocked) {
            return;
        }

        // _currentDialogIndex = dialogIndex;
        _dialogRunning = true;
        _lineIndex = 0;
        _blocked = true;
        _cooldown = true;

        dialogManager.ReadDialog(lines[_lineIndex]);
    }

    /// <summary>
    /// Proceeds to the next dialog line
    /// </summary>
    public void NextDialogLine() {
        _lineIndex++;

        if (_lineIndex >= lines.Length) {
            EndDialog();
            return;
        }

        dialogManager.ReadDialog(lines[_lineIndex]);
    }

    /// <summary>
    /// Ends the dialog
    /// </summary>
    public void EndDialog() {

        if (OnEndOfDialog != null) {
            OnEndOfDialog();
        }

        _dialogRunning = false;
        _lineIndex = 0;
        // _currentDialogIndex = -1;

        dialogManager.ToggleDialog(false);
    }

    //Close dialog when player leaves area.
    public void OnTriggerExit(Collider other) {
       if(other.tag == "Player") {
            dialogManager.CloseDialog();
            _inRange = false;
            _blocked = false;
        }
       
    }

    //Setting the range is true.
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            _inRange = true;
        }
       
    }
}
