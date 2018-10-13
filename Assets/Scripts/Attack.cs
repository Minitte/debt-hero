using System.Collections;
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

    // Use this for initialization
    void Start () {
        _physAtkdamage = transform.parent.GetComponent<CharacterStats>().physAtk;
        _magicAtkdamage = transform.parent.GetComponent<CharacterStats>().magicAtk;
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
