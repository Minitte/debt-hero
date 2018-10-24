using UnityEngine;

/// <summary>
/// This is a scriptable class for skills.
/// </summary>
[CreateAssetMenu(menuName = "New Skill")]
public class Skill : ScriptableObject {
    
    public enum SkillType {
        Melee,
        Ranged,
        Magic
    }

    /// <summary>
    /// The name of the skill.
    /// </summary>
    public string skillName;

    /// <summary>
    /// The icon of the skill.
    /// </summary>
    public Sprite skillIcon;

    /// <summary>
    /// The description of the skill.
    /// </summary>
    public string skillDescription;

    /// <summary>
    /// The type of skill.
    /// </summary>
    public SkillType skillType;

    /// <summary>
    /// The base cooldown of the skill.
    /// </summary>
    public float cooldown;

    /// <summary>
    /// How much mana the skill costs.
    /// </summary>
    public float manaCost;

    /// <summary>
    /// The base duration of the skill.
    /// </summary>
    public float duration;

    /// <summary>
    /// Multiplier for physical damage.
    /// </summary>
    public float physicalMultiplier = 1f;

    /// <summary>
    /// Multiplier for magic damage.
    /// </summary>
    public float magicMultiplier = 1f;

    /// <summary>
    /// Amount of healing done by the skill.
    /// </summary>
    public float healing;

    /// <summary>
    /// Radius for AoE skills.
    /// </summary>
    public float areaRadius;

    /// <summary>
    /// The behaviours of this skill.
    /// </summary>
    public SkillBehaviour[] skillBehaviours;

    /// <summary>
    /// Casts the skill.
    /// </summary>
    /// <param name="caster">The transform of the caster</param>
    public void Cast(Transform caster) {
        // Play melee animation if skill type is melee
        if (skillType == SkillType.Melee) {
            caster.GetComponent<BaseCharacter>().animator.SetTrigger("Attack");
        }
        // Activate each behaviour
        foreach (SkillBehaviour behaviour in skillBehaviours) {
            SkillBehaviour behaviourObj = Instantiate(behaviour, caster);
            behaviourObj.Activate(caster, this);
        }
    }

    /*
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
                skill.name = "Basic Attack";
                skill.amount = 5;
                skill.type = 0;
                skill.cooldown = 1;
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
    */
}
