/// <summary>
/// An action for the AI that heals itself when below a certain amount of health.
/// </summary>
public class HealSelf : AIAction {

    /// <summary>
    /// The amount of hp lost before AI will attempt to heal.
    /// </summary>
    public float healThreshhold = 15f;

    /// <summary>
    /// Checks if the heal threshhold has been met
    /// </summary>
    /// <returns>True if heal threshhold met, otherwise false</returns>
    public override bool Precondition() {
        if (_characterStats.maxHp - _characterStats.currentHp >= healThreshhold) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Casts a heal spell.
    /// </summary>
    public override void Action() {
        _skillCaster.Cast(1, 1);
    }
}
