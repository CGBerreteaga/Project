using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject alertNotificationDisplay;
    public bool detected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alertNotificationDisplay.SetActive(detected);
    }

    void OnTriggerStay(Collider collider) {
        if(collider.gameObject.CompareTag("Player")) {
            if (!collider.GetComponent<ThirdPersonController>().isCrouching) {
            Vector3 playerDirection = collider.transform.position - transform.position;
            transform.parent.forward = playerDirection.normalized;
            detected = true;
            } 
        }
    }

    void OnTriggerExit(Collider collider) {
        if(collider.gameObject.CompareTag("Player")) {
            detected = false;
            ;
        }
    }
}
