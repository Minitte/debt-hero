using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour {

	/// <summary>
	/// Slot number
	/// </summary>
	public int slot;

	[Header("Game Object Parent group")]

	/// <summary>
	/// Save info parent
	/// </summary>
	public GameObject saveInfo;

	/// <summary>
	/// "Empty" new slot parent 
	/// </summary>
	public GameObject newSlot;

	[Header("Save Info text")]

	/// <summary>
	/// Text representing the name of the save
	/// </summary>
	public Text nameText;

	/// <summary>
	/// Text representing the day of the save
	/// </summary>
	public Text dayValue;

	/// <summary>
	/// Text representing the time of the save
	/// </summary>
	public Text timeValue;

	/// <summary>
	/// Text representing the gold of the save
	/// </summary>
	public Text goldValue;

	/// <summary>
	/// Text representing the level of the save
	/// </summary>
	public Text levelValue;

	/// <summary>
	/// Text representing the class of the save
	/// </summary>
	public Text classValue;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		// TODO get save
	}
}
