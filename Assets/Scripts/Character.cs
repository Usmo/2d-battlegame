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

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        // Deduct damageAmount from healthPoints
        healthPoints -= damageAmount;

        // Set health to zero if it reduced below zero
        if (healthPoints < 0)
        {
            healthPoints = 0;

            // Play death animation if animator is found and character has zero health points
            if (animator != null) animator.SetTrigger("Death");
        }
        else
        {
            // Play hurt animation if animator is found and character is alive
            if (animator != null) animator.SetTrigger("Hurt");
        }
    }
    public void Attack(Character target)
    {
        // Deal attackPoints amount of damage to target
        target.TakeDamage(attackPoints);

        // Play attack animation if animator is found
        if (animator != null) animator.SetTrigger("Attack");
    }

}
