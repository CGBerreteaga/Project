using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimConstraint : MonoBehaviour
{
    public GameObject originPoint;
    public GameObject target;
    public List<GameObject> gameObjects = new List<GameObject>(); // Initialize the list

    public int targetIndexNumber = 0;

    void Start()
    {
        // Subscribe to the event
        EventManager.OnChangeTarget += ChangeTarget;

        // Initialize originPoint (assuming target is already assigned in the Inspector)

        // Add initial target to gameObjects list
        
        gameObjects.Add(originPoint);
        EventManager.TriggerTargetLock(gameObjects[targetIndexNumber]);
        
    }

    void Update()
    {
        
        // Check if gameObjects list is not null and not empty
        if (gameObjects != null && gameObjects.Count > 0)
        {
            // Ensure targetIndexNumber is within bounds
            if (targetIndexNumber < gameObjects.Count)
            {
                target.transform.position = gameObjects[targetIndexNumber].transform.position;
            }
            else
            {
                Debug.LogWarning("targetIndexNumber out of range.");
            }
        }
        else
        {
            target.transform.localPosition = originPoint.transform.localPosition;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            gameObjects.Add(collider.gameObject);
            
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            target.transform.position = collider.gameObject.transform.position;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (gameObjects.Contains(collider.gameObject))
            {
                gameObjects.Remove(collider.gameObject);
            }

            target.transform.localPosition = originPoint.transform.position;
        }
    }

    public void ChangeTarget()
    {
        if (targetIndexNumber <= gameObjects.Count-1)
        {
            
            targetIndexNumber += 1;

        }
       
       if (targetIndexNumber == gameObjects.Count) {
            targetIndexNumber = 0;
        }
        
        EventManager.TriggerTargetLock(gameObjects[targetIndexNumber]);
    }
}
