
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] CapsuleCollider _collider;
    [SerializeField] Combat combatScript;
    [SerializeField] float weaponDamage = 10;
    [SerializeField] float backstabWeaponDamage = 1000;

    [SerializeField] AudioClip[] weaponSound;
    
    public void Awake() 
    {
        combatScript = GetComponentInParent<Combat>();
    }
    public void Activate()
    {
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _collider.enabled = false;
    }

    public float GetWeaponDamage() {
        return weaponDamage;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Combat>() != null && other.gameObject.GetComponent<Combat>().attackReady && !combatScript.behindEnemy)
        {
            other.gameObject.GetComponent<Combat>().ActivateReact();
            EventManager.TriggerSwordSound(other.gameObject);
            other.gameObject.GetComponent<Player>().ReceiveDmg(weaponDamage);
        } else if(other.gameObject.GetComponent<Combat>() != null && other.gameObject.GetComponent<Combat>().attackReady && combatScript.behindEnemy)
        {
            other.gameObject.GetComponent<Combat>().ActivateReact();
            other.gameObject.GetComponent<Player>().ReceiveDmg(backstabWeaponDamage);
        }
    }

    //ICharacter is an interface that all game character inherit from
    // [SerializeField] ICharacter owner;
    // private void OnValidate()
    // {
    //     //We get a reference to the ICharacter by checking the parent GameObjects
    //     owner = GetComponentInParent<ICharacter>();
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     var character = other.GetComponent<ICharacter>();
    //     //Avoid attacking yourself
    //     if (character == owner)
    //     {
    //         return;
    //     }
    //     if (character != null)
    //     {
    //         character.ReceiveDmg(owner.TotalDmg);
    //         //Deactivate the weapon to prevent damaging multiple times with one swing.
    //         Deactivate();
    //     }
    // }
}
