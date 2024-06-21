using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Weapon weapon;
    [SerializeField] ThirdPersonController controller; // Non-nullable by default
    [SerializeField] StarterAssetsInputs _inputs; // Non-nullable by default
    public bool attackReady = true;

    public AudioClip hitReacionAudioClip;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();
        _inputs = GetComponent<StarterAssetsInputs>();

    }

    void Update()
    {
        if (_inputs != null && _inputs.attack && attackReady)
        {
            attackReady = false;
            Attack();
        }
    }

    public void Attack()
    {
        weapon = GetComponentInChildren<Weapon>();
        animator.SetTrigger("Attack");
        if (controller != null)
            controller.enabled = false;
    }

    public void Draw()
    {
        if (weapon != null)
            weapon.Activate();
    }

    public void Sheathe()
    {
        Debug.Log("sheathe");
        if (weapon != null)
            weapon.Deactivate();

        animator.ResetTrigger("Attack");
        if (controller != null)
            controller.enabled = true;
        attackReady = true;
    }

    public void ActivateReact()
    {
        
        if (attackReady) {
            AudioSource.PlayClipAtPoint(hitReacionAudioClip, transform.position);

            

            if (gameObject.GetComponent<Player>().GetHealth() > 0) {
                animator.SetTrigger("Hit");
            }
            
            attackReady = false;
            
            
        }
    }

    public void DeactivateReact()
    {
        animator.ResetTrigger("Hit");
        attackReady = true;
    }

    
}
