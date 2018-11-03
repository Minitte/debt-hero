using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a skill behaviour that deals damage over time.
/// </summary>
public class DamageOverTime : DebuffBehaviour {

    /// <summary>
    /// The total amount of physical damage to deal.
    /// </summary>
    public float physDamage;

    /// <summary>
    /// The total amount of magical damage to deal.
    /// </summary>
    public float magicDamage;

    public override void Activate(BaseCharacter caster) {
        // Do nothing
    }

    /// <summary>
    /// Called when a character takes damage from this skill.
    /// </summary>
    /// <param name="victim"></param>
    public override void OnDamageActivate(BaseCharacter victim) {
        SkillManager.instance.StartCoroutine(ApplyDoT(victim));
    }

    /// <summary>
    /// Applies a damage over time effect.
    /// </summary>
    /// <param name="victim">The character to apply the effect on</param>
    private IEnumerator ApplyDoT(BaseCharacter victim) {
        yield return _oneSecond;
        for (int i = 0; i < duration; i++) {
            if (victim.characterStats.isAlive) {
                victim.characterStats.TakeDamage(physDamage / duration, magicDamage / duration);
            } else {
                break;
            }
            yield return _oneSecond;
        }
    }
}
