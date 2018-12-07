using System.Collections;
using UnityEngine;

/// <summary>
/// This is a skill behaviour for buffs.
/// </summary>
public class BuffStat : InstantBehaviour {

    /// <summary>
    /// Which stat to buff.
    /// </summary>
    public CharacterStats.StatType statType;

    /// <summary>
    /// The amount to buff the stat by.
    /// </summary>
    public float amount;

    /// <summary>
    /// Duration of the buff.
    /// </summary>
    public float duration;

    /// <summary>
    /// Particle effects to play during the buff.
    /// </summary>
    public GameObject buffFX;

    /// <summary>
    /// Activates the buff effect.
    /// </summary>
    /// <param name="caster">The caster of this buff behaviour</param>
    public override void Activate(BaseCharacter caster) {
        SkillManager.instance.StartCoroutine(ApplyBuff(caster));
    }

    /// <summary>
    /// Applies the buff to the caster.
    /// </summary>
    /// <param name="caster">The character to apply the buff to</param>
    public IEnumerator ApplyBuff(BaseCharacter caster) {
        caster.characterStats.AddToStat(statType, amount); // Apply the buff

        // Play buff effect if it exists
        if (buffFX != null && buffFX.GetComponent<ParticleSystem>()) {
            ParticleSystem ps = Instantiate(buffFX, caster.transform).GetComponent<ParticleSystem>();
            ps.Play();
            Destroy(ps.gameObject, duration);
        }

        yield return new WaitForSeconds(duration); // Wait until buff duration is over
        caster.characterStats.AddToStat(statType, -amount); // Remove the buff
    }
}
