using UnityEngine;

/// <summary>
/// Class for damage text.
/// </summary>
public class DamageText : MonoBehaviour {

    /// <summary>
    /// How long to display the damage text for.
    /// </summary>
    public float aliveTime = 2f;

    private void Start() {
        Destroy(gameObject, aliveTime);
    }

    private void Update() {
        //transform.rotation = Quaternion.identity;
    }
}
