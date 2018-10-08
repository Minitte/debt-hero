using System.Collections;
using UnityEngine;

/// <summary>
/// This is a class for a basic attack projectile.
/// </summary>
public class BasicAttackProjectile : MonoBehaviour {

    /// <summary>
    /// Speed of the projectile
    /// </summary>
    public float speed;

    /// <summary>
    /// Whether this object is alive or not.
    /// </summary>
    private bool _isAlive;

    /// <summary>
    /// The direction of the projectile.
    /// </summary>
    private Vector3 _direction;

    /// <summary>
    /// The damage properties of the projectile.
    /// </summary>
    private float _physAtkdamage, _magicAtkdamage;

    /// <summary>
    /// Instantiates the projectile, and starts its movement.
    /// </summary>
    /// <param name="direction">The direction to travel</param>
    /// <param name="speed">The speed of the projectile</param>
    public void Instantiate(Vector3 direction, float physAtkdamage, float magicAtkdamage, float speed = 5f) {
        _direction = direction;
        _physAtkdamage = physAtkdamage;
        _magicAtkdamage = magicAtkdamage;

        this.speed = speed;

        // Start the attack
        _isAlive = true;
    }

    // Update is called once per frame
    private void Update() {
        if (_isAlive) {
            StartCoroutine(LimitTime());

            // Keep moving in the direction
            transform.position += _direction.normalized * Time.deltaTime * speed;
        }
    }

    /// <summary>
    /// For detecting collisions.
    /// </summary>
    /// <param name="other">The collision object collided with</param>
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "AI") {
            Debug.Log("Ranged hit");
            
            // Apply damage to the enemy
            other.GetComponent<CharacterStats>().TakeDamage(_physAtkdamage, _magicAtkdamage);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// To make sure that the gameobject gets deleted since there are no walls yet.
    /// </summary>
    private IEnumerator LimitTime() {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
