using UnityEngine;

public class ItemPotion : ItemBase {
    
    /// <summary>
    /// Amount of Health recovered on use
    /// </summary>
    public float HealthRecovery;

    /// <summary>
    /// Amount of mana recovered on use
    /// </summary>
    public float ManaRecovery;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        OnUse += ApplyRecovery;
    }

    /// <summary>
    /// Applies the hp/mp recovery effects to the owner 
    /// </summary>
    private void ApplyRecovery() {
        owner.characterStats.TakeHealing(HealthRecovery);

        owner.characterStats.RecoverMana(ManaRecovery);
    }
}