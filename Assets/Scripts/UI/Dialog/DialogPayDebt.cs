using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPayDebt : MonoBehaviour {
	
	public Dialog dialog;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
        dialog.OnEndOfDialog += PayDebt;
    }

    // Checks gold and updates dialog text on update
    private void Update() {
        SetDialogText();
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy() {
		dialog.OnEndOfDialog -= PayDebt;
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		dialog.OnEndOfDialog += PayDebt;
    }

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		dialog.OnEndOfDialog -= PayDebt;
	}

	/// <summary>
	/// Pay the debt if money>debt
	/// </summary>
	private void PayDebt() {
        if (PlayerManager.instance.GetComponent<CharacterInventory>().gold >= EventManager.instance.debtOwed && EventManager.instance.debtOwed != 0) {
            EventManager.instance.PayDebt();
        }
    }


    // Sets the dialog
    private void SetDialogText() {
        if (EventManager.instance.debtOwed == 0) {
            dialog.lines = new string[] { "You don't owe us anything right now." };
        }
        else if (PlayerManager.instance.GetComponent<CharacterInventory>().gold >= EventManager.instance.debtOwed) {
            dialog.lines = new string[] { "Ah, I see you have enough to pay your interest",
                "Thank you for paying the " + EventManager.instance.debtOwed + " gold you owe us.",
                "I hope to do business with you again in the near future." };
        }
        else {
            dialog.lines = new string[] {"You don't have enough to pay your interest?",
                "You'll need " + (EventManager.instance.debtOwed-PlayerManager.instance.GetComponent<CharacterInventory>().gold)
                    + " gold to pay your interest.",
                "You better earn enough soon or you'll be sorry."};
        }
    }
}
