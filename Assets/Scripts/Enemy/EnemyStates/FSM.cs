using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [SerializeField] State[] states; // Change to State[] array
    [SerializeField] State current;

    void OnValidate()
    {
        states = GetComponentsInChildren<State>();
    }

    void Start()
    {
        current = GetComponent<PatrolState>();

        foreach (var state in states)
        {
            state.onEnter += ChangeState;
            state.enabled = false;
        }
        current.enabled = true;
    }

    void ChangeState(State other)
    {
        current.enabled = false;
        current = other;
        current.enabled = true;
    }

    public void SetState(State other) {
        ChangeState(other);
    }
}
