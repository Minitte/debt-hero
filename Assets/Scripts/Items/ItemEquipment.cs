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
    /// equip flag
    /// </summary>
    public bool equiped { get { return _equipped; }}

    /// <summary>
    /// equip flag
    /// </summary>
    private bool _equipped;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        OnUse += ToggleEquip;

        properties.description = FormattedDescription();
    }

    /// <summary>
    /// Equip the equipment if able to
    /// </summary>
    public void ToggleEquip() {
        CharacterEquipment ce = owner.characterEquipment;

        // counter act the default qty consumption
        properties.quantity++;

        if (ce == null) {
            return;
        }

        if (_equipped) {
            Unequip();
        } else {
            Equip();
        }
    }

    /// <summary>
    /// Equip to the owner
    /// </summary>
    public void Equip() {
        owner.characterEquipment.Equip(this);
    }

    /// <summary>
    /// Unequip to the owner
    /// </summary>
    public void Unequip() {
        owner.characterEquipment.Unequip(this);
    }

    /// <summary>
    /// Adds bonus stats to the owner
    /// </summary>
    public void AddBonusStats() {
        if (_equipped == true) {
            Debug.Log("possibly equiping something twice? (double stats)");
        }

        _equipped = true;

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
        if (_equipped == false) {
            Debug.Log("possibly unequiping something twice? (double negative stats)");
        }

        _equipped = false;

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