using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for handling input and keybindings.
/// </summary>
public class PlayerInputHandler : MonoBehaviour {

    /// <summary>
    /// The maximum allowed difference in y value between the clicked
    /// position and the current position.
    /// </summary>
    private static readonly float MAX_CLIMB = 2f;

    /// <summary>
    /// Map for keybinds.
    /// </summary>
    private Keybinds _keybinds;

    /// <summary>
    /// The NavMeshAgent associated with this gameobject.
    /// </summary>
    private NavMeshAgent _agent;

    /// <summary>
    /// Reference to the gameobject's skill caster.
    /// </summary>
    private SkillCaster _skillCaster;

    /// <summary>
    /// Reference to the gameobject's animator.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Reference to the animator status.
    /// </summary>
    private AnimatorStatus _animatorStatus;

    /// <summary>
    /// Flag for if the player is able to move.
    /// Used to stop movement when starting actions in the middle of movement.
    /// </summary>
    private bool _ableToMove;

    // Use this for initialization
    private void Start() {
        _keybinds = new Keybinds();
        _agent = GetComponent<NavMeshAgent>();
        _skillCaster = GetComponent<SkillCaster>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _animatorStatus = transform.GetChild(0).GetComponent<AnimatorStatus>();
        _ableToMove = true;
    }

    // Update is called once per frame
    private void Update() {
        if (_agent.remainingDistance < 0.1f) {
            _animator.SetFloat("Speed", 0f); // Stop walk animation
        }
        // Don't accept input if the character is casting something
        if (!_animatorStatus.isCasting && _ableToMove) {
            // Used for inputs that involve the mouse position
            Vector3 clickedPoint;

            // Check if the player pressed or is holding the move key
            if (Input.GetKey(_keybinds["MoveKeyboard"])) {
                if (GetClickedPoint(out clickedPoint)) {
                    transform.LookAt(new Vector3(clickedPoint.x, transform.position.y, clickedPoint.z));

                    // Check for massive elevation difference between clicked point and current position
                    if (Mathf.Abs(transform.position.y - clickedPoint.y) > MAX_CLIMB) {
                        _agent.destination = new Vector3(clickedPoint.x, transform.position.y, clickedPoint.z); // Discard clicked y point
                    } else {
                        _agent.destination = clickedPoint;
                    }

                    _animator.SetFloat("Speed", 1f); // Start walk animation
                }
            }

            // Check if the player pressed the attack key
            if (Input.GetKeyDown(_keybinds["AttackKeyboard"])) {
                if (GetClickedPoint(out clickedPoint)) {
                    transform.LookAt(new Vector3(clickedPoint.x, transform.position.y, clickedPoint.z));
                    _skillCaster.Cast(0, 0);
                    StartCoroutine(StopMovement(0.5f)); // Stop movement
                    return;
                }
            }

            // Check if the player pressed or is holding the move key
            if (Input.GetKey(_keybinds["Skill1"])) {
                _skillCaster.Cast(1, 1);
            }

            // If a controller is plugged in
            if (Input.GetJoystickNames().Length > 0) {
                // Check if the player pressed or is holding the controller attack key
                if (Input.GetKeyDown(_keybinds["AttackController"])) {
                    StartCoroutine(StopMovement(0.5f)); // Stop movement
                    _skillCaster.Cast(0, 0);
                }

                // Horizontal and vertical input values of the joystick
                float horizontal = Input.GetAxis("HorizontalAnalog");
                float vertical = Input.GetAxis("VerticalAnalog");

                // Check if the player is moving the joystick
                if (horizontal != 0f || vertical != 0f) {
                    // Move in the direction of the joystick
                    Vector3 goal = gameObject.transform.position + new Vector3(horizontal, gameObject.transform.position.y, vertical).normalized;
                    _agent.destination = goal;
                }
            }
        }

        // Force model position and rotation to stay the same
        transform.GetChild(0).position = transform.position;
        transform.GetChild(0).rotation = transform.rotation;
    }

    /// <summary>
    /// Checks if the mouse position is colliding with any gameobject's colliders.
    /// </summary>
    /// <param name="clickedPoint">A Vector3 to output to</param>
    /// <returns>Whether a collider was found or not</returns>
    private bool GetClickedPoint(out Vector3 clickedPoint) {
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
    /// Stops movement of the player.
    /// </summary>
    /// <param name="seconds">How many seconds to stop movement for.</param>
    private IEnumerator StopMovement(float seconds) {
        _animator.SetFloat("Speed", 0f); // Stop walk animation
        _ableToMove = false;
        _agent.ResetPath();
        yield return new WaitForSeconds(seconds);
        _ableToMove = true;
    }
}
