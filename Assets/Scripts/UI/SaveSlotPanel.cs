using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSlotPanel : MonoBehaviour {

	/// <summary>
	/// Journal Panel refernce to return to
	/// </summary>
	public GameObject journalPanel;

	public SaveSlotUI[] saveSlots;

    public int index;

    /// <summary>
    /// Cool down flag
    /// </summary>
    private bool _cooldown;

    private float _time;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float cancel = Input.GetAxis("Menu Cancel");
		float sysMenu = Input.GetAxis("System Menu Open");

        if (_cooldown) {
            _time += Time.deltaTime;

            if (_time >= 0.15f) {
                _cooldown = false;
                _time = 0f;
            }

            return;
        }

        SaveSlotPanelControls();

        if (cancel != 0 || sysMenu != 0) {
			ExitSaveSlotPanel();
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
        StartCoroutine(UpdateAllSlotCoroutine());
        index = 0;
        saveSlots[index].GetComponent<Button>().Select();
        _cooldown = true;
    }

	/// <summary>
	/// Exits the save slot panel and returns to the journal panel
	/// </summary>
	public void ExitSaveSlotPanel() {
        if (journalPanel != null) {
            journalPanel.SetActive(true);
        }
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

    public void LoadSlot(int slot) {
		
        SaveLoadManager.instance.LoadGameData(slot);
		SceneManager.LoadScene(SaveLoadManager.instance.LoadGameData(slot).lastScene);
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
			ssui.nameText.text = data.name;
			ssui.dayValue.text = data.days + "";
			ssui.timeValue.text = data.currentHour + ":" + data.currentMinute;
			ssui.goldValue.text = data.playerGold + "g";
			ssui.levelValue.text = data.playerLevel + "";
			ssui.classValue.text = data.className;
		}
	}

    public void SaveSlotPanelControls() {

        float vert = Input.GetAxis("Menu Vertical");

        if (vert < 0) {
            if (index >= 0 && index < 4) {
                index++;
                _cooldown = true;
                return;
            }

        } else if (vert > 0) {
            if (index <= 4 && index > 0) {
                index--;
                _cooldown = true;
                return;
            }
        }

        saveSlots[index].GetComponent<Button>().Select();

        if (Input.GetAxis("Menu Confirm") != 0) {
            Debug.Log(gameObject.transform.parent.parent.parent.gameObject.name);
			if (gameObject.transform.parent.parent.parent.gameObject.name.Equals("Load Slot Panel")) {
				LoadSlot(index);
				
            }else {
                SaveToSlot(index);
            }
        }
    }
}
