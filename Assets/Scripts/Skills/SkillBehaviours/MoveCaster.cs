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

    public override IEnumerator Activate(BaseCharacter caster, Skill skill) {
        caster.animatorStatus.isCasting = true;
        // Calculate destination
        Vector3 destination = caster.transform.position + GetDirection(caster) * distance;
        caster.transform.LookAt(destination);
        caster.agent.ResetPath();

        // Interpolation variables
        float startTime = Time.time;
        float endTime = startTime + seconds;
        float step = Time.deltaTime / seconds;

        // Move the caster over the duration
        while (startTime < endTime) {
            caster.agent.nextPosition = Vector3.Lerp(caster.transform.position, destination, step);
            startTime += Time.deltaTime;
            yield return null;
        }
        caster.agent.ResetPath(); // Done moving, stop the caster
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
