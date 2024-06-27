using UnityEngine;

public class TestPatrol : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 5f;

    private Vector3 currentTarget;

    void Start()
    {
        currentTarget = endPoint.position; // Start moving towards endPoint
    }

    void Update()
    {
        Vector3 targetDirection = currentTarget - transform.position;

        // Check if we are close enough to the current target
        if (targetDirection.magnitude <= 0.1f)
        {
            // Switch to the other target
            if (currentTarget == startPoint.position)
                currentTarget = endPoint.position;
            else
                currentTarget = startPoint.position;
        }
        else
        {
            // Rotate to face the current target
            transform.forward = targetDirection.normalized;
            // Move towards the current target
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
