
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
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        OnUse += Equip;
    }

    /// <summary>
    /// Equip the equipment if able to
    /// </summary>
    public void Equip() {
        CharacterEquipment ce = GetComponent<CharacterEquipment>();

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
}