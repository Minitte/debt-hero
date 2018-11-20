using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillHitbox : MonoBehaviour {

    /// <summary>
    /// The physical attack damage of the user.
    /// </summary>
    protected float _physAtkdamage;

    /// <summary>
    /// The magical attack damage of the user.
    /// </summary>
    protected float _magicAtkdamage;

    /// <summary>
    /// Reference to the animator status.
    /// </summary>
    protected AnimatorStatus _animatorStatus;

    /// <summary>
    /// Reference to this gameobject's Collider component.
    /// </summary>
    protected Collider _collider;

    /// <summary>
    /// Reference to the skill that called this attack.
    /// </summary>
    protected Skill _skill;

    /// <summary>
    /// Flag for if this behaviour is ready to begin.
    /// </summary>
    protected bool _ready;

    /// <summary>
    /// The damage effect to show during the damage window.
    /// </summary>
    protected ParticleSystem _damageFX;

    /// <summary>
    /// Property variable for damage effect.
    /// </summary>
    public ParticleSystem DamageFX {
        set { _damageFX = value; }
    }

    // Use this for initialization
    protected void Start() {
        _animatorStatus = transform.parent.GetComponent<BaseCharacter>().animatorStatus;
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    protected void Update() {
        if (_ready) {
            // Only deal damage within the damage window of the animation
            if (_animatorStatus.canDealDamage) {
                _collider.enabled = true;
                if (_damageFX != null) {
                    _damageFX.Emit(1); // Emit a damage effect particle
                }
            } else {
                // Check if the damage window is over
                if (_collider.enabled && !_animatorStatus.canDealDamage) {
                    Destroy(gameObject);
                }
            }
        }
    }

    /// <summary>
    /// For detecting collisions.
    /// </summary>
    /// <param name="other">The collision object collided with</param>
    private void OnTriggerEnter(Collider other) {
        // Only deal damage to Player or AI tags, and no friendly fire
        if ((other.CompareTag("AI") || other.CompareTag("Player")) && !other.CompareTag(transform.parent.tag)) {

            // Apply damage to the other character
            _skill.DealDamage(transform.parent.GetComponent<BaseCharacter>(),
                other.GetComponent<BaseCharacter>(), _physAtkdamage, _magicAtkdamage);
        }
    }

    /// <summary>
    /// Enables the skill hitbox.
    /// </summary>
    /// <param name="caster">The caster of the skill</param>
    /// <param name="skill">The skill used</param>
    public abstract void Activate(Transform caster, Skill skill);
}
