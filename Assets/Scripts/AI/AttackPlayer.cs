using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// An action for the AI that attacks the player when in range.
/// </summary>
[RequireComponent(typeof(AIControl))]
public class AttackPlayer : AIAction {

    /// <summary>
    /// Checks for action conditions and adds them to the action queue if met.
    /// Does not add duplicates of this action to the queue.
    /// </summary>
    public override void Check() {
        if (!_AIControl.ActionQueue.Contains(this) && Precondition()) {
            _AIControl.ActionQueue.Enqueue(this); // Add to action queue
        }
    }

    /// <summary>
    /// Checks if the AIControl has a target that is within range of attacking.
    /// </summary>
    /// <returns>True if target is in range, false otherwise</returns>
    public override bool Precondition() {
        // Check if in range to attack player (0.5f is temporary)
        if (_AIControl.target != null 
            && Vector3.Distance(_AIControl.target.position, transform.position) <= _agent.stoppingDistance + 0.5f) {
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
        GetComponent<SkillCaster>().Cast(0, 0);
    }
    
}
