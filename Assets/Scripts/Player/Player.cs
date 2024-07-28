using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
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

    [SerializeField] int playerLevel = 1;
    [SerializeField] Text playerLevelText;
    [SerializeField] Slider experienceBar;
    [SerializeField] float max_hp;
    [SerializeField] float max_mp;
    [SerializeField] float max_stamina;
    [SerializeField] float current_hp;
    [SerializeField] float current_mp;
    [SerializeField] float current_stamina;

    [SerializeField] float max_experience = 20;
    [SerializeField] float current_experience = 0;

    private void Awake()
    {
        current_hp = max_hp;
        healthBar.maxValue = max_hp;
        current_mp = max_mp;
        current_stamina = max_stamina;

        if (manaBar != null && staminaBar != null)
        {
            manaBar.maxValue = max_mp;
            staminaBar.maxValue = max_stamina;
            experienceBar.maxValue = max_experience;
        }
    }

    private void Start()
    {
        if (GetComponent<ThirdPersonController>() != null)
        {
            thirdPersonController = GetComponent<ThirdPersonController>();
        }

        EventManager.OnExperienceAward += ReceiveExp;
        EventManager.OnManaUse += ExpendMana;
        EventManager.OnStaminaUse += ExpendStamina;
        EventManager.OnStaminaRestore += RestoreStamina;

        playerLevelText.text = "LvL " + playerLevel;
    }

    private void OnDestroy()
    {
        EventManager.OnExperienceAward -= ReceiveExp;
        EventManager.OnManaUse -= ExpendMana;
        EventManager.OnStaminaUse -= ExpendStamina;
        EventManager.OnStaminaRestore -= RestoreStamina;
    }

    void Update()
    {
        UpdateUI();
        ExperienceCheck();
    }

    public void ReceiveDmg(float damage)
    {
        current_hp -= damage;
    }

    public float GetHealth()
    {
        return current_hp;
    }

    public float GetMana()
    {
        return current_mp;
    }

    public float GetStamina()
    {
        return current_stamina;
    }

    public void ReceiveExp(int value)
    {
        current_experience += value;
    }

    private void ExperienceCheck()
    {
        if (current_experience >= max_experience)
        {
            playerLevel++;
            playerLevelText.text = "LvL " + playerLevel;
            max_experience = max_experience * playerLevel;
            current_experience = 0;
            experienceBar.maxValue = max_experience;
        }
    }

    public void ExpendMana(int manaUsed)
    {
        current_mp -= manaUsed;
    }

    public void ExpendStamina(int staminaUsed)
    {
        current_stamina -= staminaUsed;
        if (current_stamina <= 0)
        {
            current_stamina = 0;
            EventManager.TriggerOnStaminaExhausted();
        }
    }

    public void RestoreStamina(int staminaRestoreRate)
    {
        current_stamina += staminaRestoreRate;
        if (current_stamina >= max_stamina)
        {
            current_stamina = max_stamina;
        }
    }

    private void UpdateUI()
    {
        healthBar.value = current_hp;
        if (current_hp <= 0)
        {
            healthBarFill.SetActive(false);
            EventManager.TriggerOnDeath();
        }

        if (manaBar != null && staminaBar != null)
        {
            healthText.text = "HP " + ((int)current_hp) + "/" + max_hp;
            manaBar.value = current_mp;
            manaText.text = "MP " + current_mp + "/" + max_mp;
            if (current_mp <= 0)
            {
                manaBarFill.SetActive(false);
            }
            staminaBar.value = current_stamina;
            staminaText.text = "SP " + current_stamina + "/" + max_stamina;

            if (current_stamina <= 0)
            {
                staminaBarFill.SetActive(false);
            }

            experienceBar.value = current_experience;
        }
    }
}
