using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for handling input and keybindings.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerInputHandler : MonoBehaviour {

    /// <summary>
    /// Path to the keybinds text file.
    /// </summary>
    private static readonly string KEYBINDS_PATH = "keybinds.txt";

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

        // Check if there are existing keybind settings
        if (File.Exists(KEYBINDS_PATH)) {
            LoadKeybinds();
        } else {
            // Default keybinds
            _keybinds.Add("AttackKeyboard", KeyCode.Mouse0);
            _keybinds.Add("MoveKeyboard", KeyCode.Mouse1);

            _keybinds.Add("AttackController", KeyCode.JoystickButton2);

            //SaveKeybinds();
        }
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
                // Attack in front of the player
                BasicAttack(transform.position + transform.forward.normalized);
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
    /// Saves all the keybinds to a file.
    /// Format is: "action=keycode".
    /// </summary>
    public void SaveKeybinds() {
        using (StreamWriter file = new StreamWriter(KEYBINDS_PATH)) {
            foreach (string action in _keybinds.Keys) {
                file.WriteLine(action + "=" + _keybinds[action].ToString());
            }
        }
    }

    /// <summary>
    /// Loads all the keybinds from a file.
    /// </summary>
    public void LoadKeybinds() {
        // Clear the keybinds map before loading from file
        _keybinds.Clear();

        using (StreamReader file = new StreamReader(KEYBINDS_PATH)) {
            // Grab all the lines into a string array
            string[] lines = file.ReadToEnd().Split('\n');

            // Iterate through each line except the last line which will be empty
            for (int i = 0; i < lines.Length - 1; i++) {
                // Split into action and keycode
                string[] split = lines[i].Split('=');
                string action = split[0];
                KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), split[1]);

                // Add to the keybinds map
                _keybinds.Add(action, keyCode);
            }
        }
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
