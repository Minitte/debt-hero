using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for the controlling AI behaviour.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIControl : MonoBehaviour {

    #region Statics

    /// <summary>
    /// For filtering out all layers except the player's.
    /// </summary>
    private static readonly int PLAYER_MASK = 1 << 10;
    
    /// <summary>
    /// How frequently the AI should check for aggro.
    /// </summary>
    private static readonly float AGGRO_CHECK_FREQUENCY = 0.5f;

    #endregion

    /// <summary>
    /// The aggro radius.
    /// </summary>
    public float aggroRadius = 5f;

    /// <summary>
    /// Reference to the NavMeshAgent.
    /// </summary>
    private NavMeshAgent _agent;

    /// <summary>
    /// The currently targeted player.
    /// </summary>
    private Transform _target;

    // Use this for initialization
    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
        _target = transform;

        // Periodically check for nearby players
        InvokeRepeating("CheckAggro", 0f, AGGRO_CHECK_FREQUENCY);
    }

    // Update is called once per frame
    private void Update() {
        // Look towards the target as if it were on the same elevation.
        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
    }
    
    /// <summary>
    /// Handles players that move within the aggro radius.
    /// </summary>
    private void CheckAggro() {
        // Check all colliders within the aggro radius that match the player mask
        Collider[] withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadius, PLAYER_MASK);

        // Length greater than zero means at least one player is within the aggro radius
        if (withinAggroColliders.Length > 0) {
            // Move to the player
            _target = withinAggroColliders[0].transform;
            _agent.destination = _target.position;
        } else {
            // Default target value
            _target = transform;
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
