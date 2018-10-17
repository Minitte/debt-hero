using UnityEngine;

public class CharacterStats : MonoBehaviour {

    /// <summary>
    /// Death event template.
    /// </summary>
    public delegate void DeathEvent();

    /// <summary>
    /// Health changed event template.
    /// </summary>
    public delegate void HealthChangedEvent();

    /// <summary>
    /// This death event is called when the character dies.
    /// </summary>
    public event DeathEvent OnDeath;

    /// <summary>
    /// This event is called when the player takes damage or healing.
    /// </summary>
    public event HealthChangedEvent OnHealthChanged;

    /// <summary>
    /// Reference to the health bar prefab.
    /// </summary>
    public GameObject healthBar;

    /// <summary>
    /// Enum for all stat types.
    /// </summary>
    public enum StatType {
        NONE,
        CURRENT_HP,
        MAX_HP,
        CURRENT_MP,
        MAX_MP,
        PHY_ATK,
        MAG_ATK,
        PHY_DEF,
        MAG_DEF,
        EXP
    }

    /// <summary>
    /// The character's current health point.
    /// </summary>
    public float currentHp;

    /// <summary>
    /// The character's maximum health point. 
    /// </summary>
    public float maxHp;

    /// <summary>
    /// The character's current mana point.
    /// </summary>
    public float currentMp;

    /// <summary>
    /// The character's maximum mana point.
    /// </summary>
    public float maxMp;

    /// <summary>
    /// The character's physical attack stat.
    /// </summary>
    public float physAtk;

    /// <summary>
    /// The character's magic attack stat.
    /// </summary>
    public float magicAtk;

    /// <summary>
    /// The character's physical defense stat. 
    /// </summary>
    public float physDef;

    /// <summary>
    /// The character's magic defense stat.
    /// </summary>
    public float magicDef;

    /// <summary>
    /// The character's level.
    /// </summary>
    public int level;

    /// <summary>
    /// The character's experience point.
    /// </summary>
    public float exp;

    /// <summary>
    /// Boolean to represent if a character is alive.
    /// </summary>
    public bool isAlive;

    // Use this for initialization
    private void Start() {
        OnDeath += Die;
    }

    /// <summary>
    /// Called when this character takes healing.
    /// </summary>
    /// <param name="healingAmount"></param>
    public void TakeHealing(float healingAmount) {
        float netHealingTaken = 0f;
        if (currentHp + healingAmount > maxHp) {
            netHealingTaken = maxHp - currentHp;
            currentHp = maxHp;
        } else {
            currentHp += healingAmount;
            netHealingTaken = healingAmount;
        }

        ShowFloatingText(netHealingTaken, false); // Show healing numbers
    }

    /// <summary>
    /// Basic damage calculation function.
    /// </summary>
    /// <param name="physAtkDamage">The amount of physical damage to take.</param>
    /// <param name="magicAtkDamage">The amount of magical damage to take.</param>
    public void TakeDamage(float physAtkDamage, float magicAtkDamage) {
        float netDamageTaken = 0f;
        if (physDef < physAtkDamage) {
            currentHp = currentHp - (physAtkDamage - physDef);
            netDamageTaken += physAtkDamage - physDef;
        }

        if (magicDef < magicAtkDamage) {
            currentHp = currentHp - (magicAtkDamage - magicDef);
            netDamageTaken += magicAtk - magicDef;
        }

        if (currentHp > 0) {
            isAlive = true;
        } else {
            isAlive = false;
            OnDeath();
        }

        // if damage was taken, trigger OnDamageTaken event
        if (netDamageTaken > 0f) {
            if (OnDamageTaken != null) {
                OnDamageTaken(physAtkDamage, magicAtkDamage);
            }
        }
        ShowFloatingText(netDamageTaken, true); // Show damage numbers
    }

    /// <summary>
    /// Looks up a stat and adds to it
    /// </summary>
    /// <param name="targetStat">Stat to look up</param>
    /// <param name="amt"></param>
    public void AddToStat(StatType targetStat, float amt) {
        switch (targetStat) {
            case StatType.CURRENT_HP:
                currentHp += amt;
                break;

            case StatType.MAX_HP:
                maxHp += amt;
                break;
                
            case StatType.CURRENT_MP:
                currentMp += amt;
                break;
                
            case StatType.MAX_MP:
                maxMp += amt;
                break;
                
            case StatType.PHY_ATK:
                physAtk += amt;
                break;
                
            case StatType.MAG_ATK:
                magicAtk += amt;
                break;
                
            case StatType.PHY_DEF:
                physDef += amt;
                break;
                
            case StatType.MAG_DEF:
                magicDef += amt;
                break;
                
            case StatType.EXP:
                exp += amt;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Displays floating combat text next to this gameobject.
    /// </summary>
    /// <param name="value">The amount of damage or healing taken</param>
    /// <param name="isDamage">Whether it's a damage or healing value</param>
    private void ShowFloatingText(float value, bool isDamage) {
        if (tag == "AI") {
            FloatingTextController.instance.CreateCombatNumber(value, isDamage, gameObject);
        }  else {
            FloatingTextController.instance.CreateCombatNumber(value, isDamage, PlayerManager.instance.localPlayer);
        }

    }

    /// <summary>
    /// Called when the gameobject dies.
    /// </summary>
    public void Die() {
        Destroy(gameObject); // Get rid of the gameobject
    }

}
