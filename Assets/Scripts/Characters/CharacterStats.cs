using UnityEngine;

public class CharacterStats : MonoBehaviour {

    #region Delegate templates

    /// <summary>
    /// Death event template.
    /// </summary>
    public delegate void DeathEvent();

    /// <summary>
    /// Health changed event template.
    /// </summary>
    public delegate void HealthChangedEvent();

    /// <summary>
    /// Mana changed event template.
    /// </summary>
    public delegate void ManaChangedEvent();

    /// <summary>
    /// Level up event template.
    /// </summary>
    public delegate void LevelEvent();

    /// <summary>
    /// Exp changed event template.
    /// </summary>
    public delegate void ExpChangedEvent();

    #endregion

    #region Events

    /// <summary>
    /// This death event is called when the character dies.
    /// </summary>
    public event DeathEvent OnDeath;

    /// <summary>
    /// This event is called when the player takes damage or healing.
    /// </summary>
    public event HealthChangedEvent OnHealthChanged;

    /// <summary>
    /// This event is called when the player recovers or spends mana.
    /// </summary>
    public event ManaChangedEvent OnManaChanged;

    /// <summary>
    /// This event is called when the player levels up.
    /// </summary>
    public event LevelEvent OnLevel;

    /// <summary>
    /// This event is called when the player gains Exp.
    /// </summary>
    public event ExpChangedEvent OnExpChanged;

    #endregion

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
        EXP,
        MAXEXP
    }

    [Header("Resources")]
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

    [Header("Offense")]
    /// <summary>
    /// The character's physical attack stat.
    /// </summary>
    public float physAtk;

    /// <summary>
    /// The character's magic attack stat.
    /// </summary>
    public float magicAtk;

    [Header("Defence")]
    /// <summary>
    /// The character's physical defense stat. 
    /// </summary>
    public float physDef;

    /// <summary>
    /// The character's magic defense stat.
    /// </summary>
    public float magicDef;

    [Header("Other")]
    /// <summary>
    /// The character's level.
    /// </summary>
    public int level;

    /// <summary>
    /// The character's Experience point.
    /// </summary>
    public float exp;

    /// <summary>
    /// The character's Experience point.
    /// </summary>
    public float maxExp;

    /// <summary>
    /// Boolean to represent if a character is alive.
    /// </summary>
    public bool isAlive;

    /// <summary>
    /// Called when this character takes healing.
    /// </summary>
    /// <param name="manaAmount"></param>
    public void TakeHealing(float healingAmount) {
        float netHealingTaken = 0f;

        // Calculate healing taken
        if (currentHp + healingAmount > maxHp) {
            netHealingTaken = maxHp - currentHp;
            currentHp = maxHp;
        } else {
            currentHp += healingAmount;
            netHealingTaken = healingAmount;
        }

        // If healing was taken, trigger health changed event
        if (netHealingTaken > 0f) {
            if (OnHealthChanged != null) {
                OnHealthChanged();
            }
            ShowFloatingText(netHealingTaken, Color.green); // Show healing numbers
        }
    }

    /// <summary>
    /// Recovers mana up to the max
    /// </summary>
    /// <param name="amt">fixed amount to recover</param>
    public void RecoverMana(float amt) {
        float netManaRecovered = 0f;

        // Calculate mana recovery
        if (currentMp + amt > maxMp) {
            netManaRecovered = maxMp - currentMp;
            currentMp = maxMp;
        } else {
            currentMp += amt;
            netManaRecovered = amt;
        }

        // If mana was recovered, trigger mana changed event
        if (netManaRecovered > 0f) {
            if (OnManaChanged != null) {
                OnManaChanged();
            }
            ShowFloatingText(netManaRecovered, Color.blue); // Show mana recovery numbers
        }
    }

    /// <summary>
    /// Basic damage calculation function.
    /// </summary>
    /// <param name="physAtkDamage">The amount of physical damage to take.</param>
    /// <param name="magicAtkDamage">The amount of magical damage to take.</param>
    public void TakeDamage(float physAtkDamage, float magicAtkDamage) {
        float netDamageTaken = 0f;

        // Calculate damage taken
        if (physDef < physAtkDamage) {
            currentHp = currentHp - (physAtkDamage - physDef);
            netDamageTaken += physAtkDamage - physDef;
        }
        if (magicDef < magicAtkDamage) {
            currentHp = currentHp - (magicAtkDamage - magicDef);
            netDamageTaken += magicAtkDamage - magicDef;
        }

        // Check if character is dead
        if (currentHp > 0) {
            isAlive = true;
        } else {
            isAlive = false;
            OnDeath();
        }

        // If damage was taken, trigger health changed event
        if (netDamageTaken > 0f) {
            if (OnHealthChanged != null) {
                OnHealthChanged();
            }
        }
        ShowFloatingText(netDamageTaken, Color.red); // Show damage numbers
    }

    /// <summary>
    /// Called when this character spends mana.
    /// </summary>
    /// <param name="manaAmount">The amount of mana spent</param>
    public void SpendMana(float manaAmount) {
        if (currentMp >= manaAmount) {
            currentMp -= manaAmount;
            if (OnManaChanged != null) {
                OnManaChanged();
            }
        }
    }

    //Adds Exp to the player
    public void GainExp(float gainExp) {
        exp += gainExp;
        OnExpChanged();
    }

    //Levels up the player
    public void LevelUp() {
        level++;
        exp = 0;
        OnExpChanged();
        if (OnLevel != null) {
            OnLevel();
        }
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

            case StatType.MAXEXP:
                maxExp += amt;
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
    private void ShowFloatingText(float value, Color color) {
        if (CompareTag("Player")) {
            FloatingTextController.instance.CreateFloatingText(value.ToString(), color, PlayerManager.instance.localPlayer);
        } else {
            FloatingTextController.instance.CreateFloatingText(value.ToString(), color, gameObject);
        }
        
    }
}
