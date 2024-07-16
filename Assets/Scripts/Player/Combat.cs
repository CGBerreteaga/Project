using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Weapon weapon;
    [SerializeField] ThirdPersonController controller;
    [SerializeField] StarterAssetsInputs _inputs;
    public float health;
    public float backstabDistance;

    public float distanceToEnemy;
    public bool attackReady = true;
    public bool behindEnemy = false;
    public bool backstabbed = false;

    public GameObject target;

    public GameObject backstabAbleAlertUI;

    
    //public AudioClip hitReactionAudioClip;

    public  float dotProduct;

    void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (controller == null)
            controller = GetComponent<ThirdPersonController>();

        if (_inputs == null)
            _inputs = GetComponent<StarterAssetsInputs>();

        EventManager.OnTargetLock += DesignateTarget;

        
    }
    void Update()
    {
        health = GetComponent<Player>().GetHealth();

        if (_inputs != null && _inputs.attack && attackReady)
        {
            attackReady = false;
            Attack(behindEnemy);
        }

        if (health <= 0) {
                animator.SetTrigger("Death");
            }

        
        Vector3 directionToEnemy = target.gameObject.transform.forward - transform.position;
        Vector3 playerDirection = transform.forward;
        dotProduct = Vector3.Dot(directionToEnemy.normalized, playerDirection.normalized);
        distanceToEnemy = Vector3.Distance(transform.position,target.gameObject.transform.position);
        
        // Check if player is directly behind the enemy
        if (dotProduct >= 0.7 && controller.isCrouching && distanceToEnemy < backstabDistance)
        {
            behindEnemy = true;
            backstabAbleAlertUI.SetActive(true);
        }
        else
        {
            behindEnemy = false;
            backstabAbleAlertUI.SetActive(false);
        }
        
    }

    public void Attack(bool behindEnemy)
    {
        weapon = GetComponentInChildren<Weapon>();
        
        if (behindEnemy)
        {
            target.GetComponentInParent<Combat>().backstabbed = true;
            target.transform.localPosition = transform.position + new Vector3(0,0,-.6f);
            target.transform.localRotation = transform.localRotation;
            animator.SetTrigger("Backstab");
            EventManager.TriggerBackstabSound(gameObject);
        }
        else
        {
            animator.SetTrigger("Attack");
            EventManager.TriggerAttackSound(gameObject);
        }

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
        if (weapon != null)
            weapon.Deactivate();

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Backstab");
        attackReady = true;

        if (controller != null)
            controller.enabled = true;
    }

    public void ActivateReact()
    {
        if (attackReady)
        {
            //AudioSource.PlayClipAtPoint(hitReactionAudioClip, transform.position);

            if (health > 0 && !backstabbed)
            {
                animator.SetTrigger("Hit");
                EventManager.TriggerHitSound(gameObject);
                attackReady = false;
            }
        }
    }

    public void DeactivateReact()
    {
        animator.ResetTrigger("Hit");
        attackReady = true;
    }

    
            
     
    // void OnTriggerStay(Collider collider)
    // {
    //     if (collider.gameObject.CompareTag("Enemy"))
    //     {
    //         target = collider.gameObject;
    //         Vector3 directionToEnemy = collider.gameObject.transform.forward - transform.position;
    //         Vector3 playerDirection = transform.forward;
    //         dotProduct = Vector3.Dot(directionToEnemy.normalized, playerDirection.normalized);
            
            
    //         // Check if player is directly behind the enemy
    //         if (dotProduct >= 0.7 && controller.isCrouching && Vector3.Distance(transform.position,target.gameObject.transform.position) < 0.5)
    //         {
    //             behindEnemy = true;
    //             backstabAbleAlertUI.SetActive(true);
    //         }
    //         else
    //         {
    //             behindEnemy = false;
    //             backstabAbleAlertUI.SetActive(false);
    //         }
    //     }
    // }

    // void OnTriggerExit(Collider collider)
    // {
    //     if (collider.gameObject.CompareTag("Enemy"))
    //     {
    //         target = null;
    //         behindEnemy = false;
            
    //     }
    // }

    void DesignateTarget(GameObject enemy) {
        
            target = enemy;
        
    }
}
