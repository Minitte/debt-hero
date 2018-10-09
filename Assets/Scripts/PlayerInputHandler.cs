using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for handling input and keybindings.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerInputHandler : MonoBehaviour {

    /// <summary>
    /// Map for keybinds.
    /// </summary>
    private Keybinds _keybinds;

    /// <summary>
    /// The NavMeshAgent associated with this gameobject.
    /// </summary>
    private NavMeshAgent _agent;

    // Use this for initialization
    private void Start() {
        _keybinds = new Keybinds();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update() {
        // Used for inputs that involve the mouse position
        Vector3 clickedPoint;

        // Check if the player pressed the attack key
        if (Input.GetKeyDown(_keybinds["AttackKeyboard"])) {
            if (GetClickedPoint(out clickedPoint)) {
                BasicAttack(clickedPoint);
            }
        }

        // Check if the player pressed or is holding the move key
        if (Input.GetKey(_keybinds["MoveKeyboard"])) {

            if (GetClickedPoint(out clickedPoint)) {
                _agent.destination = clickedPoint;
            }

            // Position to look towards
            Vector3 lookPos = _agent.destination;
            lookPos.y = transform.position.y; // Prevents gameobject from looking up or down

            // Face towards the destination
            transform.LookAt(lookPos);
        }

        // If a controller is plugged in
        if (Input.GetJoystickNames().Length > 0) {
            // Check if the player pressed or is holding the controller attack key
            if (Input.GetKeyDown(_keybinds["AttackController"])) {
                BasicAttack(transform.position + transform.forward.normalized); // Attack in front of the player
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
    /// Perform a basic attack.
    /// </summary>
    /// <param name="attackPoint">The point to attack</param>
    public void BasicAttack(Vector3 attackPoint) {
        // Position to look towards
        Vector3 lookPos = attackPoint;
        lookPos.y = transform.position.y;

        // Face towards the attack point and stop movement
        transform.LookAt(lookPos);
        GetComponent<NavMeshAgent>().destination = transform.position;

        // Basic melee attack
        transform.Find("TestSword").GetComponent<BasicAttackMelee>().Attack();

        /*
        // Generate a test projectile object
        GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        projectile.transform.position = gameObject.transform.position;
        projectile.GetComponent<BoxCollider>().isTrigger = true;
        projectile.AddComponent<Rigidbody>().isKinematic = true;
        projectile.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        // Get the player's damage
        CharacterStats characterInfo = gameObject.GetComponent<CharacterStats>();
        projectile.AddComponent<BasicAttackProjectile>().Instantiate(attackPoint - transform.position, characterInfo.physAtk, characterInfo.magicAtk);
        */
    }
}
