using UnityEngine;

/// <summary>
/// This is a class that heals gameobjects it collides with.
/// </summary>
public class Heal : MonoBehaviour {

    /// <summary>
    /// The amount to heal for.
    /// </summary>
    public float healingAmount;

    /// <summary>
    /// For detecting collisions.
    /// </summary>
    /// <param name="other">The collision object collided with</param>
    private void OnTriggerEnter(Collider other) {
        // Only deal damage to Player or AI tags, and no friendly fire
        if ((other.tag == "AI" || other.tag == "Player")) {

            // Apply a heal to the target
            other.GetComponent<CharacterStats>().TakeHealing(healingAmount);
        }
    }
}
