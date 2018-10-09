using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for the controlling AI behaviour.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIControl : MonoBehaviour {

    /// <summary>
    /// For filtering out all layers except the player's.
    /// </summary>
    private static readonly int PLAYER_MASK = 1 << 10;

    /// <summary>
    /// The aggro radius.
    /// </summary>
    public float aggroRadius = 5f;

    /// <summary>
    /// Reference to the NavMeshAgent.
    /// </summary>
    private NavMeshAgent _agent;

    // Use this for initialization
    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Transform target;

        // If there is a player in aggro radius, it will be assigned to target
        if (CheckAggro(out target)) {
            _agent.destination = target.position; // Move to the player

            // If in melee range
            if (Vector3.Distance(target.position, transform.position) <= _agent.stoppingDistance) {
                // Basic melee attack
                transform.Find("TestSword").GetComponent<BasicAttackMelee>().Attack();

                // Keep facing the target
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            }
        }
    }

    /// <summary>
    /// Checks if any players are within the aggro radius.
    /// </summary>
    /// <param name="target">A transform to output to</param>
    /// <returns>Whether a player is within the aggro radius</returns>
    private bool CheckAggro(out Transform target) {
        // Check all colliders within the aggro radius that match the player mask
        Collider[] withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadius, PLAYER_MASK);

        // Array length greater than zero means at least one player is within the aggro radius
        if (withinAggroColliders.Length > 0) {
            target = withinAggroColliders[0].transform; // Target is the player's transform

            // Check if the target is behind a wall
            RaycastHit hit;
            if (Physics.Raycast(transform.position, target.position - transform.position, out hit)) {
                if (hit.collider.tag == "Wall") {
                    return false; // Player is behind wall, don't aggro
                }
            }

            return true;
        } else {
            target = transform; // Default target value
            return false;
        }
    }

    /// <summary>
    /// Draws a red sphere to indicate the aggro radius in the editor.
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
