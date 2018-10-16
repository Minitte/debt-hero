using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for the controlling AI behaviour.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SkillCaster))]
[RequireComponent(typeof(CharacterStats))]
public class AIController : MonoBehaviour {

    /// <summary>
    /// For filtering out all layers except the player's.
    /// </summary>
    private static readonly int PLAYER_MASK = 1 << 10;

    /// <summary>
    /// The currently targeted gameobject.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The aggro radius.
    /// </summary>
    public float aggroRadius = 5f;

    /// <summary>
    /// Reference to the NavMeshAgent.
    /// </summary>
    private NavMeshAgent _agent;

    /// <summary>
    /// Reference to the SkillCaster.
    /// </summary>
    private SkillCaster _skillCaster;

    /// <summary>
    /// A queue of actions for the AI.
    /// </summary>
    private Queue<AIAction> _actionQueue;

    /// <summary>
    /// Reference to the AI's health bar.
    /// </summary>
    private HealthBar _healthBar;

    /// <summary>
    /// Property variable for the action queue.
    /// </summary>
    public Queue<AIAction> ActionQueue {
        get { return _actionQueue; }
    }

    // Use this for initialization
    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
        _skillCaster = GetComponent<SkillCaster>();
        _actionQueue = new Queue<AIAction>();
        GetComponent<CharacterStats>().OnDamageTaken += DrawHealthBar;
        GetComponent<CharacterStats>().OnDeath += Die;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // Don't do anything if already attacking
        if (!_skillCaster.isCasting) {

            // If there is a player in aggro radius, it will be assigned to target
            if (CheckAggro()) {
                _agent.destination = target.position; // Move to the target

                // Keep facing the target
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            }

            // If there are actions in the queue, perform them
            if (_actionQueue.Count > 0) {
                _actionQueue.Dequeue().Action();
            }
        }

        // Force model position and rotation to stay the same
        transform.GetChild(0).position = transform.position;
        transform.GetChild(0).rotation = transform.rotation;
    }

    /// <summary>
    /// Checks if any players are within the aggro radius.
    /// </summary>
    /// <param name="target">A transform to output to</param>
    /// <returns>Whether a player is within the aggro radius</returns>
    private bool CheckAggro() {
        // Check all colliders within the aggro radius that match the player mask
        Collider[] withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadius, PLAYER_MASK);

        // Array length greater than zero means at least one player is within the aggro radius
        if (withinAggroColliders.Length > 0) {
            Transform aggroedTarget = withinAggroColliders[0].transform; // Target is the player's transform

            // Check if the target is behind a wall
            RaycastHit hit;
            if (Physics.Raycast(transform.position, aggroedTarget.position - transform.position, out hit)) {
                if (hit.collider.tag == "Wall") {
                    return false; // Player is behind wall, don't aggro
                }
            }

            target = aggroedTarget;
            return true;
        } else {
            target = null; // No targets in aggro range
            return false;
        }
    }

    /// <summary>
    /// Draws a health bar over this gameobject when damage is first taken.
    /// </summary>
    /// <param name="physAtkdamge">Unused</param>
    /// <param name="magicAtkdamage">Unused</param>
    private void DrawHealthBar(float physAtkdamge, float magicAtkdamage) {
        if (_healthBar == null) {
            _healthBar = FloatingTextController.instance.CreateHealthBar(gameObject);
        }
    }

    /// <summary>
    /// Draws a red sphere to indicate the aggro radius in the editor.
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }

    /// <summary>
    /// Additional cleanup for when this gameobject dies.
    /// </summary>
    private void Die() {
        Destroy(_healthBar.gameObject); // Get rid of the health bar
    }
}
