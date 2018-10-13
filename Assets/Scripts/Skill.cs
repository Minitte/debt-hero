using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {

    public string name;
    public float cooldown;
    public CharacterStats.StatType stat;
    public float duration;
    public float amount;
    public int type;

    public Skill()
    {
        name = "";
        cooldown = 0;
        stat = CharacterStats.StatType.NONE;
        amount = 0;
        duration = 0;
        type = -1;
    }


    /// <summary>
    /// ALL SKILL INFO IS HERE
    /// Gets info for a skill by Skill ID
    /// Skill.type is -1 if skill not found
    /// </summary>
    public static Skill GetInfo(int skillID)
    {
        Skill skill = new Skill();

        switch (skillID)
        {
            case 0:
                skill.name = "Power Strike";
                skill.amount = 20;
                skill.type = 4;
                skill.cooldown = 20;
                break;
            case 1:
                skill.name = "Healing";
                skill.stat = CharacterStats.StatType.CURRENT_HP;
                skill.amount = 20;
                skill.type = 1;
                skill.cooldown = 30;
                break;
        }

        return skill;
    }

    /// <summary>
    /// Skill Type 1
    /// To use a buff skill that increases a stat, for recovery
    /// (i.e.) Gain("hp", 10); gain 10 hp
    /// Should only be used for HP and MP
    /// </summary>
    public static void Gain(CharacterStats target, CharacterStats.StatType stat, float amount)
    {
        //target.GetComponent<Animator>().SetBool("skillcast", true);
        target.AddToStat(stat, amount);
    }
    /// <summary>
    /// Skill Type 2
    /// To use a buff skill that buffs a stat by amount for duration
    /// (i.e.) BuffDuration("hp", 10, 10); buffs MaxHP by 10 for 10 seconds
    /// </summary>
    public static void BuffDuration(string stat, float duration, float amount)
    {

    }
    /// <summary>
    /// Skill Type 3
    /// To use a buff skill that gives stat by amount/second for duration
    /// (i.e.) BuffOverTime("hp", 10, 10); gives 10 HP per second for 10 seconds
    /// </summary>
    public static void BuffOverTime(string stat, float duration, float amount)
    {

    }

    /// <summary>
    /// Skill Type 4
    /// To use a melee skill that does damage
    /// </summary>
    public static void Melee(float damage, Collider enemy)
    {
        enemy.GetComponent<CharacterStats>().TakeDamage(damage, 0);
    }

    /// <summary>
    /// Skill Type 5
    /// To use a ranged skill
    /// </summary>
    public static void Ranged(float damage)
    {

    }
}
