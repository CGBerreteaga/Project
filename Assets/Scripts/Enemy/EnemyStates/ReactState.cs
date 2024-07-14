using UnityEngine;

public class ReactState : State
{
    void OnEnable()
    {   
        agent.speed = 0;
        animator.SetTrigger("React");
        
    }

    public void AnimationComplete() {
        animator.ResetTrigger("React");
        Transition(chaseState);
    }
}


