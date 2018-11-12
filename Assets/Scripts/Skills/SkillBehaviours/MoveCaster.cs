using System.Collections;
using UnityEngine;

public class MoveCaster : SkillBehaviour {

    /// <summary>
    /// Movement duration.
    /// </summary>
    public float duration;

    /// <summary>
    /// How far to move.
    /// </summary>
    public float distance;

    public override void Activate(BaseCharacter caster) {
        SkillManager.instance.StartCoroutine(Move(caster));
    }

    /// <summary>
    /// Moves the caster.
    /// </summary>
    /// <param name="caster">The character to move</param>
    private IEnumerator Move(BaseCharacter caster) {
        yield return new WaitForEndOfFrame(); // Give enough time for character to face the correct direction
        caster.animator.SetBool("Dash", true);

        Vector3 direction = caster.transform.forward;

        // Make sure that the character is looking in the correct direction
        caster.transform.LookAt(caster.transform.position + direction);
        caster.agent.ResetPath();

        // For keeping track of duration
        float startTime = Time.time;
        float endTime = startTime + duration;
        float step = distance / duration;

        // Move the caster over the duration
        while (startTime < endTime && caster != null) {
            caster.agent.nextPosition = caster.transform.position + (direction * step * Time.deltaTime);
            startTime += Time.deltaTime;
            yield return null;
        }

        // Check for null in case character has dashed into the next level
        if (caster != null) {
            // Done moving
            caster.agent.ResetPath();
            caster.animator.SetBool("Dash", false);
        }
    }
}
