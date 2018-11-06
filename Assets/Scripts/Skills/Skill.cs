using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
        Ranged,
        Magic
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
    public GameObject damagePrefab;

    /// <summary>
    /// Multiplier for physical damage.
    /// </summary>
    public float physicalMultiplier = 1f;

    /// <summary>
    /// Multiplier for magic damage.
    /// </summary>
    public float magicMultiplier = 1f;

    /// <summary>
    /// Multiplier for the damage range.
    /// Used for melee attacks.
    /// </summary>
    public float rangeMultiplier = 1f;

    /// <summary>
    /// Multiplier for the damage area.
    /// Used for AoE skills.
    /// </summary>
    public float areaMultiplier = 1f;
    #endregion

    /// <summary>
    /// List of skill behaviours.
    /// </summary>
    public List<SkillBehaviour> skillBehaviours;

    /// <summary>
    /// Used for initialization.
    /// </summary>
    private void Awake() {
        skillBehaviours = new List<SkillBehaviour>();
        
        // Reload subassets
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
        foreach (Object o in assets) {
            if (o is SkillBehaviour) {
                skillBehaviours.Add(o as SkillBehaviour);
            }
        }
    }

    /// <summary>
    /// Casts the skill.
    /// </summary>
    /// <param name="caster">The transform of the caster</param>
    public void Cast(BaseCharacter caster) {
        // Activate the damage prefab if it exists
        if (damagePrefab != null) {
            GameObject damage = Instantiate(damagePrefab, caster.transform);
            switch (skillType) {
                case SkillType.Melee:
                    damage.GetComponent<Melee>().Activate(caster.transform, this);
                    if(soundFX != null) {
                        SoundManager.instance.PlaySound(GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<AudioSource>(), soundFX);
                    }
                    break;
            }
        }
        // Activate all the skill behaviours
        foreach (SkillBehaviour behaviour in skillBehaviours) {
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
            // Activate all the debuff behaviours
            foreach (DebuffBehaviour behaviour in skillBehaviours) {
                behaviour.OnDamageActivate(dealer, victim);
            }
        }
    }
}
