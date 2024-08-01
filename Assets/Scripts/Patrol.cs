using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Animator animator;
    [SerializeField] Detection detectionMechanism;
    NavMeshAgent agent;
    int current = 0;
    bool isPatrolling = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(Patrolling());
    }

    void Update()
    {
        if (detectionMechanism.detected)
        {
            isPatrolling = false;
            animator.SetBool("Walking", false);
            agent.ResetPath();  // Stops the agent immediately
        }
        else
        {
            isPatrolling = true;
            animator.SetBool("Walking", true);
        }
    }

    private IEnumerator Patrolling()
    {
        while (true)
        {
            if (isPatrolling && patrolPoints.Length > 0)
            {
                agent.SetDestination(patrolPoints[current].position);
                current = (current + 1) % patrolPoints.Length;

                yield return new WaitUntil(() => agent.remainingDistance < 0.1f);
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;  // Wait for the next frame and check again
            }
        }
    }
}
