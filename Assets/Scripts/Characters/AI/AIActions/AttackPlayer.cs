using UnityEngine;

/// <summary>
/// An action for the AI that attacks the player when in range.
/// </summary>
public class AttackPlayer : AIAction {

    /// <summary>
    /// Checks if the AIControl has a target that is within range of attacking.
    /// </summary>
    /// <returns>True if target is in range, false otherwise</returns>
    public override bool Precondition() {
        // Check if in range to attack player
        if (_AIControl.target != null 
            && Vector3.Distance(_AIControl.target.position, transform.position) <= _characterStats.attackRange) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Casts a basic attack spell.
    /// </summary>
    public override void Action() {
        // Basic melee attack
        _skillCaster.Cast(skillNum);
    }
}
