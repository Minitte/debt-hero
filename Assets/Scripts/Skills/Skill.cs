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
    [Header("General")]
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
    [Header("Costs")]
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
    [Header("Damage")]
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

    /// <summary>
    /// Array of skill behaviours.
    /// </summary>
    public GameObject[] skillBehaviours;

    /// <summary>
    /// Casts the skill.
    /// </summary>
    /// <param name="caster">The transform of the caster</param>
    public void Cast(Transform caster) {
        // Activate all the skill behaviours
        foreach (GameObject behaviour in skillBehaviours) {
            SkillBehaviour sb = Instantiate(behaviour, caster).GetComponent<SkillBehaviour>();
            sb.Activate(caster, this);
        }
    }
}
