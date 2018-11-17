using UnityEngine;

public class ItemEquipment : ItemBase {

    /// <summary>
    /// bonus max health
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// bonus max mana
    /// </summary>
    public int maxMana;

    /// <summary>
    /// bonus physical attack
    /// </summary>
    public int phyAttack;

    /// <summary>
    /// Bonus magic attack
    /// </summary>
    public int magAttack;

    /// <summary>
    /// Bonus physical defence
    /// </summary>
    public int phyDefence;

    /// <summary>
    /// Bonus mag defense
    /// </summary>
    public int magDefence;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        OnUse += Equip;

        properties.description = FormattedDescription();
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy() {
        CharacterEquipment ce = owner.characterEquipment;

        if (ce == null) {
            return;
        }

        ce.Unequip(this);
    }

    /// <summary>
    /// Equip the equipment if able to
    /// </summary>
    public void Equip() {
        CharacterEquipment ce = owner.characterEquipment;

        // counter act the default qty consumption
        properties.quantity++;

        if (ce == null) {
            return;
        }

        ce.Equip(this);
    }

    /// <summary>
    /// Adds bonus stats to the owner
    /// </summary>
    public void AddBonusStats() {
        owner.characterStats.maxHp += maxHealth;
        owner.characterStats.maxMp += maxMana;

        owner.characterStats.physAtk += phyAttack;
        owner.characterStats.magicAtk += magAttack;

        owner.characterStats.physDef += phyDefence;
        owner.characterStats.magicDef += magDefence;
    }

    /// <summary>
    /// Removes bonus stats from the owners
    /// </summary>
    public void RemoveBonusStats() {
        owner.characterStats.maxHp -= maxHealth;
        owner.characterStats.maxMp -= maxMana;
        
        owner.characterStats.physAtk -= phyAttack;
        owner.characterStats.magicAtk -= magAttack;

        owner.characterStats.physDef -= phyDefence;
        owner.characterStats.magicDef -= magDefence;
    }
    

    /// <summary>
    /// Get a string containing the description and stats
    /// </summary>
    /// <returns></returns>
    public string FormattedDescription() {
        return string.Format(
            properties.description + "\n" +
            "HP: {0,-8} P.Att: {2,-8} P.Def: {4,-8}\n" +
            "MP: {1,-8} M.Att: {3,-8} M.Def: {5,-8}",
            maxHealth.ToString("+#;-#"), maxMana.ToString("+#;-#"),
            phyAttack.ToString("+#;-#"), magAttack.ToString("+#;-#"),
            phyDefence.ToString("+#;-#"), magDefence.ToString("+#;-#")
            );
	}
}