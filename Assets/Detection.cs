using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Detection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider) {
        if(collider.gameObject.CompareTag("Player")) {
            if (!collider.GetComponent<ThirdPersonController>().isCrouching) {
            Vector3 playerDirection = collider.transform.position - transform.position;
            transform.parent.forward = playerDirection.normalized;
            }
        }
    }
}
