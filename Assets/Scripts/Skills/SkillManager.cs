using UnityEngine;

/// <summary>
/// This is a class for skill management.
/// </summary>
public class SkillManager : MonoBehaviour {

    /// <summary>
    /// Singleton instance of this class.
    /// </summary>
    public static SkillManager instance;

	// Use this for initialization
	void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Tried to instantiate skill manager instance twice.");
        }
	}
}
