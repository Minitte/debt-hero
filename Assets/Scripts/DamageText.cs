using UnityEngine;

/// <summary>
/// Class for damage text.
/// </summary>
public class DamageText : MonoBehaviour {

    /// <summary>
    /// How long to display the damage text for.
    /// </summary>
    public float aliveTime;

    private void Start() {
        Destroy(transform.parent.gameObject, aliveTime);
    }

    private void Update() {
        //transform.rotation = Quaternion.identity;
    }
}
