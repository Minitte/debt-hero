using UnityEditor;
using UnityEngine;

/// <summary>
/// This is a skill behaviour that heals gameobjects in a radius around user.
/// </summary>
public class Heal : SkillBehaviour {

    /// <summary>
    /// Amount of healing done by the skill.
    /// </summary>
    public float healing;

    /// <summary>
    /// Radius of the healing skill.
    /// </summary>
    public float healingRadius;

    public override void Activate(Transform caster, Skill skill) {
        // Apply a heal within the radius of the spell
        Collider[] withinRadiusColliders = Physics.OverlapSphere(transform.position, healingRadius, (1 << transform.parent.gameObject.layer));
        foreach (Collider c in withinRadiusColliders) {
            c.GetComponent<BaseCharacter>().characterStats.TakeHealing(healing);
        }

        Destroy(gameObject); // Healing spell over
    }
    
}
