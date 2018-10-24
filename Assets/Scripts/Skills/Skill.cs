using UnityEngine;

/// <summary>
/// This is a scriptable class for skills.
/// </summary>
[CreateAssetMenu(menuName = "New Skill")]
public class Skill : ScriptableObject {
    
    /// <summary>
    /// Enum for the different types of skills.
    /// </summary>
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
}
