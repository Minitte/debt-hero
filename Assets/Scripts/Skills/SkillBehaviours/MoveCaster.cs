using System.Collections;
using UnityEngine;

public class MoveCaster : SkillBehaviour {

    /// <summary>
    /// Movement duration.
    /// </summary>
    public float seconds;

    /// <summary>
    /// How far to move.
    /// </summary>
    public float distance;

    /// <summary>
    /// Moves the caster.
    /// </summary>
    /// <param name="caster">The character to move</param>
    /// <param name="skill">Reference to the skill used</param>
    public override IEnumerator Activate(BaseCharacter caster, Skill skill) {
        caster.animator.SetBool("Dash", true);
        // Calculate direction
        Vector3 direction = GetDirection(caster);

        // Make sure that the character is looking in the correct direction
        caster.transform.LookAt(caster.transform.position + direction);
        caster.agent.ResetPath();
        
        // For keeping track of duration
        float startTime = Time.time;
        float endTime = startTime + seconds;
        float step = distance / seconds;

        // Move the caster over the duration
        while (startTime < endTime) {
            caster.agent.nextPosition = caster.transform.position + (direction * step  * Time.deltaTime);
            startTime += Time.deltaTime;
            yield return null;
        }

        // Done moving
        caster.agent.ResetPath();
        caster.animator.SetBool("Dash", false);
    }

    /// <summary>
    /// Gets the direction of movement.
    /// For mouse players, the direction will be towards the mouse cursor.
    /// Otherwise, the direction will be the forward vector of the caster.
    /// </summary>
    /// <param name="caster">The character that casted this skill</param>
    /// <returns>The direction to move</returns>
    private Vector3 GetDirection(BaseCharacter caster) {
        // Check if it's a mouse player
        if (Input.GetJoystickNames().Length == 0) {
            Vector3 clickedPoint = new Vector3();
            if (PlayerCharacter.GetClickedPoint(out clickedPoint)) {
                return (clickedPoint - caster.transform.position).normalized;
            }
        }
        return caster.transform.forward;
    }
}
