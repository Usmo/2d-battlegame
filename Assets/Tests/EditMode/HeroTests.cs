using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HeroTests
{

    [Test]
    public void Heal_IncreasesHealthPointsBy25PercentOfMaxHealthPoints()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.maxHealthPoints = 100f;
        hero.healthPoints = 50f;

        // ACT
        hero.Heal();

        // ASSERT
        Assert.AreEqual(75f, hero.healthPoints);
    }

    [Test]
    public void Heal_LimitsHealthPointsToMaxHealthPoints()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.maxHealthPoints = 100f;
        hero.healthPoints = 95f;

        // ACT
        hero.Heal();

        // ASSERT
        Assert.AreEqual(hero.maxHealthPoints, hero.healthPoints);
    }

    [Test]
    public void UpgradeHealth_IncreasesMaxHealthPointsBy20Percent()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.maxHealthPoints = 10f;

        // ACT
        hero.UpgradeHealth();

        // ASSERT
        Assert.AreEqual(12f, hero.maxHealthPoints);
    }

    [Test]
    public void UpgradeHealth_IncreasesHealthPointsBy20Percent()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.healthPoints = 10f;

        // ACT
        hero.UpgradeHealth();

        // ASSERT
        Assert.AreEqual(12f, hero.healthPoints);
    }

    [Test]
    public void UpgradeAttack_IncreasesAttackPointsBy20Percent()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.attackPoints = 10f;

        // ACT
        hero.UpgradeAttack();

        // ASSERT
        Assert.AreEqual(12f, hero.attackPoints);
    }

    [Test]
    public void RespawnHero_SetsMaxHealthPointsToStartingValue()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.maxHealthPoints = 10f;

        // ACT
        hero.RespawnHero();

        // ASSERT
        Assert.AreEqual(hero.startingHealthPoints, hero.maxHealthPoints);
    }

    [Test]
    public void RespawnHero_SetsHealthPointsToStartingValue()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.healthPoints = 10f;

        // ACT
        hero.RespawnHero();

        // ASSERT
        Assert.AreEqual(hero.startingHealthPoints, hero.healthPoints);
    }

    [Test]
    public void RespawnHero_SetsAttackPointsToStartingValue()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Hero", typeof(Hero));
        Hero hero = gameObject.GetComponent<Hero>();
        hero.attackPoints = 10f;

        // ACT
        hero.RespawnHero();

        // ASSERT
        Assert.AreEqual(hero.startingAttackPoints, hero.attackPoints);
    }
}
