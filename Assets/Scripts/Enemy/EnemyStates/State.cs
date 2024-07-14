using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected LineOfSight los;
    [SerializeField] protected IdleState idleState;
    [SerializeField] protected PatrolState patrolState;
    [SerializeField] protected ChaseState chaseState;
    [SerializeField] protected AttackState attackState;
    [SerializeField] protected ReactState reactState;
    [SerializeField] protected DeadState deadState;

    public event Action<State> onEnter;

    void OnValidate()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        los = GetComponent<LineOfSight>();
        idleState = GetComponent<IdleState>();
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();
        reactState = GetComponent<ReactState>();
        deadState = GetComponent<DeadState>();
    }

    public void Transition(State state)
    {
        if (enabled)
        {
            onEnter?.Invoke(state);
        }
    }
}
