using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] float chaseThreshold = 3.0f;
    [SerializeField] bool attacking = false;

    [SerializeField] float coolDown = 3;
    [SerializeField] float elapsedTime;
    GameObject player;

    void OnEnable()
    {
        attacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        elapsedTime = coolDown;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (!attacking && distanceToPlayer <= chaseThreshold && elapsedTime >= coolDown)
        {   
            PerformAttack();
            elapsedTime = 0;
            
        }
        else if (distanceToPlayer > chaseThreshold)
        {
            Transition(chaseState);
        }
    }

    void PerformAttack()
    {
        attacking = true;
        agent.speed = 0;
        animator.SetTrigger("Attack");
    }

    public void OnAttackAnimationEnd()
    {
        attacking = false;
        animator.ResetTrigger("Attack");
    }

    void OnDisable()
    {
        animator.ResetTrigger("Attack");
        attacking = false;
    }
}
