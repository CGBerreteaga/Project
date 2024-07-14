using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] List<Transform> patrolPoints;
    [SerializeField] Transform currentPatrolPoint;
    [SerializeField] float patrolSpeed = 1.5f;
    [SerializeField] float thresholdDistance = 0.1f;
    [SerializeField] float delayTime = 3.0f;

    private bool isWaiting = false;

    void OnEnable()
    {
    }

    void Start()
    {
        // Initialize patrol behavior
        if (patrolPoints.Count > 0)
        {
            SetNewPatrolPoint();
        }
        else
        {
            Debug.LogError("No patrol points assigned.");
        }
    }

    void Update()
    {

        // Transition to ChaseState if player is detected
        if (los.detected)
        {
            animator.SetBool("isWalking", false);
            Transition(chaseState);
        }
        else
        {
            // If not waiting and player is not detected, continue patrolling
            if (!isWaiting)
            {
                Patrol();
            }
        }
    }

    void Patrol()
    {
        animator.SetBool("isWalking", true);
        agent.speed = patrolSpeed;
        // Check if reached patrol point
        if (agent.remainingDistance <= thresholdDistance)
        {
            agent.speed = 0;
            animator.SetBool("isWalking", false);
            // Start waiting before moving to next patrol point
            StartCoroutine(PatrolStop(delayTime));
        }
    }

    IEnumerator PatrolStop(float delayTime)
    {
        isWaiting = true;
        yield return new WaitForSeconds(delayTime);
        SetNewPatrolPoint();
        agent.speed = patrolSpeed;
        animator.SetBool("isWalking", true);
        isWaiting = false;
    }

    void SetNewPatrolPoint()
    {
        // Choose a new random patrol point
        currentPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Count)];
        agent.SetDestination(currentPatrolPoint.position);
        agent.speed = patrolSpeed;
    }

    void OnDisable()
    {
        // Reset patrol state when disabled
        animator.SetBool("isWalking", false);
    }
}
