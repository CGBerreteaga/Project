using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon2 : MonoBehaviour
{
    [SerializeField] float weaponMinDamage = 2;
    [SerializeField] float weaponMaxDamage = 5;
    [SerializeField] public Collider _collider;

    
    

   void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<Player>().ReceiveDmg(UnityEngine.Random.Range(weaponMinDamage,weaponMaxDamage));
            other.gameObject.GetComponent<PlayerController>().isAttacking = false;
            Debug.Log("Player Hit, Remaining HP: " + other.GetComponent<Player>().GetHealth());
 
        } else if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().TakeDamage(UnityEngine.Random.Range(weaponMinDamage, weaponMaxDamage));
            Debug.Log("Enemy Hit, Remaining HP: " + other.GetComponent<Enemy>().current_hp);
            ReactState reactState = other.GetComponent<ReactState>();
            State state = other.GetComponent<State>();
            state.Transition(reactState);

        }
    }

    
}


