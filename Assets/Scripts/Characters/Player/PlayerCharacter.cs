using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for handling input and keybindings.
/// </summary>
public class PlayerCharacter : BaseCharacter {

    /// <summary>
    /// Used for inputs that involve the mouse position
    /// </summary>
    private Vector3 _clickedPoint;

    /// <summary>
    /// The current path that the agent is taking.
    /// </summary>
    private NavMeshPath _path;

    // Use this for initialization
    private void Start() {
        _path = new NavMeshPath();
        characterStats = PlayerManager.instance.GetComponent<CharacterStats>();
        characterStats.OnDeath += Die;
    }

    // Update is called once per frame
    private void Update() {
        animator.SetFloat("Speed", agent.velocity.magnitude); // Run animation

        // Don't accept input if the character is casting something
        if (!animatorStatus.isCasting) {
            // Handle player input
            HandleMouseInput();
            HandleKeyboardInput();

            // If a controller is plugged in
            if (Input.GetJoystickNames().Length > 0) {
                HandleControllerInput();
            }
        }    

        // Force model position and rotation to stay the same
        transform.GetChild(0).position = transform.position;
        transform.GetChild(0).rotation = transform.rotation;
    }

    /// <summary>
    /// Handles all mouse input from the player.
    /// </summary>
    private void HandleMouseInput() {
        // Check if the player pressed or is holding the move key
        if (Input.GetMouseButton(0)) {
            if (GetClickedPoint(out _clickedPoint)) {
                transform.LookAt(new Vector3(_clickedPoint.x, transform.position.y, _clickedPoint.z));
                
                // Get the closest nav mesh position
                NavMeshHit navHitPosition;
                NavMesh.SamplePosition(_clickedPoint, out navHitPosition, 20f, NavMesh.AllAreas);

                // Calculate path to that location and move to it
                agent.CalculatePath(navHitPosition.position, _path);
                agent.path = _path;
            }
        }

        // Check if the player pressed the attack key
        if (Input.GetButtonDown("Basic Attack") && GetClickedPoint(out _clickedPoint)) {
            if (skillCaster.Cast(0)) { // Attempt to attack
                transform.LookAt(new Vector3(_clickedPoint.x, transform.position.y, _clickedPoint.z));
                agent.ResetPath();
            }
            return;
        }
    }

    /// <summary>
    /// Handles keyboard input from the player.
    /// </summary>
    private void HandleKeyboardInput() {
        // Check if the player pressed the Skill 1 key
        if (Input.GetButtonDown("Dash")) {
            skillCaster.Cast(1);
            return;
        }
    }

    /// <summary>
    /// Handles controller input from the player.
    /// </summary>
    private void HandleControllerInput() {
        // Check if the player pressed or is holding the controller attack key
        if (Input.GetButtonDown("Basic Attack")) {
            if (skillCaster.Cast(0)) {
                agent.ResetPath();
            }
        }

        // Horizontal and vertical input values of the left joystick
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if the player is moving the left joystick
        if (horizontal != 0f || vertical != 0f) {
            // Move in the direction of the left joystick
            Vector3 goal = gameObject.transform.position + new Vector3(horizontal, gameObject.transform.position.y, vertical).normalized;
            agent.destination = goal;
        }
    }
    /// <summary>
    /// Checks if the mouse position is colliding with any gameobject's colliders.
    /// </summary>
    /// <param name="clickedPoint">A Vector3 to output to</param>
    /// <returns>Whether a collider was found or not</returns>
    public static bool GetClickedPoint(out Vector3 clickedPoint) {
        // Ray from camera to the clicked position in world space
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray collided with anything
        if (Physics.Raycast(ray, out hit, 100)) {
            // Return the collision point
            clickedPoint = hit.point;
            return true;
        }

        // No collision point
        clickedPoint = Vector3.zero;
        return false;
    }

    /// <summary>
    /// Additional cleanup for when this gameobject dies.
    /// </summary>
    private void Die() {
        Destroy(gameObject);
    }
}
