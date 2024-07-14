using UnityEngine;

public class IdleState : State
{
    void OnEnable()
    {
        Transition(patrolState);
    }
}
