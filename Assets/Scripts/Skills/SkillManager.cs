using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for skill management.
/// </summary>
public class SkillManager : MonoBehaviour {

    /// <summary>
    /// Singleton instance of this class.
    /// </summary>
    public static SkillManager instance;

    /// <summary>
    /// Array of skill icon references.
    /// The actual skill numbers are offset by +1.
    /// 0 = Dash
    /// 1 = First skill
    /// 2 = Second skill
    /// 3 = Third skill
    /// </summary>
    public SkillIcon[] _skillIcons;

    /// <summary>
    /// Reference to the local player's skill caster.
    /// </summary>
    private SkillCaster _skillCaster;

    // Use this for initialization
    private void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Tried to instantiate skill manager instance twice.");
        }
	}

    private void Update() {
        // Check for skill key presses
        if (Input.GetButtonDown("Dash")) {
            _skillIcons[0].Press();
        }
        if (Input.GetButtonDown("First Skill")) {
            _skillIcons[1].Press();
        }
        if (Input.GetButtonDown("Second Skill")) {
            _skillIcons[2].Press();
        }
        if (Input.GetButtonDown("Third Skill")) {
            _skillIcons[3].Press();
        }
    }

    /// <summary>
    /// Updates the skill references.
    /// </summary>
    /// <param name="skillCaster">The skill caster component of the local player</param>
    public void UpdateSkills(SkillCaster skillCaster) {
        for (int i = 0; i < _skillIcons.Length; i++) {
            _skillIcons[i].skill = skillCaster.skills[i + 1];
            _skillIcons[i].GetComponent<Image>().sprite = _skillIcons[i].skill.skillIcon;
        }
    }

    /// <summary>
    /// Starts the cooldown for a specified skill.
    /// </summary>
    /// <param name="skillNum">The skill number</param>
    /// <param name="cooldownEndTime">The end time of the cooldown</param>
    public void StartCooldown(int skillNum, float cooldownEndTime) {
        _skillIcons[skillNum - 1].StartCooldown(cooldownEndTime);
    }
}
