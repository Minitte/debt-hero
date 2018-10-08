using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for handling controller input and keybindings.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerInputController : MonoBehaviour {

    /// <summary>
    /// Map for keybinds.
    /// </summary>
    private Dictionary<string, KeyCode> _keybinds;

    /// <summary>
    /// The NavMeshAgent associated with this gameobject.
    /// </summary>
    private NavMeshAgent _agent;

    // Use this for initialization
    private void Start() {
        _keybinds = new Dictionary<string, KeyCode>();
        _agent = GetComponent<NavMeshAgent>();

        // Default keybinds
        _keybinds.Add("Attack", KeyCode.JoystickButton2);
    }

    // Update is called once per frame
    private void Update() {
        // Check if the player pressed or is holding the attack button
        if (Input.GetKeyDown(_keybinds["Attack"])) {
            // Attack in front of the player
            BasicAttack(transform.position + transform.forward.normalized);
        }

        // Horizontal and vertical input values of the joystick
        float horizontal = Input.GetAxis("HorizontalAnalog");
        float vertical = Input.GetAxis("VerticalAnalog");

        if (horizontal != 0f || vertical != 0f) {
            // Move in the direction of the joystick
            Vector3 goal = gameObject.transform.position + new Vector3(horizontal, gameObject.transform.position.y, vertical).normalized;
            _agent.destination = goal;
        }
    }

    /// <summary>
    /// Perform a basic attack.
    /// </summary>
    /// <param name="enemy">The enemy gameobject to attack</param>
    public void BasicAttack(Vector3 attackPoint) {
        // Position to look towards
        Vector3 lookPos = attackPoint;
        lookPos.y = transform.position.y;

        // Face towards the attack point and stop movement
        transform.LookAt(lookPos);
        GetComponent<NavMeshAgent>().destination = transform.position;

        // Attack
        transform.Find("TestSword").GetComponent<BasicAttackMelee>().Attack();
    }
}
