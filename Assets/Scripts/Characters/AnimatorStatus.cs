using UnityEngine;

/// <summary>
/// This is a class for communication between the animator and the control components.
/// </summary>
public class AnimatorStatus : MonoBehaviour {

    /// <summary>
    /// Whether an attack animation is currently being played.
    /// </summary>
    public bool isCasting;

    /// <summary>
    /// Whether the animation is at a point where it should deal damage.
    /// Used to create damage windows.
    /// </summary>
    public bool canDealDamage;

    /// <summary>
    /// Triggers an attack. Used for skill outputs that depend on the animation.
    /// </summary>
    public void TriggerAttack() {
        transform.parent.GetComponent<SkillCaster>().DelayedCast();
    }

    public void DoneCasting() {
        isCasting = false;
    }
}
