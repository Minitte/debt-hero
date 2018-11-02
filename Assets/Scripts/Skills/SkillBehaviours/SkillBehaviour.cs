using UnityEngine;

/// <summary>
/// This is an abstract class for skill behaviours.
/// </summary>
public abstract class SkillBehaviour : ScriptableObject {

    /// <summary>
    /// Activates the skill behaviour.
    /// </summary>
    public abstract void Activate(Transform caster, Skill skill);
}
