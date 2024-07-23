using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon2 : MonoBehaviour
{
    [SerializeField] float weaponMinDamage = 2;
    [SerializeField] float weaponMaxDamage = 5;
    [SerializeField] float behindAttackMultiplier = 100f; // Multiplier for damage when attacked from behind
    [SerializeField] public Collider _collider;


    void Start() {
        EventManager.OnBackstabSound += HandleBackstabSound;
        EventManager.OnAttackSound += HandleAttackSound;
        EventManager.OnHitSound += HandleHitSound;
    }

    void OnDestroy() {
        EventManager.OnBackstabSound -= HandleBackstabSound;
        EventManager.OnAttackSound -= HandleAttackSound;
        EventManager.OnHitSound -= HandleHitSound;
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            float damage = UnityEngine.Random.Range(weaponMinDamage, weaponMaxDamage);
            other.GetComponent<Player>().ReceiveDmg(damage);
            other.gameObject.GetComponent<PlayerController>().isAttacking = false;
            other.gameObject.GetComponent<Animator>().SetBool("isReacting", true);
            EventManager.TriggerAttackSound(gameObject);
            EventManager.TriggerHitSound(other.gameObject);
            Debug.Log("Player Hit, Remaining HP: " + other.GetComponent<Player>().GetHealth());
 
        } else if (other.gameObject.CompareTag("Enemy")) {
            // Calculate attack direction
            Vector3 attackDirection = GetComponentInParent<PlayerController>().transform.position - other.transform.position;
            attackDirection.Normalize();

            // Get enemy's forward direction
            Vector3 enemyForward = other.transform.forward;
            enemyForward.Normalize();

            // Calculate dot product to determine if the attack is from behind
            float dotProduct = Vector3.Dot(attackDirection, enemyForward);

            // Determine if the attack is from behind
            bool isFromBehind = dotProduct < 0;  // Negative dot product indicates attack from behind

            // Calculate damage
            float damage = UnityEngine.Random.Range(weaponMinDamage, weaponMaxDamage);
            if (isFromBehind) {
                damage *= behindAttackMultiplier; // Increase damage if attacked from behind
                Debug.Log("Hit From Behind for " + damage);
                EventManager.TriggerBackstabSound(gameObject);
            } else {
                Debug.Log("Hit Enemy From Front for " + damage);
                EventManager.TriggerAttackSound(gameObject);
                EventManager.TriggerHitSound(other.gameObject);
            }

            // Apply damage to enemy
            other.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Enemy Hit, Remaining HP: " + other.GetComponent<Enemy>().current_hp);

            // React to the attack
            ReactState reactState = other.GetComponent<ReactState>();
            State state = other.GetComponent<State>();
            state.Transition(reactState);
        }
    }

    public void HandleBackstabSound(GameObject instigator, AudioSource audioSource) {
        Debug.Log("Backstab Sound Triggered");
    }

    public void HandleAttackSound(GameObject instigator, AudioSource audioSource) {
        Debug.Log("Attack Sound Triggered");
    }

    public void HandleHitSound(GameObject instigator, AudioSource audioSource) {
        Debug.Log("Hit Sound Triggered");
    }
}
