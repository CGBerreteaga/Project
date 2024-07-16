using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEffect : MonoBehaviour
{
    [SerializeField] GameObject stepsEffect;
    // Start is called before the first frame update
    
    void Steps() {
        Instantiate(stepsEffect,transform.position,transform.rotation);
    }
}
