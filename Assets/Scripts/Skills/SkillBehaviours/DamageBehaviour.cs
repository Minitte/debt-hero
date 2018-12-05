using UnityEngine;

/// <summary>
/// This is a class for skill behaviours that activate when damage is dealt.
/// </summary>
public abstract class DamageBehaviour : ScriptableObject {

    /// <summary>
    /// Represents one second of waiting time.
    /// </summary>
    protected WaitForSeconds _oneSecond;

    /// <summary>
    /// Called when this object is enabled.
    /// </summary>
    private void OnEnable() {
        _oneSecond = new WaitForSeconds(1f);
    }

    /// <summary>
    /// Behaviour that is activated after damage is dealt.
    /// ie. DoTs, status effects
    /// </summary>
    /// <param name="victim">The character that took damage</param>
    public abstract void OnDamageActivate(BaseCharacter dealer, BaseCharacter victim);
}
