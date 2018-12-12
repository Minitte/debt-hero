using UnityEngine;

/// <summary>
/// This is an abstract class for instant behaviours.
/// </summary>
public abstract class InstantBehaviour : ScriptableObject {

    /// <summary>
    /// Activates the skill behaviour.
    /// </summary>
    public abstract void Activate(BaseCharacter caster);
}
