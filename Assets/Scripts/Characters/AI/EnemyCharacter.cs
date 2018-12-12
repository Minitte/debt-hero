using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for the controlling AI behaviour.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyCharacter : BaseCharacter {

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
    public float aggroRadius = 25f;

    /// <summary>
    /// Reference to the AI's health bar.
    /// </summary>
    private HealthBar _healthBar;

    /// <summary>
    /// A queue of actions for the AI.
    /// </summary>
    private Queue<AIAction> _actionQueue;

    /// <summary>
    /// Collider array used for storing results of physics overlap spheres.
    /// </summary>
    private Collider[] _withinAggroColliders;

    /// <summary>
    /// Property variable for the action queue.
    /// </summary>
    public Queue<AIAction> ActionQueue {
        get { return _actionQueue; }
    }

    // Use this for initialization
    private void Start() {
        _actionQueue = new Queue<AIAction>();
        _withinAggroColliders = new Collider[1];
        characterStats.OnHealthChanged += DrawHealthBar;
        characterStats.OnDeath += Die;

        // Check for aggro five times per second
        InvokeRepeating("CheckAggro", 0f, 0.2f);
    }

    // Update is called once per frame
    private void Update() {
        animator.SetFloat("Speed", agent.velocity.magnitude); // Run animation

        // Don't do anything if already attacking
        if (!animatorStatus.isCasting) {

            // If there is a player in aggro radius, it will be assigned to target
            if (target != null) {
                agent.destination = target.position; // Move to the target

                // Keep facing the target
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            }

            // If there are actions in the queue, perform them
            if (_actionQueue.Count > 0) {
                _actionQueue.Dequeue().Action();
            }
        }

        // Force model position to stay the same
        transform.GetChild(0).position = transform.position;
    }

    /// <summary>
    /// Checks if any players are within the aggro radius.
    /// </summary>
    /// <param name="target">A transform to output to</param>
    /// <returns>Whether a player is within the aggro radius</returns>
    private bool CheckAggro() {
        // Check all colliders within the aggro radius that match the player mask
        int hits = Physics.OverlapSphereNonAlloc(transform.position, aggroRadius, _withinAggroColliders, PLAYER_MASK);

        // Array length greater than zero means at least one player is within the aggro radius
        if (hits > 0) {
            Transform aggroedTarget = _withinAggroColliders[0].transform; // Target is the player's transform

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
    private void DrawHealthBar() {
        if (_healthBar == null && characterStats.currentHp > 0f) {
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
        // Delay removal of gameobject
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);

        if (_healthBar != null) {
            Destroy(_healthBar.gameObject); // Get rid of the health bar
        }

        skillCaster.ClearSkillObjects(); // Remove all active skill objects
    }
}
