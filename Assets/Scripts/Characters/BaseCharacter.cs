using UnityEngine;
using UnityEngine.AI;

public abstract class BaseCharacter : MonoBehaviour {

    [Header("Basics")]
    /// <summary>
    /// character's stats script
    /// </summary>
    public CharacterStats characterStats;

    /// <summary>
    /// character's agent script
    /// </summary>
    public NavMeshAgent agent;

    /// <summary>
    /// character's skill caster script
    /// </summary>
    public SkillCaster skillCaster;

    /// <summary>
    /// character's animator component
    /// </summary>
    public Animator animator;

    /// <summary>
    /// character's animator status component
    /// </summary>
    public AnimatorStatus animatorStatus;

    [Header("Optional")]
    /// <summary>
    /// Character's equipment component
    /// </summary>
    public CharacterEquipment characterEquipment;
}
