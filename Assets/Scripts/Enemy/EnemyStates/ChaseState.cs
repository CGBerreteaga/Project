using UnityEngine;

public class ChaseState : State
{
    [SerializeField] float chaseSpeed = 3.0f;
    [SerializeField] GameObject player;
    [SerializeField] float attackDistanceMinimumThreshold = 1.0f;
    [SerializeField] float attackDistanceMaxThreshold = 10.0f;

    void OnEnable()
    {
        animator.SetBool("isRunning", true);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        agent.speed = chaseSpeed;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackDistanceMinimumThreshold)
        {
            animator.SetBool("isRunning", false);
            Transition(attackState);
        }
        if (distanceToPlayer >= attackDistanceMaxThreshold)
        {
            animator.SetBool("isRunning", false);
            Transition(patrolState);
        }
    }

    void OnDisable()
    {
        animator.SetBool("isRunning", false);
        player = null;
    }
}
