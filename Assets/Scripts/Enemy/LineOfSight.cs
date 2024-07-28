using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [Header("Detection Properties")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float fieldOfViewAngle = 110f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] public bool detected = false;
    public bool first = true;
    public GameObject alertedUI;
    private Transform playerTransform;

    void Start() 
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (GetComponent<Enemy>().current_hp > 0) {
            Detected();
        }
    }

    public void Detected()
    {
        // Calculate direction to player and distance
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // Check if player is within the field of view and range
        if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfViewAngle / 2 && distanceToPlayer <= detectionRange)
        {
            RaycastHit hit;

            // Perform raycast to check for line of sight
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (first) 
                    {
                        EventManager.TriggerDetectionSound(gameObject);
                        alertedUI.SetActive(true);
                        detected = true;
                        first = false;
                    }
                }
            }
        }
        else
        {
            alertedUI.SetActive(false);
            detected = false;
            first = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the field of view
        Vector3 fovLine1 = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * detectionRange;
        Vector3 fovLine2 = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * detectionRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);

        // Draw the rays for the field of view
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * detectionRange);
    }
}
