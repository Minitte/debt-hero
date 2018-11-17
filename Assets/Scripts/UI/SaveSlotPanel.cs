using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotPanel : MonoBehaviour {

	/// <summary>
	/// Journal Panel refernce to return to
	/// </summary>
	public GameObject journalPanel;

	public SaveSlotUI[] saveSlots;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float cancel = Input.GetAxis("Menu Cancel");
		float sysMenu = Input.GetAxis("System Menu Open");

		if (cancel != 0 || sysMenu != 0) {
			ExitSaveSlotPanel();
		}
	}

	/// <summary>
	/// Exits the save slot panel and returns to the journal panel
	/// </summary>
	public void ExitSaveSlotPanel() {
		journalPanel.SetActive(true);
		this.gameObject.SetActive(false);
	}

	/// <summary>
	/// Slot to save to
	/// </summary>
	/// <param name="slot"></param>
	public void SaveToSlot(int slot) {
		// TODO save to slot

		ExitSaveSlotPanel();
	}

	public void UpdateAllSlots() {

	}

}
