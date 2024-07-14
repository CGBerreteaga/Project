using UnityEngine;

public class DeadState : State
{
    void OnEnable()
    {
        animator.SetTrigger("Die");
        agent.speed = 0;
    }
}
