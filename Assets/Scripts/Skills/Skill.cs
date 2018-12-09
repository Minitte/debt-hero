using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a scriptable class for skills.
/// </summary>
[CreateAssetMenu(menuName = "New Skill")]
public class Skill : ScriptableObject {
    
    /// <summary>
    /// Enum for the different types of skills.
    /// </summary>
    public enum SkillType {
        Melee,
        Projectile,
        AreaOfEffect
    }

    #region General
    /// <summary>
    /// The name of the skill.
    /// </summary>
    [Header("General")]
    public string skillName;

    /// <summary>
    /// The icon of the skill.
    /// </summary>
    public Sprite skillIcon;

    /// <summary>
    /// The description of the skill.
    /// </summary>
    public string skillDescription;

    /// <summary>
    /// The type of skill.
    /// </summary>
    public SkillType skillType;

    public AudioClip soundFX;
    #endregion

    #region Costs
    /// <summary>
    /// The base cooldown of the skill.
    /// </summary>
    [Header("Costs")]
    public float cooldown;

    /// <summary>
    /// How much mana the skill costs.
    /// </summary>
    public float manaCost;
    #endregion

    #region Damage Properties
    [Header("Damage")]
    /// <summary>
    /// Prefab of the damage hitbox.
    /// </summary>
    public GameObject damageHitbox;

    /// <summary>
    /// Prefab of the damage effect.
    /// </summary>
    public GameObject damageFX;

    /// <summary>
    /// Multiplier for physical damage.
    /// </summary>
    public float physicalMultiplier = 1f;

    /// <summary>
    /// Multiplier for magic damage.
    /// </summary>
    public float magicMultiplier = 1f;

    /// <summary>
    /// Multiplier for the hitbox range.
    /// </summary>
    public Vector3 hitboxScale = new Vector3(1f, 1f, 1f);

    /// <summary>
    /// Velocity of the projectile.
    /// </summary>
    public float projectileVelocity = 1f;

    /// <summary>
    /// Whether the skill is delayed or not.
    /// </summary>
    public bool delayed;
    #endregion

    /// <summary>
    /// List of skill behaviours.
    /// </summary>
    public List<InstantBehaviour> instantBehaviours;

    /// <summary>
    /// List of on damage behaviours.
    /// </summary>
    public List<DamageBehaviour> damageBehaviours;

    /// <summary>
    /// Used for initialization.
    /// </summary>
    private void Awake() {
        instantBehaviours = new List<InstantBehaviour>();
        damageBehaviours = new List<DamageBehaviour>();
        /*
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
        foreach (Object o in assets) {
            if (o is SkillBehaviour) {
                skillBehaviours.Add(o as SkillBehaviour);
            }
        }
        */

        // Reload subassets
        if (name != "") {
            Object[] assets = Resources.LoadAll("Skills/" + name);
            foreach (Object o in assets) {
                if (o is InstantBehaviour) {
                    instantBehaviours.Add(o as InstantBehaviour);
                } else if (o is DamageBehaviour) {
                    damageBehaviours.Add(o as DamageBehaviour);
                }
            }
        }
    }

    /// <summary>
    /// Casts the skill.
    /// </summary>
    /// <param name="caster">The transform of the caster</param>
    public void Cast(BaseCharacter caster) {
        // Face mouse position if applicable
        if (caster is PlayerCharacter && !PlayerCharacter.OnPS4) {
            ((PlayerCharacter)caster).FaceMousePosition(); 
        }

        // Activate the damage prefab if it exists
        if (damageHitbox != null) {
            // Create the hitbox gameobjects
            GameObject skillObject = new GameObject(skillName);
            skillObject.transform.position = caster.transform.position;
            skillObject.transform.rotation = caster.transform.rotation;
            GameObject damage = Instantiate(damageHitbox, skillObject.transform);
            SkillHitbox hitbox = damage.GetComponent<SkillHitbox>();

            // Register the skill gameobject in the caster's active skills list
            caster.GetComponent<SkillCaster>().ActiveSkillObjects.Add(skillObject);

            // Set the damage effect if it exists
            if (damageFX != null) {
                ParticleSystem damagePS = Instantiate(damageFX, damage.transform).GetComponent<ParticleSystem>();
                ParticleSystem.MainModule module = damagePS.main;
                hitbox.DamageFX = damagePS;
                
                // Modify damage effect size accordingly to hitbox multiplier
                float maxMultiplierValue = Mathf.Max(Mathf.Max(hitboxScale.x, hitboxScale.y), hitboxScale.z);
                module.startSize = module.startSize.constant * maxMultiplierValue;
            }

            // Activate the damage hitbox
            hitbox.Activate(caster, this);

            // Play sound effect
            if(soundFX != null) {
                SoundManager.instance.PlaySound(GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<AudioSource>(), soundFX);
            }
        }
        // Activate all the skill behaviours
        foreach (InstantBehaviour behaviour in instantBehaviours) {
            behaviour.Activate(caster);
        }
    }

    /// <summary>
    /// Deals damage to a character.
    /// </summary>
    /// <param name="dealer">The character dealing the damage</param>
    /// <param name="victim">The character to deal damage to</param>
    /// <param name="physDamage">The amount of physical damage to deal</param>
    /// <param name="magicDamage">The amount of magical damage to deal</param>
    public void DealDamage(BaseCharacter dealer, BaseCharacter victim, float physDamage, float magicDamage) {
        victim.characterStats.TakeDamage(physDamage, magicDamage);

        if (victim.characterStats.isAlive) {
            // Activate all the damage behaviours
            foreach (DamageBehaviour behaviour in damageBehaviours) {
                behaviour.OnDamageActivate(dealer, victim);
            }
        }
    }
}
