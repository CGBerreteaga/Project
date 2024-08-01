using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Scripting;
using UnityEngine;

public class StatBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI mana;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI dexertity;
    [SerializeField] TextMeshProUGUI intelligence;
    


    // Start is called before the first frame update
    void Start()
    {
        EventManager.UpdateStatBoard += UpdateStats;
        Debug.Log("Updating Stat Board");
    }

    public void UpdateStats(float health, float mana, float stamina, int strength, int dexertity, int intelligence) {
        this.health.text = "" + health;
        this.mana.text =  "" + mana;
        this.stamina.text = "" +  stamina;
        this.strength.text = "" +  strength;
        this.dexertity.text = "" +  dexertity;
        this.intelligence.text = "" +  intelligence;

    }
}
