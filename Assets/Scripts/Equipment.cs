using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item, IModifier
{
    //The set specifies what GameObjects this equipment is comprised of
    //This allows us to distinguish between equipment that should replace other equipment when equipped
    public EnumsScriptableObject set;
    //objects is a list of GameObjects that form the equipment
    public List<GameObject> objects;
    //We keep track of all the different equipment that are currently equipped by the player
    //This allows us to unequip them when equipping a different set
    public static Dictionary<EnumsScriptableObject, Equipment> active = new Dictionary<EnumsScriptableObject, Equipment>();

    public override void Use(Player player)
    {
        if (active.ContainsKey(set))
        {
            //First deactivate the equipped equipment
            active[set].Unequip(player);

            //And update the active equipment
            active[set] = this;
        }
        else
            active.Add(set, this);
        //Then equip the new equipment
        Equip(player);
    }

    public virtual void Equip(Player player)
    {
        //Activate all GameObjects that form this equipment
        objects.ForEach(z => z.SetActive(true));
        //Update the player stats with the modifier of this equipment
        var stat = player.Get<Stats>(attribute);
        if (stat != null)
            stat.AddModifier(this);
    }

    public virtual void Unequip(Player player)
    {
        objects.ForEach(z => z.SetActive(false));
        var stat = player.Get<Stats>(attribute);
        if (stat != null)
            stat.RemoveModifier(this);
    }
}
