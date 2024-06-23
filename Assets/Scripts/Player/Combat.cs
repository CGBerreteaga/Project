using StarterAssets;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Weapon weapon;
    [SerializeField] ThirdPersonController controller;
    [SerializeField] StarterAssetsInputs _inputs;
    public float playerHealth;
    public bool attackReady = true;
    public bool behindEnemy = false;
    public bool backstabbed = false;

    public Combat enemy;
    public AudioClip hitReactionAudioClip;

    public  float dotProduct;

    void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (controller == null)
            controller = GetComponent<ThirdPersonController>();

        if (_inputs == null)
            _inputs = GetComponent<StarterAssetsInputs>();
        
        
    }

    void Update()
    {
        playerHealth = GetComponent<Player>().GetHealth();

        if (_inputs != null && _inputs.attack && attackReady)
        {
            attackReady = false;
            Attack(behindEnemy);
        }

        if (playerHealth <= 0 && backstabbed) {
                Debug.Log("Backstab Death");
                animator.SetTrigger("Backstab Death");
            } else if (playerHealth <= 0 && !backstabbed) {
                Debug.Log("Death");
                animator.SetTrigger("Death");
            }
    }

    public void Attack(bool behindEnemy)
    {
        weapon = GetComponentInChildren<Weapon>();
        if (behindEnemy)
        {
            enemy.backstabbed = true;
            animator.SetTrigger("Backstab");
        }
        else
        {
            animator.SetTrigger("Attack");
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
            AudioSource.PlayClipAtPoint(hitReactionAudioClip, transform.position);

            if (playerHealth > 0 && !backstabbed)
            {
                animator.SetTrigger("Hit");
                attackReady = false;
            }
        }
    }

    public void DeactivateReact()
    {
        animator.ResetTrigger("Hit");
        attackReady = true;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Vector3 directionToEnemy = collider.gameObject.transform.forward - transform.position;
            Vector3 playerDirection = transform.forward;
            dotProduct = Vector3.Dot(directionToEnemy.normalized, playerDirection.normalized);
            enemy = collider.gameObject.GetComponent<Combat>();
            
            // Check if player is directly behind the enemy
            if (dotProduct >= 0.7)
            {
                behindEnemy = true;
            }
            else
            {
                behindEnemy = false;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            behindEnemy = false;
            enemy = null;
        }
    }
}
