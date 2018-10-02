using UnityEngine;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour {

    /// <summary>
    /// Keybind for basic attacking.
    /// </summary>
    private string _attack;

	// Use this for initialization
	void Start () {
        // Default keybinds
        _attack = "mouse 0";
	}
	
	// Update is called once per frame
	void Update () {
        // Check if the player pressed or is holding the attack button
        if (Input.GetKeyDown(_attack)) {
            // Ray from camera to the clicked position in world space
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray collided with an enemy
            if (Physics.Raycast(ray, out hit, 100)) {
                    
                // Attack the selected target
                BasicAttack(hit.point);
            }
        }
	}

    /// <summary>
    /// Perform a basic attack.
    /// </summary>
    /// <param name="enemy">The enemy gameobject to attack</param>
    public void BasicAttack(Vector3 attackPoint) {
        // Generate a test projectile object
        GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        projectile.transform.position = gameObject.transform.position;
        projectile.GetComponent<BoxCollider>().isTrigger = true;
        projectile.AddComponent<Rigidbody>().isKinematic = true;
        projectile.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        projectile.AddComponent<BasicAttackProjectile>().Instantiate(attackPoint - transform.position);

    }
}
