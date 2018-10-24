using UnityEngine;

/// <summary>
/// This is a skill behaviour that heals gameobjects in a radius around user.
/// </summary>
public class Heal : SkillBehaviour {

    public override void Activate(Transform caster, Skill skill) {
        // Apply a heal within the radius of the spell
        Collider[] withinRadiusColliders = Physics.OverlapSphere(transform.position, skill.areaRadius, (1 << transform.parent.gameObject.layer));
        foreach (Collider c in withinRadiusColliders) {
            c.GetComponent<BaseCharacter>().characterStats.TakeHealing(skill.healing);
        }

        Destroy(gameObject); // Healing spell over
    }
    
}
