using UnityEngine;

/// <summary>
/// This is a skill behaviour that deals damage in an area around the player.
/// </summary>
public class AreaOfEffect : SkillHitbox {

    private void Update() {
        if (_active) {
            _collider.enabled = true;
            Destroy(transform.parent.gameObject, 1f);
            if (_damageFX != null) {
                _damageFX.Emit(1); // Emit a damage effect particle
            }
            _active = false;
        }
    }

    /// <summary>
    /// Starts the melee attack.
    /// </summary>
    public override void Activate(BaseCharacter caster, Skill skill) {
        // Setup melee properties
        _physAtkdamage = caster.characterStats.physAtk * skill.physicalMultiplier;
        _magicAtkdamage = caster.characterStats.magicAtk * skill.magicMultiplier;
        _animatorStatus = caster.animatorStatus;
        _caster = caster;
        _skill = skill;

        // Setup area of effect
        float maxMultiplierValue = Mathf.Max(Mathf.Max(_skill.hitboxScale.x, _skill.hitboxScale.y), _skill.hitboxScale.z);
        GetComponent<SphereCollider>().radius *= maxMultiplierValue;

        // Start the attack
        caster.animator.SetTrigger("TakeDamage"); // Play attack animation
        _active = true;
    }
}
