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
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		StartCoroutine(UpdateAllSlotCoroutine());
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
		SaveLoadManager.instance.Save(slot);

		UpdateSlot(slot);

		ExitSaveSlotPanel();
	}

	/// <summary>
	/// Updates all save slots ui objects.
	/// Corountine Compatible
	/// </summary>
	/// <returns></returns>
	public IEnumerator UpdateAllSlotCoroutine() {
		yield return new WaitForEndOfFrame();

		UpdateAllSlots();

		yield return new WaitForEndOfFrame();
	}

	/// <summary>
	/// Updates all save slots ui objects
	/// </summary>
	public void UpdateAllSlots() {
		SaveLoadManager saveManager = SaveLoadManager.instance;

		for (int i = 0; i < saveSlots.Length; i++) {
			UpdateSlot(i);
		}
	}

	/// <summary>
	/// Updates a single slot ui object
	/// </summary>
	/// <param name="slot"></param>
	public void UpdateSlot(int slot) {
		SaveLoadManager saveManager = SaveLoadManager.instance;
		SaveSlotUI ssui = saveSlots[slot];
		GameData data = saveManager.LoadGameData(ssui.slot);

		// no data available for thatt slot
		if (data == null) {
			// show "empty"
			ssui.newSlot.SetActive(true);
			ssui.saveInfo.SetActive(false);
			return;
		} 
		// data is available for that slot
		else {
			// show info
			ssui.newSlot.SetActive(false);
			ssui.saveInfo.SetActive(true);

			// set text
			ssui.nameText.text = "Save File"; // TODO actual save name
			ssui.dayValue.text = data.days + "";
			ssui.timeValue.text = data.currentHour + ":" + data.currentMinute;
			ssui.goldValue.text = data.playerGold + "g";
			ssui.levelValue.text = data.playerLevel + "";
			ssui.classValue.text = "Warrior"; // TODO actual class
		}
	}

}
