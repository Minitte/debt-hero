using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for handling player input and keybindings.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerInput : MonoBehaviour {

    /// <summary>
    /// Path to the keybinds text file.
    /// </summary>
    private static readonly string KEYBINDS_PATH = "keybinds.txt";

    /// <summary>
    /// Map for keybinds.
    /// </summary>
    private Dictionary<string, KeyCode> _keybinds;

    // Use this for initialization
    private void Start() {
        _keybinds = new Dictionary<string, KeyCode>();

        // Check if there are existing keybind settings
        if (File.Exists(KEYBINDS_PATH)) {
            LoadKeybinds();
        } else {
            // Default keybinds
            _keybinds.Add("Attack", KeyCode.Mouse0);

            //SaveKeybinds();
        }
    }

    // Update is called once per frame
    private void Update() {
        // Check if the player pressed or is holding the attack button
        if (Input.GetKeyDown(_keybinds["Attack"])) {
            // Ray from camera to the clicked position in world space
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray collided with a gameobject
            if (Physics.Raycast(ray, out hit, 100)) {

                // Attack the selected point
                BasicAttack(hit.point);
            }
        }
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
    /// <param name="enemy">The enemy gameobject to attack</param>
    public void BasicAttack(Vector3 attackPoint) {
        // Position to look towards
        Vector3 lookPos = attackPoint;
        lookPos.y = transform.position.y;

        // Face towards the attack point and stop movement
        transform.LookAt(lookPos);
        GetComponent<NavMeshAgent>().destination = transform.position;

        transform.Find("TestSword").GetComponent<BasicAttackMelee>().Attack();

        // Generate a test projectile object
        /*
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
