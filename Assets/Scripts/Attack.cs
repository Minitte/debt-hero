using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(gameObject, 0.5f);
    }

    /// <summary>
    /// For detecting collisions.
    /// </summary>
    /// <param name="other">The collision object collided with</param>
    private void OnTriggerEnter(Collider other) {
        // Only deal damage to Player or AI tags, and no friendly fire
        if ((other.tag == "AI" || other.tag == "Player") && other.tag != tag) {

            // Apply damage to the enemy
            other.GetComponent<CharacterStats>().TakeDamage(_physAtkdamage, _magicAtkdamage);
        }
    }
}
