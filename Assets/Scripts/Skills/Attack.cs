using UnityEngine;

/// <summary>
/// This is a class that deals damage to gameobjects collided with.
/// </summary>
public class Attack : MonoBehaviour {

    /// <summary>
    /// The physical attack damage of the user.
    /// </summary>
    private float _physAtkdamage;

    /// <summary>
    /// The magical attack damage of the user.
    /// </summary>
    private float _magicAtkdamage;

    /// <summary>
    /// Reference to the animator status.
    /// </summary>
    private AnimatorStatus _animatorStatus;

    /// <summary>
    /// Reference to this gameobject's Collider component.
    /// </summary>
    private Collider _collider;

    // Use this for initialization
    private void Start () {
        _physAtkdamage = transform.parent.GetComponent<CharacterStats>().physAtk;
        _magicAtkdamage = transform.parent.GetComponent<CharacterStats>().magicAtk;
        _animatorStatus = transform.parent.GetChild(0).GetComponent<AnimatorStatus>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    private void Update() {
        // Only deal damage within the damage window of the animation
        if (_animatorStatus.canDealDamage) {
            _collider.enabled = true;
        } else {
            // Check if the damage window is over
            if (_collider.enabled) {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// For detecting collisions.
    /// </summary>
    /// <param name="other">The collision object collided with</param>
    private void OnTriggerEnter(Collider other) {
        // Only deal damage to Player or AI tags, and no friendly fire
        if ((other.tag == "AI" || other.tag == "Player") && other.tag != transform.parent.tag) {

            // Apply damage to the enemy
            other.GetComponent<CharacterStats>().TakeDamage(_physAtkdamage, _magicAtkdamage);
        }
    }
}
