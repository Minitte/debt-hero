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
    /// Reference to the caster of this attack.
    /// </summary>
    protected BaseCharacter _caster;

    /// <summary>
    /// Flag for if this hitbox is active.
    /// </summary>
    protected bool _active;

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
        _collider = GetComponent<Collider>();
    }

    /// <summary>
    /// For detecting collisions.
    /// </summary>
    /// <param name="other">The collision object collided with</param>
    private void OnTriggerEnter(Collider other) {
        // Only deal damage to Player or AI tags, and no friendly fire
        if ((other.CompareTag("AI") || other.CompareTag("Player")) && !other.CompareTag(_caster.tag)) {

            // Apply damage to the other character
            _skill.DealDamage(_caster, other.GetComponent<BaseCharacter>(), _physAtkdamage, _magicAtkdamage);
        }
    }

    /// <summary>
    /// Enables the skill hitbox.
    /// </summary>
    /// <param name="caster">The caster of the skill</param>
    /// <param name="skill">The skill used</param>
    public abstract void Activate(BaseCharacter caster, Skill skill);
}
