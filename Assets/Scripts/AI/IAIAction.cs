/// <summary>
/// This is an interface for AI Actions.
/// </summary>
public interface IAIAction {

    /// <summary>
    /// Checks for conditions that must be true before this script's action can be done.
    /// </summary>
    /// <returns>True if all conditions are met, false otherwise</returns>
    bool Precondition();

    /// <summary>
    /// The action to perform.
    /// </summary>
    void Action();
}
