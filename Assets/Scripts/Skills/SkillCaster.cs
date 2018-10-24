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
    private float[] timestamps;

    /// <summary>
    /// Flag for if a skill is ready to be casted.
    /// </summary>
    private bool[] canCasts;

    /// <summary>
    /// Reference to the character stats.
    /// </summary>
    private CharacterStats _characterStats;

    // Use this for initialization
    void Start() {
        timestamps = new float[skills.Length];
        canCasts = new bool[skills.Length];
        _characterStats = GetComponent<BaseCharacter>().characterStats;

        // Initialize timestamps
        for (int i = 0; i < timestamps.Length; i++) {
            timestamps[i] = Time.time;
        }
        /*
        canCast0 = canCast1 = canCast2 = canCast3 = canCast4 = true;
        timeStamp0 = timeStamp1 = timeStamp2 = timeStamp3 = timeStamp4 = Time.time;
        test.character = transform;
        */
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("Cooldown Skill1: "+ (timeStamp1-Time.time));
        //canCast0 = canCast1 = canCast2 = canCast3 = canCast4 = false;
        for (int i = 0; i < timestamps.Length; i++) {
            if (timestamps[i] <= Time.time) {
                canCasts[i] = true;
            }
        }
        /*
        /// <summary>
        /// If skill time is less than current time, skill0 can be casted again
        /// </summary>
        if (timeStamp0 <= Time.time) {
            canCast0 = true;
        }
        /// <summary>
        /// If skill time is less than current time, skill1 can be casted again
        /// </summary>
        if (timeStamp1 <= Time.time) {
            canCast1 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill2 can be casted again
        /// </summary>
        if (timeStamp2 <= Time.time) {
            canCast2 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill3 can be casted again
        /// </summary>
        if (timeStamp3 <= Time.time) {
            canCast3 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill4 can be casted again
        /// </summary>
        if (timeStamp4 <= Time.time) {
            canCast4 = true;
        }
        */
    }

    /// <summary>
    /// Casts a skill if it is available.
    /// </summary>
    /// <param name="skillNum">The index of the skill to cast</param>
    public void Cast(int skillNum) {
        if (skillNum < skills.Length && skills[skillNum] != null && canCasts[skillNum] == true) {
            if (_characterStats.currentMp >= skills[skillNum].manaCost) {
                skills[skillNum].Cast(transform); // Cast the skill
                _characterStats.currentMp -= skills[skillNum].manaCost;

                // Put skill on cooldown
                timestamps[skillNum] = Time.time + skills[skillNum].cooldown;
                canCasts[skillNum] = false;

                if (OnSkillCasted != null) {
                    OnSkillCasted();
                }
            }
        }

        /*
        bool cast = false;
        
        /*
        switch (skillNum) {
            case 0:
                if (canCast0) {
                    cast = true;
                }
                break;
            case 1:
                if (canCast1) {
                    cast = true;
                }
                break;
            case 2:
                if (canCast2) {
                    cast = true;
                }
                break;
            case 3:
                if (canCast3) {
                    cast = true;
                }
                break;
            case 4:
                if (canCast4) {
                    cast = true;
                }
                break;
        }
        
        if (cast) {
            /*
            Skill currentSkill = Skill.GetInfo(skillID);
            if (currentSkill.type != -1) {
                switch (Skill.GetInfo(skillID).type) {
                    case 0:
                        if (GetComponent<BaseCharacter>().animator != null) {
                            GetComponent<BaseCharacter>().animator.SetTrigger("Attack");
                        }
                        Instantiate(skill0, transform); // Create the basic attack hitbox
                        break;
                    case 1:
                        //Skill.Gain(GetComponent<CharacterStats>(), currentSkill.stat, currentSkill.amount);
                        Instantiate(skill1, transform);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }
                
                UpdateCooldown(skillNum, currentSkill.cooldown);
            }
            
        }
        */
    }
    /*
    /// <summary>
    /// For updating the cooldowns
    /// </summary>
    private void UpdateCooldown(int skillNum, float cooldown) {
        
        switch (skillNum) {
            case 0:
                timeStamp0 += cooldown;
                break;
            case 1:
                timeStamp1 += cooldown;
                break;
            case 2:
                timeStamp2 += cooldown;
                break;
            case 3:
                timeStamp3 += cooldown;
                break;
            case 4:
                timeStamp4 += cooldown;
                break;
        }
    }
    */



}
