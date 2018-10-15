using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(AIControl))]
public class AttackPlayer : MonoBehaviour, IAIAction {

    /// <summary>
    /// How often the conditions for this action should be checked.
    /// </summary>
    public float checkFrequency = 0.5f;

    /// <summary>
    /// Reference to the NavMeshAgent.
    /// </summary>
    private NavMeshAgent _agent;

    /// <summary>
    /// Reference to the Skill Caster.
    /// </summary>
    private SkillCaster _skillCaster;

    /// <summary>
    /// Reference to the AI Controller.
    /// </summary>
    private AIControl _AIControl;

	// Use this for initialization
	private void Start () {
        _agent = GetComponent<NavMeshAgent>();
        _skillCaster = GetComponent<SkillCaster>();
        _AIControl = GetComponent<AIControl>();
        InvokeRepeating("Check", 0f, checkFrequency);
	}

    /// <summary>
    /// Checks for action conditions and adds them to the action queue if met.
    /// Does not add duplicates of this action to the queue.
    /// </summary>
    private void Check() {
        if (!_AIControl.ActionQueue.Contains(this) && Precondition()) {
            _AIControl.ActionQueue.Enqueue(this); // Add to action queue
        }
    }

    /// <summary>
    /// Checks if the AIControl has a target that is within range of attacking.
    /// </summary>
    /// <returns>True if target is in range, false otherwise</returns>
    public bool Precondition() {
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
    public void Action() {
        // Basic melee attack
        GetComponent<SkillCaster>().Cast(0, 0);
    }
    
}
