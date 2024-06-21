using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    [SerializeField] GameObject other;

    [SerializeField] Vector3 otherDirection;
    [SerializeField] bool isInFieldOfView = false;

    [SerializeField] float angle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        otherDirection = other.transform.position - transform.position;
        angle = Mathf.Abs(Vector3.Angle(transform.forward, otherDirection));
        isInFieldOfView = IsInView();
    }

    public bool IsInView () {
        if (Mathf.Abs(Vector3.Angle(transform.forward,otherDirection)) < 45) {
            Debug.Log("Enemy Detected Player");
            return true;
        } else {
            return false;
        }
    }
}
