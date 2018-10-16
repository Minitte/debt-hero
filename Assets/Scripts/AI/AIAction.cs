using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is an abstract class for AI Actions.
/// </summary>
[RequireComponent(typeof(AIController))]
[RequireComponent(typeof(CharacterStats))]
public abstract class AIAction : MonoBehaviour {

    /// <summary>
    /// Information about the skill.
    /// </summary>
    public int skillNum, skillID;

    /// <summary>
    /// How often the conditions for this action should be checked.
    /// </summary>
    public float checkFrequency = 0.5f;

    /// <summary>
    /// Reference to the NavMeshAgent.
    /// </summary>
    protected NavMeshAgent _agent;

    /// <summary>
    /// Reference to the Skill Caster.
    /// </summary>
    protected SkillCaster _skillCaster;

    /// <summary>
    /// Reference to the AI Controller.
    /// </summary>
    protected AIController _AIControl;

    /// <summary>
    /// Reference to the character stats
    /// </summary>
    protected CharacterStats _characterStats;

    // Use this for initialization
    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
        _skillCaster = GetComponent<SkillCaster>();
        _AIControl = GetComponent<AIController>();
        _characterStats = GetComponent<CharacterStats>();
        InvokeRepeating("Check", 0f, checkFrequency);
    }

    /// <summary>
    /// Checks for action conditions and adds them to the action queue if met.
    /// Does not add duplicates of this action to the queue.
    /// </summary>
    public void Check() {
        if (!_AIControl.ActionQueue.Contains(this) && Precondition()) {
            _AIControl.ActionQueue.Enqueue(this); // Add to action queue
        }
    }

    /// <summary>
    /// Checks for conditions that must be true before this script's action can be done.
    /// </summary>
    /// <returns>True if all conditions are met, false otherwise</returns>
    public abstract bool Precondition();

    /// <summary>
    /// The action to perform.
    /// </summary>
    public abstract void Action();
}
