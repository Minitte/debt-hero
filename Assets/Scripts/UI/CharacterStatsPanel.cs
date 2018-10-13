using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CharacterStatsPanel : MonoBehaviour {

	[Header("Text Values")]
	
	/// <summary>
	/// Text representing HP
	/// </summary>
	public TextMeshProUGUI hpTextValue;

	/// <summary>
	/// Text representing MP
	/// </summary>
	public TextMeshProUGUI mpTextValue;

	/// <summary>
	/// Text representing p att
	/// </summary>
	public TextMeshProUGUI pattTextValue;

	/// <summary>
	/// Text representing m att
	/// </summary>
	public TextMeshProUGUI mattTextValue;

	/// <summary>
	/// Text representing p def
	/// </summary>
	public TextMeshProUGUI pdefTextValue;

	/// <summary>
	/// Text representing m def
	/// </summary>
	public TextMeshProUGUI mdefTextValue;

	/// <summary>
	/// Text representing level
	/// </summary>
	public TextMeshProUGUI levelTextValue;

	/// <summary>
	/// Text representing exp
	/// </summary>
	public TextMeshProUGUI expTextValue;

	[Header("Stat")]

	/// <summary>
	/// Stats to display
	/// </summary>
	public CharacterStats stats;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		UpdateTextValues();
	}

	/// <summary>
	/// Updates the text values with the corresponding values in stats
	/// </summary>
	public void UpdateTextValues() {
		hpTextValue.text = stats.currentHp + " / " + stats.maxHp;
		mpTextValue.text = stats.currentMp + " / " + stats.maxMp;

		pattTextValue.text = stats.physAtk + "";
		mattTextValue.text = stats.magicAtk + "";

		pdefTextValue.text = stats.physDef + "";
		mattTextValue.text = stats.magicDef + "";

		levelTextValue.text = stats.level + "";

		expTextValue.text = stats.exp + "";

	}
}
