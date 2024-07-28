using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpValue : MonoBehaviour
{
    // The amount of experience this enemy is worth
    [SerializeField]
    private int experiencePoints = 10; // Default value, adjust as needed

    // This method can be called to get the experience value of the enemy
    public int GetExperiencePoints()
    {
        return experiencePoints;
    }

    // You can add other methods or functionality here as needed
}
