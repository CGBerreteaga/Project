
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    private GameObject target;

    [SerializeField] GameObject bodyGameObject;

    void Start() {
    }
    void OnTriggerStay(Collider collider)
    {
        
        if(collider.gameObject.CompareTag("FlyingObject")) {
        target = collider.gameObject;

            if (target != null)
            {
                Vector3 lookDirection = target.transform.position - transform.position;
                transform.forward = lookDirection.normalized;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
            transform.forward = bodyGameObject.transform.forward;
            target = null; // Reset target when it exits trigger
    }

    
}
