using UnityEngine;

public class DeadState : State
{
    void OnEnable()
    {
        EventManager.TriggerExperienceAward(GetComponent<ExpValue>().GetExperiencePoints());
        animator.SetTrigger("Die");
        agent.speed = 0;
        los.alertedUI.SetActive(false);


    }
}
