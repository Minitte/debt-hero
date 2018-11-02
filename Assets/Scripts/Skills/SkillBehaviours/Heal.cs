using UnityEngine;

/// <summary>
/// This is a skill behaviour that heals gameobjects in a radius around the user.
/// </summary>
public class Heal : SkillBehaviour {

    /// <summary>
    /// Amount of healing to be done.
    /// </summary>
    public float healing;

    /// <summary>
    /// Radius of the heal.
    /// </summary>
    public float healingRadius;

    public override void Activate(Transform caster, Skill skill) {
        // Apply a heal within the radius of the spell
        Collider[] withinRadiusColliders = Physics.OverlapSphere(caster.position, healingRadius, (1 << caster.gameObject.layer));
        foreach (Collider c in withinRadiusColliders) {
            c.GetComponent<BaseCharacter>().characterStats.TakeHealing(healing);
        }
    }
}
