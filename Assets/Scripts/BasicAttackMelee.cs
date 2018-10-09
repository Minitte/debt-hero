using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BasicAttackMelee : MonoBehaviour {

    /// <summary>
    /// Speed of the attack animation modified by the character's attack speed.
    /// </summary>
    public float attackSpeed;

    /// <summary>
    /// Reference to this gameobject's animator.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// The damage properties of the projectile.
    /// </summary>
    private float _physAtkdamage, _magicAtkdamage;

    // Use this for initialization
    private void Start() {
        _animator = GetComponent<Animator>();

        // Get damage information from parent gameobject
        CharacterStats characterInfo = transform.parent.gameObject.GetComponent<CharacterStats>();
        _physAtkdamage = characterInfo.physAtk;
        _magicAtkdamage = characterInfo.magicAtk;
    }

    /// <summary>
    /// Starts the attack animation.
    /// </summary>
    public void Attack() {
        // Play the animation
        _animator.SetFloat("Speed", attackSpeed);
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// For detecting collisions.
    /// </summary>
    /// <param name="other">The collision object collided with</param>
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "AI" || other.tag == "Player") {
            Debug.Log("Melee hit");

            // Apply damage to the enemy
            other.GetComponent<CharacterStats>().TakeDamage(_physAtkdamage, _magicAtkdamage);
        }
    }
}
