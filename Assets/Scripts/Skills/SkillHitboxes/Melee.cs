using UnityEngine;

/// <summary>
/// This is a skill behaviour that deals damage in an arc infront of the player.
/// </summary>
public class Melee : SkillHitbox {

    // Update is called once per frame
    private void Update() {
        // Only deal damage within the damage window of the animation
        if (_animatorStatus.canDealDamage) {
            if (_active) {
                _collider.enabled = true;
                if (_damageFX != null) {
                    _damageFX.Emit(1); // Emit a damage effect particle
                }
            }
            _active = false;
        } else {
            // Check if the damage window is over
            if (_collider.enabled) {
                Destroy(transform.parent.gameObject);
            }
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

        // Setup melee range
        transform.localScale = Vector3.Scale(transform.localScale, skill.hitboxScale);
        transform.localPosition = new Vector3(0f, transform.localScale.y, transform.localScale.z * 1.5f);
        
        // Start the melee attack
        //caster.animator.SetTrigger("Attack"); // Play attack animation
        _active = true;
    }
}
