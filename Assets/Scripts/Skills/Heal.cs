using UnityEngine;

/// <summary>
/// This is a class that heals gameobjects it collides with.
/// </summary>
public class Heal : MonoBehaviour {

    /// <summary>
    /// The radius of the heal.
    /// </summary>
    public float healRadius;

    /// <summary>
    /// The amount to heal for.
    /// </summary>
    public float healingAmount;

    private void Start() {
        // Apply a heal within the radius of the spell
        Collider[] withinRadiusColliders = Physics.OverlapSphere(transform.position, healRadius, (1 << transform.parent.gameObject.layer));
        foreach (Collider c in withinRadiusColliders) {
            c.GetComponent<CharacterStats>().TakeHealing(healingAmount);
        }
    }
}
