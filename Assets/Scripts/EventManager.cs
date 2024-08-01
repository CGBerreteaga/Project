using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public GameObject attackSoundPrefab;
    public GameObject detectionSoundPrefab;
    public GameObject hitSoundPrefab;
    public GameObject swordSoundPrefab;

    public GameObject jumpStartSoundPrefab;
    public GameObject jumpLandSoundPrefab;

    public GameObject backstabSoundPrefab;

    public delegate void AudioEvent(GameObject instigator, AudioSource audioSource);
    public static event AudioEvent OnAttackSound;
    public static event AudioEvent OnDetectionSound;
    public static event AudioEvent OnHitSound;
    public static event AudioEvent OnSwordSound;
    public static event AudioEvent OnBackstabSound;
    public static event AudioEvent OnJumpStartSound;
    public static event AudioEvent OnJumpLandSound;

    public static event Action OnDeath;
    public static event Action OnChangeTarget;
    public static event Action<GameObject> OnTargetLock;
    public static event Action<int> OnExperienceAward;
    public static event Action<int> OnManaUse;
    public static event Action<int> OnStaminaUse;
    public static event Action<int> OnStaminaRestore;
    public static event Action OnStaminaExhausted;
    public static event Action<float, float, float, int, int, int> UpdateStatBoard;
     // Added event for stamina exhaustion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void TriggerAttackSound(GameObject instigator)
    {
        if (instance != null && instance.attackSoundPrefab != null)
        {
            GameObject audioObject = Instantiate(instance.attackSoundPrefab, instigator.transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                OnAttackSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Attack audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Attack audio prefab is not set.");
        }
    }

    public static void TriggerDetectionSound(GameObject instigator)
    {
        if (instance != null && instance.detectionSoundPrefab != null)
        {
            GameObject audioObject = Instantiate(instance.detectionSoundPrefab, instigator.transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                OnDetectionSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Detection audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Detection audio prefab is not set.");
        }
    }

    public static void TriggerHitSound(GameObject instigator)
    {
        if (instance != null && instance.hitSoundPrefab != null)
        {
            GameObject audioObject = Instantiate(instance.hitSoundPrefab, instigator.transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                OnHitSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Hit audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Hit audio prefab is not set.");
        }
    }

    public static void TriggerSwordSound(GameObject instigator)
    {
        if (instance != null && instance.swordSoundPrefab != null)
        {
            GameObject audioObject = Instantiate(instance.swordSoundPrefab, instigator.transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                OnSwordSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Sword audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Sword audio prefab is not set.");
        }
    }

    public static void TriggerBackstabSound(GameObject instigator)
    {
        if (instance != null && instance.backstabSoundPrefab != null)
        {
            GameObject audioObject = Instantiate(instance.backstabSoundPrefab, instigator.transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                OnBackstabSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Backstab audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Backstab audio prefab is not set.");
        }
    }

    public static void TriggerJumpStartSound(GameObject instigator)
    {
        if (instance != null && instance.jumpStartSoundPrefab != null)
        {
            GameObject audioObject = Instantiate(instance.jumpStartSoundPrefab, instigator.transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                OnJumpStartSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Jump Start audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Jump Start audio prefab is not set.");
        }
    }

    public static void TriggerJumpLandSound(GameObject instigator)
    {
        if (instance != null && instance.jumpLandSoundPrefab != null)
        {
            GameObject audioObject = Instantiate(instance.jumpLandSoundPrefab, instigator.transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                OnJumpLandSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Jump Land audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Jump Land audio prefab is not set.");
        }
    }

    public static void TriggerOnDeath()
    {
        OnDeath?.Invoke();
    }

    public static void TriggerChangeTarget()
    {
        OnChangeTarget?.Invoke();
    }

    public static void TriggerTargetLock(GameObject instigator)
    {
        OnTargetLock?.Invoke(instigator);
    }

    public static void TriggerExperienceAward(int value)
    {
        OnExperienceAward?.Invoke(value);
    }

    public static void TriggerOnManaUse(int value)
    {
        OnManaUse?.Invoke(value);
    }

    public static void TriggerOnStaminaUse(int value)
    {
        OnStaminaUse?.Invoke(value);
    }

    public static void TriggerOnStaminaRestore(int value)
    {
        OnStaminaRestore?.Invoke(value);
    }

    public static void TriggerOnStaminaExhausted()
    {
        OnStaminaExhausted?.Invoke();
    }

    public static void TriggerStatBoardUpdate(float health, float mana, float stamina, int strength, int dexertity, int intelligence) {
        UpdateStatBoard?.Invoke(health, mana, stamina, strength, dexertity,intelligence );
    }
}
