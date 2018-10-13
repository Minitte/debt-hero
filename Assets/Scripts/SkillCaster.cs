using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For casting skills
/// Also manages skill cooldowns
/// </summary>
public class SkillCaster : MonoBehaviour {

    /// <summary>
    /// Whether the gameobject can deal damage or not.
    /// Used by the animator to make damage windows.
    /// </summary>
    public bool canDealDamage;

    /// <summary>
    /// Represents if the character is attacking.
    /// </summary>
    public bool isCasting;

    /// <summary>
    /// Reference to skill0's prefab.
    /// </summary>
    public GameObject skill0;

    // Skill 0 Cooldown
    public int skill0ID;
    private float timeStamp0;
    public bool canCast0;

    // Skill 1 Cooldown
    public int skill1ID;
    private float timeStamp1;
    public bool canCast1;

    // Skill 2 Cooldown
    public int skill2ID;
    private float timeStamp2;
    public bool canCast2;

    // Skill 3 Cooldown
    public int skill3ID;
    private float timeStamp3;
    public bool canCast3;

    // Skill 4 Cooldown
    public int skill4ID;
    private float timeStamp4;
    public bool canCast4;


    // Use this for initialization
    void Start() {
        canCast0 = canCast1 = canCast2 = canCast3 = canCast4 = true;
        timeStamp0 = timeStamp1 = timeStamp2 = timeStamp3 = timeStamp4 = Time.time;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("Cooldown Skill1: "+ (timeStamp1-Time.time));
        canCast0 = canCast1 = canCast2 = canCast3 = canCast4 = false;

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
    }

    /// <summary>
    /// Cast
    /// </summary>
    public void Cast(int skillNum, int skillID) {
        bool cast = false;
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
            Skill currentSkill = Skill.GetInfo(skillID);
            if (currentSkill.type != -1) {
                switch (Skill.GetInfo(skillID).type) {
                    case 0:
                        GetComponent<Animator>().SetTrigger("Attack");
                        Instantiate(skill0, transform); // Create the basic attack hitbox
                        break;
                    case 1:
                        Skill.Gain(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>(), currentSkill.stat, currentSkill.amount);
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
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AI") {
            Debug.Log("Melee skill hit");

            // Apply damage to the enemy
            Skill.Melee(0, other);
        }
    }*/


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



}
