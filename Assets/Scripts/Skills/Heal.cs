using UnityEngine;

/// <summary>
/// This is a skill behaviour that heals gameobjects in a radius around user.
/// </summary>
public class Heal : SkillBehaviour {

    /// <summary>
    /// The radius of the heal.
    /// </summary>
    public float healRadius;

    /// <summary>
    /// The amount to heal for.
    /// </summary>
    public float healingAmount;

    public override void Activate(Transform caster, Skill skill) {
        Instantiate(gameObject, caster);
    }

    
    private void Start() {
        // Apply a heal within the radius of the spell
        Collider[] withinRadiusColliders = Physics.OverlapSphere(transform.position, healRadius, (1 << transform.parent.gameObject.layer));
        foreach (Collider c in withinRadiusColliders) {
            c.GetComponent<BaseCharacter>().characterStats.TakeHealing(healingAmount);
        }
    }
    
}
