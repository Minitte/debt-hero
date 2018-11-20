using UnityEngine;

/// <summary>
/// This is a skill behaviour that deals damage in an arc infront of the player.
/// </summary>
public class AreaOfEffect : SkillHitbox {

    /// <summary>
    /// Starts the melee attack.
    /// </summary>
    public override void Activate(Transform caster, Skill skill) {
        // Setup melee properties
        _physAtkdamage = caster.GetComponent<BaseCharacter>().characterStats.physAtk * skill.physicalMultiplier;
        _magicAtkdamage = caster.GetComponent<BaseCharacter>().characterStats.magicAtk * skill.magicMultiplier;
        _skill = skill;

        // Setup area of effect
        transform.localScale *= skill.areaMultiplier;
        
        // Start the attack
        caster.GetComponent<BaseCharacter>().animator.SetTrigger("Attack"); // Play attack animation
        _ready = true;
    }
}
