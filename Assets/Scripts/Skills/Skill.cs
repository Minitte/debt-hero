using System.Collections.Generic;
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

    #region General
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
    #endregion

    #region Costs
    /// <summary>
    /// The base cooldown of the skill.
    /// </summary>
    public float cooldown;

    /// <summary>
    /// How much mana the skill costs.
    /// </summary>
    public float manaCost;
    #endregion

    #region Damage Properties
    /// <summary>
    /// Prefab of the damage hitbox.
    /// </summary>
    public GameObject damagePrefab;

    /// <summary>
    /// Multiplier for physical damage.
    /// </summary>
    public float physicalMultiplier = 1f;

    /// <summary>
    /// Multiplier for magic damage.
    /// </summary>
    public float magicMultiplier = 1f;

    /// <summary>
    /// Multiplier for the damage range.
    /// Used for melee attacks.
    /// </summary>
    public float rangeMultiplier = 1f;

    /// <summary>
    /// Multiplier for the damage area.
    /// Used for AoE skills.
    /// </summary>
    public float areaMultiplier = 1f;
    #endregion

    #region Healing Properties
    /// <summary>
    /// Prefab of the healing hitbox.
    /// </summary>
    public GameObject healingPrefab;

    /// <summary>
    /// Amount of healing done by the skill.
    /// </summary>
    public float healing;

    /// <summary>
    /// Radius for AoE skills.
    /// </summary>
    public float healingRadius;
    #endregion

    #region Status Effects
    /// <summary>
    /// Flag for if this skill has status effects.
    /// </summary>
    public bool hasStatusEffects;

    /// <summary>
    /// Array of status effects.
    /// </summary>
    public GameObject[] statusEffects;
    #endregion

    /// <summary>
    /// Casts the skill.
    /// </summary>
    /// <param name="caster">The transform of the caster</param>
    public void Cast(Transform caster) {
        // Play melee animation if skill type is melee
        if (skillType == SkillType.Melee) {
            caster.GetComponent<BaseCharacter>().animator.SetTrigger("Attack");
        }

        // Activate damage behaviour
        if (damagePrefab != null) {
            SkillBehaviour damage = Instantiate(damagePrefab, caster).GetComponent<SkillBehaviour>();
            damage.Activate(caster, this);
        }

        // Activate healing behaviour
        if (healingPrefab != null) {
            SkillBehaviour healing = Instantiate(healingPrefab, caster).GetComponent<SkillBehaviour>();
            healing.Activate(caster, this);
        }
    }
}
