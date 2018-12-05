using UnityEngine;

/// <summary>
/// This is a skill behaviour that deals damage in an arc infront of the player.
/// </summary>
public class Projectile : SkillHitbox {

    // Update is called once per frame
    private void Update() {
        if (_damageFX != null) {
            _damageFX.Emit(1); // Emit a damage effect particle
        }
    }

    /// <summary>
    /// Starts the melee attack.
    /// </summary>
    public override void Activate(BaseCharacter caster, Skill skill) {
        // Setup melee properties
        _physAtkdamage = caster.GetComponent<BaseCharacter>().characterStats.physAtk * skill.physicalMultiplier;
        _magicAtkdamage = caster.GetComponent<BaseCharacter>().characterStats.magicAtk * skill.magicMultiplier;
        _caster = caster;
        _skill = skill;

        // Setup projectile size
        transform.localScale = Vector3.Scale(transform.localScale, skill.hitboxScale);
        transform.localPosition = new Vector3(0f, transform.localScale.y, transform.localScale.z * 1.5f);
        
        // Start the projectile attack
        caster.GetComponent<BaseCharacter>().animator.SetTrigger("Attack"); // Play attack animation
        GetComponent<Collider>().enabled = true;
    }
}
