using UnityEngine;

/// <summary>
/// This is an abstract class for all skill behaviours.
/// </summary>
public abstract class SkillBehaviour : MonoBehaviour {

    /// <summary>
    /// Activates the skill behaviour.
    /// </summary>
    public abstract void Activate(Transform caster, Skill skill);
}
