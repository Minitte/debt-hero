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

    /// <summary>
    /// Preset contaning a canvas, image, text.
    /// </summary>
    public HealthBar healthbar;

    /// <summary>
    /// Instance to the preset.
    /// </summary>
    private HealthBar Healthbarinstance;

    public StationaryResourcesUI stationaryResourcesUI;

    private StationaryResourcesUI stationaryResourcesUIinstance;


    // Use this for initialization
    private void Start() {
        _agent = GetComponent<NavMeshAgent>();

        // Make sure that this gameobject has a NavMeshAgent
        if (_agent == null) {
            Debug.Log("Attempted to run PlayerController script without a NavMeshAgent.");
            Destroy(this);
        }


        //Instantiates the instance to the Healthbar prefab.
        Healthbarinstance = Instantiate(healthbar) as HealthBar;

        //For test purposes its known as Player. Can change later.
        Healthbarinstance.BarGenerateName("Player");

        //Setting a red color.
        Healthbarinstance.BarColor(86, 163, 65, 255);

        //For test purposes instantiating a Stationary UI object.
        stationaryResourcesUIinstance = Instantiate(stationaryResourcesUI) as StationaryResourcesUI;
    }

    // Update is called once per frame
    private void Update() {

        // If the right mouse button is held down
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

        // If there is a controller plugged in
        if (Input.GetJoystickNames().Length > 0) {

            // Horizontal and vertical input values of the joystick
            float horizontal = Input.GetAxis("HorizontalAnalog");
            float vertical = Input.GetAxis("VerticalAnalog");

            // Move in the direction of the joystick
            Vector3 goal = gameObject.transform.position + new Vector3(horizontal, gameObject.transform.position.y, vertical).normalized;
            _agent.destination = goal;
        }
        Healthbarinstance.BarPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
