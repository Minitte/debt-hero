using System.Collections.Generic;
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
    /// List of skill objects that are currently running for this character.
    /// </summary>
    private List<GameObject> _activeSkillObjects;

    /// <summary>
    /// Property variable for active skill objects.
    /// </summary>
    public List<GameObject> ActiveSkillObjects {
        get { return _activeSkillObjects; }
    }

    /// <summary>
    /// List of skills that are currently being delayed for animation purposes.
    /// </summary>
    private Queue<Skill> _delayedSkills;

    /// <summary>
    /// Reference to the character stats.
    /// </summary>
    private CharacterStats _characterStats;

    // Use this for initialization
    private void Start() {
        _timestamps = new float[skills.Length];
        _canCasts = new bool[skills.Length];
        _activeSkillObjects = new List<GameObject>();
        _delayedSkills = new Queue<Skill>();
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
    private void Update() {
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
        if (_delayedSkills.Count != 0) {
            Debug.LogWarning(name + "tried to cast two skills at once.");
        }

        if (skillNum < skills.Length && skills[skillNum] != null && _canCasts[skillNum] == true) { // Validation
            if (_characterStats.currentMp >= skills[skillNum].manaCost) { // Check if enough mana to cast
                if (!skills[skillNum].delayed) {
                    skills[skillNum].Cast(GetComponent<BaseCharacter>());
                    _characterStats.SpendMana(skills[skillNum].manaCost);
                } else {
                    _delayedSkills.Enqueue(skills[skillNum]); // Add delayed skill to the queue
                }
                
                // Play attack animation
                Animator characterAnimator = GetComponent<BaseCharacter>().animator;
                switch (skills[skillNum].skillType) {
                    case Skill.SkillType.Melee:
                        characterAnimator.SetTrigger("Attack");
                        break;
                    case Skill.SkillType.Projectile:
                        characterAnimator.SetTrigger("Attack");
                        break;
                    case Skill.SkillType.AreaOfEffect:
                        characterAnimator.SetTrigger("Hurt"); // Placeholder TODO
                        break;
                }

                // Put skill on cooldown
                _timestamps[skillNum] = Time.time + skills[skillNum].cooldown;
                _canCasts[skillNum] = false;

                if (CompareTag("Player") && skillNum != 0) { // Skill cooldown UI for player, ignore basic attack
                    SkillManager.instance.StartCooldown(skillNum, _timestamps[skillNum]);
                }

                GetComponent<BaseCharacter>().animatorStatus.isCasting = true; // Set casting flag

                return true;
            } else {
                if (CompareTag("Player")) {
                    FloatingTextController.instance.CreateFloatingText("Out of mana!", Color.yellow, gameObject);
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Dequeues a skill from the delayed skills queue, and casts it.
    /// </summary>
    public void DelayedCast() {
        Skill skill = _delayedSkills.Dequeue();
        skill.Cast(GetComponent<BaseCharacter>());
        _characterStats.SpendMana(skill.manaCost);
    }

    /// <summary>
    /// Clears the list of skill objects by destroying them.
    /// </summary>
    public void ClearSkillObjects() {
        foreach (GameObject skillObject in _activeSkillObjects) {
            Destroy(skillObject);
        }
        _activeSkillObjects.Clear();
    }
}
