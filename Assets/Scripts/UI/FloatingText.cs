using System.Collections;
using UnityEngine;

/// <summary>
/// Class for damage text.
/// </summary>
public class FloatingText : MonoBehaviour {

    /// <summary>
    /// How long to display the damage text for.
    /// </summary>
    public float aliveTime;

    /// <summary>
    /// Reference to this gameobject's CanvasRenderer.
    /// </summary>
    private CanvasRenderer _canvasRenderer;

    private void Start() {
        _canvasRenderer = GetComponent<CanvasRenderer>();

        Destroy(transform.parent.gameObject, aliveTime);
    }

    private void Update() {
        // Fade out effect
        _canvasRenderer.SetAlpha(_canvasRenderer.GetAlpha() - Time.deltaTime / aliveTime);
    }
}
