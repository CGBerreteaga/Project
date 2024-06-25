using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{   
    private ThirdPersonController thirdPersonController;
    [SerializeField] Animator animator;
    [SerializeField] Slider healthBar;
    [SerializeField] Text healthText;

    [SerializeField] GameObject healthBarFill;

    [SerializeField] Slider manaBar;
    [SerializeField] Text manaText;
    [SerializeField] GameObject manaBarFill;

    [SerializeField] Slider staminaBar;
    [SerializeField] Text staminaText;
    [SerializeField] GameObject staminaBarFill;

    [SerializeField] float max_hp;
    [SerializeField] float max_mp;
    [SerializeField] float max_stamina;
     [SerializeField] float current_hp;
    [SerializeField] float current_mp;
    [SerializeField] float current_stamina;

    private void Awake()
    {
        current_hp = max_hp;
        healthBar.maxValue = max_hp;
        current_mp = max_mp;
        current_stamina = max_stamina;

        if (manaBar != null && staminaBar != null) {
            manaBar.maxValue = max_mp;
            staminaBar.maxValue = max_stamina;
        }
    }

    private void Start() {
        if (GetComponent<ThirdPersonController>() != null) {
            thirdPersonController = GetComponent<ThirdPersonController>();
            InvokeRepeating("DecreaseStaminaOncePerSecond", 0.0f, 1.0f);
        }
    }
    void Update() 
    {
        UpdateUI();
    }

    public void ReceiveDmg(float damage)
    {
        current_hp -= damage;
        
    }
    public float GetHealth() {
        return current_hp;
    }

    public float GetMana() {
        return current_mp;
    }

    public float GetStamina() {
        return current_stamina;
    }

    private void UpdateUI () {
        //Health Updates
        healthBar.value = current_hp;

        if (current_hp <= 0)
        {  
            healthBarFill.SetActive(false);
        }

        
        if (manaBar != null && staminaBar != null) {
            healthText.text = "HP " + current_hp + "/" + max_hp;
            //Mana Updates
            manaBar.value = current_mp;
            manaText.text = "MP " + current_mp + "/" + max_mp;
            if (current_mp <= 0) 
            {
                manaBarFill.SetActive(false);
            }
            //Stamina Updates
            staminaBar.value = current_stamina;
            staminaText.text = "SP " + current_stamina + "/" + max_stamina;

            if (current_stamina <= 0) 
            {
                staminaBarFill.SetActive(false);
            }
        }
    }

    void DecreaseStaminaOncePerSecond()
    {
        if (thirdPersonController.GetSpeed() > 5)
        {
            current_stamina -= 1;
            if (current_stamina < 0)
            {
                current_stamina = 0;
            }
        } else if (current_stamina <= max_stamina) {
            current_stamina += 0.5f;
            if (current_stamina > 25) {
                current_stamina = 25;
            }
        }
    }
}