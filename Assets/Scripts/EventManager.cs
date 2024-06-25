using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance; 
    public GameObject attackSoundPrefab;
    public GameObject detectionSoundPrefab;
    public GameObject hitSoundPrefab;
    public GameObject swordSoundPrefab;

    public GameObject backstabSoundPrefab;

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
    public delegate void AudioEvent(GameObject instigator, AudioSource audioSource);
    public static event AudioEvent OnAttackSound;
    public static event AudioEvent OnDetectionSound;
    public static event AudioEvent OnHitSound;
    public static event AudioEvent OnSwordSound;
    public static event AudioEvent OnBackstabSound;
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
            Debug.LogWarning("EventManager: Sword audio prefab  is not set.");
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
                OnSwordSound?.Invoke(instigator, audioSource);
            }
            else
            {
                Debug.LogWarning("EventManager: Backstab audio source or clip is not set.");
            }
        }
        else
        {
            Debug.LogWarning("EventManager: Backstab audio prefab  is not set.");
        }
    }
}
