using UnityEngine;

/// <summary>
/// Class for floating text.
/// </summary>
public class FloatingText : MonoBehaviour {

    /// <summary>
    /// An offset to the floating text's position.
    /// </summary>
    public Vector3 textOffset;

    /// <summary>
    /// How long to display the floating text for.
    /// </summary>
    public float aliveTime;

    /// <summary>
    /// Reference to this gameobject's CanvasRenderer.
    /// </summary>
    private CanvasRenderer _canvasRenderer;

    /// <summary>
    /// The gameobject that this floating text belongs to.
    /// e.g. For damage text, it's the gameobject that took damage.
    /// </summary>
    private Transform _owner;

    /// <summary>
    /// Property variable for owner.
    /// </summary>
    public Transform Owner {
        set {
            _owner = value;
        }
    }

    private void Start() {
        _canvasRenderer = GetComponent<CanvasRenderer>();
        Destroy(transform.parent.gameObject, aliveTime);
    }

    private void Update() {
        // Fade out effect
        _canvasRenderer.SetAlpha(_canvasRenderer.GetAlpha() - Time.deltaTime / aliveTime);

        // Update position
        if (_owner != null) {
            transform.parent.position = Camera.main.WorldToScreenPoint(_owner.position + textOffset);
        }
    }
}
