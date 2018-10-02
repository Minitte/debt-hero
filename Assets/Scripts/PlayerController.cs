using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for player movement.
/// </summary>
public class PlayerController : MonoBehaviour {

    /// <summary>
    /// The NavMeshAgent associated with this gameobject.
    /// </summary>
    private NavMeshAgent _agent;

    // Use this for initialization
    void Start() {
        _agent = GetComponent<NavMeshAgent>();

        // Make sure that this gameobject has a NavMeshAgent
        if (_agent == null) {
            Debug.Log("Attempted to run PlayerController script without a NavMeshAgent.");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update() {

        // Right mouse button held down
        if (Input.GetMouseButton(1)) {

            // Ray from camera to the clicked position in world space
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray collided with any rigid bodies
            if (Physics.Raycast(ray, out hit, 100)) {
                // Attempt to move to the collision point
                _agent.destination = hit.point;
            }
        }
    }
}
