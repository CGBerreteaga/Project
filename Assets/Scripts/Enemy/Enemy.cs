using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float max_hp = 25;
    public float current_hp;
    State state;
    DeadState deadState;
    ReactState reactState;
    bool dead = false;
    FSM fsm;
    public Slider healthBarSlider;
    public GameObject healthFillBar;

    

    void Start()
    {
        fsm = GetComponent<FSM>();
        state = GetComponent<State>();
        deadState = GetComponent<DeadState>();
        reactState = GetComponent<ReactState>();
        current_hp = max_hp;
        healthBarSlider.maxValue = max_hp;
        healthBarSlider.value = max_hp;
    }

    void Update()
    {
        
    }

    void HealthCheck()
    {
        if (current_hp <= 0 && !dead)
        {
            current_hp = 0;
            dead = true;
            healthFillBar.SetActive(false);
            fsm.SetState(deadState);
        }
    }

    public void TakeDamage(float damage) {
        current_hp -= damage;
        healthBarSlider.value = current_hp;
        HealthCheck();
        if(!dead) {
            fsm.SetState(reactState);
        }
    }

    public void Sleep() {
        Debug.Log("Enemy Sleeping");
    }
}
