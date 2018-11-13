using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SkillDisplayUI : MonoBehaviour {

	[Header("Skill Information")]
	
	/// <summary>
	/// Skill's Icon
	/// </summary>
	public Image icon;

	/// <summary>
	/// Skill's name text
	/// </summary>
	public TextMeshProUGUI nameText;

	/// <summary>
	/// Skill's description
	/// </summary>
	public TextMeshProUGUI descText;

	[Header("Others")]
	
	/// <summary>
	/// Background of the skill display
	/// </summary>
	public Image background;

	/// <summary>
	/// Gray out overlay
	/// </summary>
	public Image grayout;
}
