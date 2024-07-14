using System.Runtime.ExceptionServices;
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
    public GameObject player;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Detected();
    }

    public void Detected()
    {

            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            // Check if player is within field of view and range
            if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfViewAngle / 2 && distanceToPlayer <= detectionRange)
            {
                if(first) {
                    EventManager.TriggerDetectionSound(gameObject);
                    alertedUI.SetActive(true);
                    detected = true;
                    first = false;
                }
            } else {
                alertedUI.SetActive(false);
                detected = false;
                first = true;
            }
                
      
    }


    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Vector3 fovLine1 = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * detectionRange;
        Vector3 fovLine2 = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * detectionRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }
}
