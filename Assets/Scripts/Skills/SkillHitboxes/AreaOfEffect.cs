using UnityEngine;

/// <summary>
/// This is a skill behaviour that deals damage in an area around the player.
/// </summary>
public class AreaOfEffect : SkillHitbox {

    private void Update() {
        if (_active) {
            _collider.enabled = true;
            Destroy(gameObject, 1f);
            if (_damageFX != null) {
                _damageFX.Emit(1); // Emit a damage effect particle
            }
            _active = false;
        }
    }

    /// <summary>
    /// Starts the melee attack.
    /// </summary>
    public override void Activate(Transform caster, Skill skill) {
        // Setup melee properties
        _physAtkdamage = caster.GetComponent<BaseCharacter>().characterStats.physAtk * skill.physicalMultiplier;
        _magicAtkdamage = caster.GetComponent<BaseCharacter>().characterStats.magicAtk * skill.magicMultiplier;
        _skill = skill;

        // Setup area of effect
        GetComponent<SphereCollider>().radius *= _skill.areaMultiplier;
        
        // Start the attack
        caster.GetComponent<BaseCharacter>().animator.SetTrigger("Hurt"); // Play attack animation TODO
        _active = true;
    }
}
