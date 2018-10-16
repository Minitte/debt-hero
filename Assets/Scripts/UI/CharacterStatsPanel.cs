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

	/// <summary>
	/// Stats to display
	/// </summary>
	private CharacterStats _stats;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		UpdateTextValues();
	}

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
		if (_stats == null) {
			_stats = PlayerManager.instance.GetComponent<CharacterStats>();
		}
		
		hpTextValue.text = _stats.currentHp + " / " + _stats.maxHp;
		mpTextValue.text = _stats.currentMp + " / " + _stats.maxMp;

		pattTextValue.text = _stats.physAtk + "";
		mattTextValue.text = _stats.magicAtk + "";

		pdefTextValue.text = _stats.physDef + "";
		mattTextValue.text = _stats.magicDef + "";

		levelTextValue.text = _stats.level + "";

		expTextValue.text = _stats.exp + "";

	}
}
