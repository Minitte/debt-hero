using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Knockback : DebuffBehaviour {

    /// <summary>
    /// The magnitude of the knockback.
    /// </summary>
    public float magnitude;

    public override void Activate(BaseCharacter caster) {
        // Do nothing
    }

    /// <summary>
    /// Knocks the victim back.
    /// </summary>
    /// <param name="dealer">The character that dealt damage</param>
    /// <param name="victim">The character that took damage</param>
    public override void OnDamageActivate(BaseCharacter dealer, BaseCharacter victim) {
        Vector3 direction = (victim.transform.position - dealer.transform.position).normalized;
        SkillManager.instance.StartCoroutine(ApplyKnockback(victim, direction));
    }

    /// <summary>
    /// Applies the knockback to the victim.
    /// </summary>
    /// <param name="victim">The character being knocked back</param>
    /// <param name="direction">The direction of the knockback</param>
    public IEnumerator ApplyKnockback(BaseCharacter victim, Vector3 direction) {
        // Stop player movement
        victim.agent.ResetPath();
        //victim.animator.SetBool("Stop", true);

        // Move the victim backwards
        Vector3 step = direction * magnitude * Time.deltaTime;
        for (int i = 0; i < 60; i++) {
            victim.transform.position += step;
            yield return null;
        }

        //victim.animator.SetBool("Stop", false); // Resume player movement
    }
}
