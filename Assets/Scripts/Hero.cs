using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{

    public void Heal()
    {
        // Calculate heal amount (25% of maxHealthPoints) 
        float healAmount = (float)(maxHealthPoints * 0.25);

        // Add healAmount to healthPoints
        healthPoints += healAmount;

        // Check that addition does not add health points over maxHealthPoints limit
        if (healthPoints > maxHealthPoints)
        {
            healthPoints = maxHealthPoints;
        }
    }

    public void UpgradeHealth()
    {
        // Calculate and increase maxHealthPoints by 20% of maxHealthPoints
        maxHealthPoints += (float)(maxHealthPoints * 0.20);

        // Calculate and increase healthPoints by 20% of healthPoints
        healthPoints += (float)(healthPoints * 0.20);
    }

    public void UpgradeAttack()
    {
        // Calculate and increase attackPoints by 20% of attackPoints
        attackPoints += (float)(attackPoints * 0.20);
    }

    public void RespawnHero()
    {
        // Set healthPoints and maxHealthPoints to startingHealthPoints
        maxHealthPoints = startingHealthPoints;
        healthPoints = startingHealthPoints;

        // Set attackPoints to startingAttackPoints
        attackPoints = startingAttackPoints;
    }
}
