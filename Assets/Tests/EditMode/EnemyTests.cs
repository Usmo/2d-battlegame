using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTests
{

    [Test]
    public void RespawnEnemy_healthPointsAndMaxHealthPointsHaveSameValue()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Enemy", typeof(Enemy));
        Enemy enemy = gameObject.GetComponent<Enemy>();
        enemy.startingHealthPoints = 20f;
        enemy.maxHealthPoints = 30f;
        enemy.healthPoints = 0f;
        int defeatedEnemyCount = 0;

        // ACT
        enemy.RespawnEnemy(defeatedEnemyCount);

        // ASSERT
        Assert.AreEqual(enemy.maxHealthPoints, enemy.healthPoints);
    }

    [Test]
    public void RespawnEnemy_SetsAttackPointsToStartingValueIf_defeatedEnemyCount_isZero()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Enemy", typeof(Enemy));
        Enemy enemy = gameObject.GetComponent<Enemy>();
        enemy.startingAttackPoints = 20f;
        enemy.attackPoints = 100f;
        int defeatedEnemyCount = 0;

        // ACT
        enemy.RespawnEnemy(defeatedEnemyCount);

        // ASSERT
        Assert.AreEqual(enemy.startingAttackPoints, enemy.attackPoints);
    }

    [Test]
    public void RespawnEnemy_SetsMaxHealthPointsToStartingValueIf_defeatedEnemyCount_isZero()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Enemy", typeof(Enemy));
        Enemy enemy = gameObject.GetComponent<Enemy>();
        enemy.startingHealthPoints = 20f;
        enemy.maxHealthPoints = 100f;
        int defeatedEnemyCount = 0;

        // ACT
        enemy.RespawnEnemy(defeatedEnemyCount);

        // ASSERT
        Assert.AreEqual(enemy.startingHealthPoints, enemy.maxHealthPoints);
    }

    [Test]
    public void RespawnEnemy_IncreasesMaxHealthPointsBy10PercentForEachDefeatedEnemy()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Enemy", typeof(Enemy));
        Enemy enemy = gameObject.GetComponent<Enemy>();
        enemy.startingHealthPoints = 100f;
        int defeatedEnemyCount = 2;

        // ACT
        enemy.RespawnEnemy(defeatedEnemyCount);

        // ASSERT
        Assert.AreEqual(121f, enemy.maxHealthPoints); // 100f * 1.1 * 1.1 = 121
    }

    [Test]
    public void RespawnEnemy_IncreasesAttackPointsBy10PercentForEachDefeatedEnemy()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Enemy", typeof(Enemy));
        Enemy enemy = gameObject.GetComponent<Enemy>();
        enemy.startingAttackPoints = 100f;
        int defeatedEnemyCount = 2;

        // ACT
        enemy.RespawnEnemy(defeatedEnemyCount);

        // ASSERT
        Assert.AreEqual(121f, enemy.attackPoints); // 100f * 1.1 * 1.1 = 121
    }

}
