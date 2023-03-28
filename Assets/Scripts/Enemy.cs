using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public void RespawnEnemy(int defeatedEnemyCount)
    {
        // Set maxHealthPoints and attackPoints to starting values
        maxHealthPoints = startingHealthPoints;
        attackPoints = startingAttackPoints;

        // Increase maxHealthPoints and attackPoints by 10 percent for each defeated enemy
        for(int i = 0; i < defeatedEnemyCount; i++)
        {
            maxHealthPoints = (float)(maxHealthPoints * 1.1);
            attackPoints = (float)(attackPoints * 1.1);
        }

        // Set healthPoints to same value as maxHealthPoints
        healthPoints = maxHealthPoints;
    }
}
