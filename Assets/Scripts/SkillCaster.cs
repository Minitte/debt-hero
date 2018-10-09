using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour {
    /// <summary>
    /// For casting skills, has all the information on what each skill does
    /// Also manages skill cooldowns
    /// </summary>

    // Skill 1 Cooldown
    public int skill1ID;
    private float timeStamp1;
    public float coolDown1;
    public bool canCast1;

    // Skill 2 Cooldown
    public int skill2ID;
    private float timeStamp2;
    public float coolDown2;
    public bool canCast2;

    // Skill 3 Cooldown
    public int skill3ID;
    private float timeStamp3;
    public float coolDown3;
    public bool canCast3;

    // Skill 4 Cooldown
    public int skill4ID;
    private float timeStamp4;
    public float coolDown4;
    public bool canCast4;

    // Use this for initialization
    void Start () {
		canCast1 = canCast2 = canCast3 = canCast4 = true;
        timeStamp1 = timeStamp2 = timeStamp3 = timeStamp4 = Time.time;
}
	
	// Update is called once per frame
	void Update () {

        /// <summary>
        /// If skill time is less than current time, skill1 can be casted again
        /// </summary>
        if (timeStamp1 <= Time.time)
        {
            canCast1 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill2 can be casted again
        /// </summary>
        if (timeStamp2 <= Time.time)
        {
            canCast2 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill3 can be casted again
        /// </summary>
        if (timeStamp3 <= Time.time)
        {
            canCast3 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill4 can be casted again
        /// </summary>
        if (timeStamp4 <= Time.time)
        {
            canCast4 = true;
        }
    }

    /// <summary>
    /// Cast
    /// </summary>
    public void Cast(int skillNum, int skillID)
    {
        bool cast = false;
        switch (skillNum)
        {
            case 1:
                if (canCast1)
                {
                    cast = true;
                }
                break;
            case 2:
                if (canCast2)
                {
                    cast = true;
                }
                break;
            case 3:
                if (canCast3)
                {
                    cast = true;
                }
                break;
            case 4:
                if (canCast4)
                {
                    cast = true;
                }
                break;
        }
        if(cast)
        {

        }
    }

    /// <summary>
    /// Skill Type 1
    /// To use a buff skill that buffs a stat by amount for duration
    /// (i.e.) Gain("hp", 10); gain 10 hp
    /// Should only be used for HP and MP
    /// </summary>
    public void Gain(string stat, float amount)
    {

    }
    /// <summary>
    /// Skill Type 2
    /// To use a buff skill that buffs a stat by amount for duration
    /// (i.e.) BuffDuration("hp", 10, 10); buffs MaxHP by 10 for 10 seconds
    /// </summary>
    public void BuffDuration(string stat, float duration, float amount)
    {

    }
    /// <summary>
    /// Skill Type 3
    /// To use a buff skill that gives stat by amount/second for duration
    /// (i.e.) BuffOverTime("hp", 10, 10); gives 10 HP per second for 10 seconds
    /// </summary>
    public void BuffOverTime(string stat, float duration, float amount)
    {

    }

    /// <summary>
    /// Skill Type 4
    /// To use a melee skill that does damage
    /// </summary>
    public void Melee(float damage)
    {

    }

    /// <summary>
    /// Skill Type 5
    /// To use a ranged skill
    /// </summary>
    public void Ranged(float damage)
    {

    }

    /// <summary>
    /// Gets info for a skill by Skill ID
    /// </summary>
    public Skill GetSkillInfo(int skillID)
    {
        Skill skill = new Skill();

        switch (skillID)
        {
            case 0:
                skill.name = "Power Strike";
                skill.amount = 20;
                skill.type = 4;
                skill.coolDown = 20;
                break;
            case 1:
                skill.name = "HP Buff";
                skill.amount = 20;
                skill.type = 2;
                skill.coolDown = 30;
                skill.duration = 0;
                break;
        }

        return skill;
    }

}
