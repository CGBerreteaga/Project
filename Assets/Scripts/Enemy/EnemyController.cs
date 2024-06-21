using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public UnityEvent onHitEvent;
    public Animator animator;
    void Start()
    {
        // Subscribe a method to the event (optional)
        animator = GetComponent<Animator>();
        onHitEvent.AddListener(ReactToHit);
    }

    void ReactToHit()
    {
        // Code to react to being hit, such as playing an animation, reducing health, etc.
        animator.SetTrigger("Hit");
    }
}
