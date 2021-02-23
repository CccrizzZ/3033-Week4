using UnityEngine;

public class ReloadingAnimation : StateMachineBehaviour
{
    private static readonly int IsReloading = Animator.StringToHash("IsReloading");
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IsReloading, false);
    }
    
}
