using UnityEngine;

public class EndCast : StateMachineBehaviour {

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Sets the casting flag to false
        animator.GetComponent<AnimatorStatus>().DoneCasting();
    }
}
