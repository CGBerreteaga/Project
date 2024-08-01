using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Collider weaponCollider;

    void Start() {
        weaponCollider = GetComponentInChildren<Weapon2>()._collider;
        weaponCollider.enabled = false;
    }
    public void DrawWeapon() {
        weaponCollider.enabled = true;
    }

    public void SheatheWeapon() {
        weaponCollider.enabled= false;
    }
}
