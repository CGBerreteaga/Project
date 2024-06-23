using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] float xpGain, maxHp, currentHp, maxMana, maxXp, baseStr, baseDef, baseInt;
    [SerializeField] AttributeScriptableObject strength, defense, intelligence, hp, mana, xp, stamina;
    [SerializeField] Dictionary<AttributeScriptableObject, Wrapper> stats = new Dictionary<AttributeScriptableObject, Wrapper>();
    //Added animator and spellCost to allow code to compile. May need to remove this later
    [SerializeField] Animator animator;

    [SerializeField] Slider healthBar;

    [SerializeField] GameObject healthBarFill;

    [SerializeField] float spellCost = 5;

   
    //Player gains experience by defeating enemies.
    //If the XP reaches its max, the character levels up. 
    internal void AddXP()
    {
        var stat = Get<Notifier>(xp);
        stat.Add(xpGain);
        if (stat.Amount >= stat.Max)
        {
            stat.Add(-1 * stat.Max);
            LevelUp();
        }
    }

    //Leveling up upgrades the basic value of all stats. 
    private void LevelUp()
    {
        Get<Stats>(strength).Upgrade();
        Get<Stats>(defense).Upgrade();
        Get<Stats>(intelligence).Upgrade();
    }

    //Populate the wrapper dictionary with the player attributes 
    private void Awake()
    {
        stats.Add(strength, new Stats(baseStr));
        stats.Add(defense, new Stats(baseDef));
        stats.Add(intelligence, new Stats(baseInt));
        stats.Add(hp, new Notifier(maxHp, maxHp));
        stats.Add(mana, new Notifier(maxMana, maxMana));
        stats.Add(xp, new Notifier(0, maxXp));
        currentHp = maxHp;
        healthBar.maxValue = maxHp;
    }

    void Update() 
    {
        
        healthBar.value = currentHp;

        if (currentHp <= 0)
        {  
            healthBarFill.SetActive(false);
        }

    }

    //A character's strength determines its damage 
    public float TotalDmg
    {
        get
        {
            var str = Get<Stats>(strength);
            if (str != null)
                return str.Total;
            else
                return 0;
        }
    }

    //A character's intelligence determines its spell damage 
    public float SpellDmg
    {
        get
        {
            var @int = Get<Stats>(intelligence);
            if (@int != null)
                return @int.Total;
            else
                return 0;
        }
    }

    //A character can only cast spells if they have enough MP 
    internal bool Cast()
    {
        var stat = Get<Notifier>(mana);
        if (stat.Amount >= spellCost)
        {
            stat.Add(-1 * spellCost);
            return true;
        }
        return false;
    }
    
    //A character's defense determines how much incoming damage is reduced
    public void ReceiveDmg(float damage)
    {
        // var stat = Get<Notifier>(hp);
        // stat.Add(Get<Stats>(defense).Total - damage);
        currentHp -= damage;
        
    }

    //All classes can use this function to access a character's wrappers
    //simply by referencing an attribute ScriptableObject 
    public T Get<T>(AttributeScriptableObject attribute) where T : Wrapper
    {
        if (!stats.ContainsKey(attribute))
            return null;
        return stats[attribute] as T;
    }

    public float GetHealth() {
        return currentHp;
    }
}