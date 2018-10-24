using UnityEngine;

/// <summary>
/// For casting skills
/// Also manages skill cooldowns
/// </summary>
public class SkillCaster : MonoBehaviour {

    /// <summary>
    /// Skill cast event template.
    /// </summary>
    public delegate void SkillCastedEvent();

    /// <summary>
    /// This event is called when the character casts a skill.
    /// </summary>
    public event SkillCastedEvent OnSkillCasted;

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
    public void Cast(int skillNum) {
        if (skillNum < skills.Length && skills[skillNum] != null && _canCasts[skillNum] == true) { // Validation
            if (_characterStats.currentMp >= skills[skillNum].manaCost) { // Check if enough mana to cast
                skills[skillNum].Cast(transform); // Cast the skill
                _characterStats.currentMp -= skills[skillNum].manaCost;

                // Put skill on cooldown
                _timestamps[skillNum] = Time.time + skills[skillNum].cooldown;
                _canCasts[skillNum] = false;

                if (OnSkillCasted != null) {
                    OnSkillCasted(); // Fire the skill casted event
                }
            } else {
                if (CompareTag("Player")) {
                    FloatingTextController.instance.CreateFloatingText("Out of mana!", gameObject);
                }
            }
        }
    }
}
