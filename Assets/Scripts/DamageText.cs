using UnityEngine;

/// <summary>
/// Class for damage text.
/// </summary>
public class DamageText : MonoBehaviour {

    /// <summary>
    /// How long to display the damage text for.
    /// </summary>
    public float aliveTime = 2f;

	// Use this for initialization
	private void Start () {
        Destroy(gameObject, aliveTime); // Destroy this gameobject after 2 seconds.
	}
}
