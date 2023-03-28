using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float healthPoints;
    public float maxHealthPoints;
    public float attackPoints;
    public float startingHealthPoints;
    public float startingAttackPoints;

    public void TakeDamage(float damageAmount)
    {
        // Deduct damageAmount from healthPoints
        healthPoints -= damageAmount;

        // Set health to zero if it reduced below zero
        if (healthPoints < 0)
        {
            healthPoints = 0;
        }
    }
    public void Attack(Character target)
    {
        // Deal attackPoints amount of damage to target
        target.TakeDamage(attackPoints);
    }

}
