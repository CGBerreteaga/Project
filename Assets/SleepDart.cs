using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SleepDart : MonoBehaviour
{
    [SerializeField] int abilityManaCost = 10;
    [SerializeField] int sleepDartsAvailable;
    [SerializeField] int sleepDartsMax = 10;
    [SerializeField] TextMeshProUGUI sleepDartCurrentText;
    // Start is called before the first frame update
    void Start()
    {
       
        sleepDartsAvailable = sleepDartsMax;
    }

    // Update is called once per frame
    

    public void UseSleepDart() {
        EventManager.TriggerOnManaUse(abilityManaCost);
        sleepDartsAvailable -= 1;
        sleepDartCurrentText.text = "" + sleepDartsAvailable;
        if (sleepDartsAvailable <= 0) {
            sleepDartsAvailable = 0;
        }
    }

    
}
