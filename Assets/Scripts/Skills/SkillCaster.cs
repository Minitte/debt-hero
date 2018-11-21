using UnityEngine;

/// <summary>
/// For casting skills
/// Also manages skill cooldowns
/// </summary>
public class SkillCaster : MonoBehaviour {

    /// <summary>
    /// List of skills.
    /// </summary>
    public Skill[] skills;

    /// <summary>
    /// Timestamps used for skill cooldown.
    /// </summary>
    private float[] _timestamps;

    /// <summary>
    /// Flag for if a skill is ready to be casted.
    /// </summary>
    private bool[] _canCasts;

    /// <summary>
    /// Reference to the character stats.
    /// </summary>
    private CharacterStats _characterStats;

    // Use this for initialization
    void Start() {
        _timestamps = new float[skills.Length];
        _canCasts = new bool[skills.Length];
        _characterStats = GetComponent<BaseCharacter>().characterStats;

        // Initialize timestamps
        for (int i = 0; i < _timestamps.Length; i++) {
            _timestamps[i] = Time.time;
        }

        if (CompareTag("Player")) {
            //OnSkillCasted += GameObject.Find("Canvas").transform.GetComponentInChildren<PlayerResource>().UpdateMana;

            // Update skill icons
            SkillManager.instance.UpdateSkills(this);
        }
    }

    // Update is called once per frame
    void Update() {
        // Check if cooldowns are over
        for (int i = 0; i < _timestamps.Length; i++) {
            if (_timestamps[i] <= Time.time) {
                _canCasts[i] = true;
            }
        }
    }

    /// <summary>
    /// Casts a skill if it is available.
    /// </summary>
    /// <param name="skillNum">The index of the skill to cast</param>
    /// <returns>True if successfully cast, else false</returns>
    public bool Cast(int skillNum) {
        if (skillNum < skills.Length && skills[skillNum] != null && _canCasts[skillNum] == true) { // Validation
            if (_characterStats.currentMp >= skills[skillNum].manaCost) { // Check if enough mana to cast
                skills[skillNum].Cast(GetComponent<BaseCharacter>()); // Cast the skill
                _characterStats.SpendMana(skills[skillNum].manaCost);

                // Put skill on cooldown
                _timestamps[skillNum] = Time.time + skills[skillNum].cooldown;
                _canCasts[skillNum] = false;

                if (CompareTag("Player") && skillNum != 0) { // Skill cooldown UI for player, ignore basic attack
                    SkillManager.instance.StartCooldown(skillNum, _timestamps[skillNum]);
                }

                return true;
            } else {
                if (CompareTag("Player")) {
                    FloatingTextController.instance.CreateFloatingText("Out of mana!", Color.yellow, gameObject);
                }
            }
        }
        return false;
    }
}
