using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BattleSystemTests
{
    private GameObject enemyGameObject;
    private GameObject heroGameObject;
    private GameObject battleSystemGameObject;
    private GameObject playerActionCanvas;
    private GameObject playerUpgradeCanvas;
    private GameObject endScreenCanvas;
    private Enemy enemy;
    private Hero hero;
    private BattleSystem battleSystem;

    [SetUp]
    public void SetUp() // SetUp will run before every test
    {
        enemyGameObject = new GameObject("Enemy", typeof(Enemy));
        heroGameObject = new GameObject("Hero", typeof(Hero));
        battleSystemGameObject = new GameObject("BattleSystem", typeof(BattleSystem));
        playerActionCanvas = new GameObject("PlayerActionCanvas");
        playerUpgradeCanvas = new GameObject("PlayerUpgradeCanvas");
        endScreenCanvas = new GameObject("EndScreenCanvas");
        enemy = enemyGameObject.GetComponent<Enemy>();
        hero = heroGameObject.GetComponent<Hero>();
        battleSystem = battleSystemGameObject.GetComponent<BattleSystem>();
        battleSystem.hero = hero;
        battleSystem.enemy = enemy;
        battleSystem.playerActionCanvas = playerActionCanvas;
        battleSystem.playerUpgradeCanvas = playerUpgradeCanvas;
        battleSystem.endScreenCanvas = endScreenCanvas;
    }

    [TearDown]
    public void TearDown() // TearDown will run after every test
    {
        GameObject.DestroyImmediate(enemyGameObject);
        GameObject.DestroyImmediate(heroGameObject);
        GameObject.DestroyImmediate(battleSystemGameObject);
        GameObject.DestroyImmediate(playerActionCanvas);
        GameObject.DestroyImmediate(playerUpgradeCanvas);
        GameObject.DestroyImmediate(endScreenCanvas);
    }

    [Test]
    public void EnemyAttacksHero_enemyDealsDamageToPlayer()
    {
        // ARRANGE
        enemy.attackPoints = 10f;
        hero.healthPoints = 15f;

        // ACT
        battleSystem.EnemyAttacksHero();

        // ASSERT
        Assert.AreEqual(5f, battleSystem.hero.healthPoints);
    }
    
    [Test]
    public void HeroHealSelected_healsHero()
    {
        // ARRANGE
        battleSystem.hero.maxHealthPoints = 50f;
        battleSystem.hero.healthPoints = 20f;

        // ACT
        battleSystem.HeroHealSelected();

        // ASSERT
        Assert.Greater(battleSystem.hero.healthPoints, 20f);
    }

    [Test]
    public void HeroAttackSelected_heroDealsDamageToEnemy()
    {
        // ARRANGE
        hero.attackPoints = 10f;
        enemy.healthPoints = 15f;

        // ACT
        battleSystem.HeroAttackSelected();

        // ASSERT
        Assert.AreEqual(5f, battleSystem.enemy.healthPoints);
    }

    [Test]
    public void HeroArmorUpgradeSelected_IncreasesHeroMaxHealthPoints()
    {
        // ARRANGE
        hero.maxHealthPoints = 15f;

        // ACT
        battleSystem.HeroArmorUpgradeSelected();

        // ASSERT
        Assert.Greater(battleSystem.hero.maxHealthPoints, 15f);
    }

    [Test]
    public void HeroWeaponUpgradeSelected_IncreasesHeroAttackPoints()
    {
        // ARRANGE
        hero.attackPoints = 15f;

        // ACT
        battleSystem.HeroWeaponUpgradeSelected();

        // ASSERT
        Assert.Greater(battleSystem.hero.attackPoints, 15f);
    }

    [Test]
    public void ShowActionButtons_Sets_PlayerActionCanvas_ActiveOn_PLAYER_TURN_turnState()
    {
        // ARRANGE
        playerActionCanvas.SetActive(false);
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_TURN;

        // ACT
        battleSystem.ShowActionButtons();

        // ASSERT
        Assert.IsTrue(playerActionCanvas.activeSelf);
    }

    [Test]
    public void ShowActionButtons_Sets_PlayerUpgradeCanvas_ActiveOn_PLAYER_UPGRADE_turnState()
    {
        // ARRANGE
        playerUpgradeCanvas.SetActive(false);
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_UPGRADE;

        // ACT
        battleSystem.ShowActionButtons();

        // ASSERT
        Assert.IsTrue(playerUpgradeCanvas.activeSelf);
    }

    [Test]
    public void HideActionButtons_Sets_PlayerActionCanvas_Inactive()
    {
        // ARRANGE
        playerActionCanvas.SetActive(true);
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_TURN;

        // ACT
        battleSystem.HideActionButtons();

        // ASSERT
        Assert.IsFalse(playerActionCanvas.activeSelf);
    }

    [Test]
    public void HideActionButtons_Sets_PlayerUpgradeCanvas_Inactive()
    {
        // ARRANGE
        playerUpgradeCanvas.SetActive(true);
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_UPGRADE;

        // ACT
        battleSystem.HideActionButtons();

        // ASSERT
        Assert.IsFalse(playerUpgradeCanvas.activeSelf);
    }

    [Test]
    public void EndTurnAndMoveToNextTurn_IfTurnStateIs_ENEMY_TURN_andPlayerHasHealthPointsSetTurnStateTo_PLAYER_TURN()
    {
        // ARRANGE
        playerUpgradeCanvas.SetActive(true);
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_UPGRADE;

        // ACT
        battleSystem.HideActionButtons();

        // ASSERT
        Assert.IsFalse(playerUpgradeCanvas.activeSelf);
    }

    [Test]
    public void EndTurnAndMoveToNextTurn_EndGameIfPlayerHasZeroHealthPoints()
    {
        // ARRANGE
        battleSystem.hero.healthPoints = 0f;
        battleSystem.enemy.healthPoints = 10f;
        endScreenCanvas.SetActive(false);

        // ACT
        battleSystem.EndTurnAndMoveToNextTurn();

        // ASSERT
        Assert.IsTrue(endScreenCanvas.activeSelf);
    }

    [Test]
    public void EndTurnAndMoveToNextTurn_AddOneCounterTo_defeatedEnemyCount_IfEnemyHasZeroHealthPoints()
    {
        // ARRANGE
        battleSystem.hero.healthPoints = 10f;
        battleSystem.enemy.healthPoints = 0f;
        battleSystem.defeatedEnemyCount = 0;

        // ACT
        battleSystem.EndTurnAndMoveToNextTurn();

        // ASSERT
        Assert.AreEqual(1, battleSystem.defeatedEnemyCount);
    }

    [Test]
    public void EndTurnAndMoveToNextTurn_RespawnEnemyIfEnemyHasZeroHealthPoints()
    {
        // ARRANGE
        battleSystem.hero.healthPoints = 10f;
        battleSystem.enemy.startingHealthPoints = 10f;
        battleSystem.enemy.healthPoints = 0f;

        // ACT
        battleSystem.EndTurnAndMoveToNextTurn();

        // ASSERT
        Assert.Greater(battleSystem.enemy.healthPoints, 0f);
    }

    [Test]
    public void EndTurnAndMoveToNextTurn_IfCharactersAreAliveAndTurnStateIs_PLAYER_TURN_SetAndDoEnemyTurnAndSetTurnBackTo_PLAYER_TURN()
    {
        // ARRANGE
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_TURN;
        battleSystem.hero.healthPoints = 10f;
        battleSystem.enemy.healthPoints = 10f;

        // ACT
        battleSystem.EndTurnAndMoveToNextTurn();

        // ASSERT
        Assert.AreEqual(BattleSystem.TurnState.PLAYER_TURN, battleSystem.turnState);
    }

    [Test]
    public void EndTurnAndMoveToNextTurn_IfIfCharactersAreAliveAndTurnStateIs_ENEMY_TURN_SetTurnStateTo_PLAYER_TURN()
    {
        // ARRANGE
        battleSystem.turnState = BattleSystem.TurnState.ENEMY_TURN;
        battleSystem.hero.healthPoints = 10f;
        battleSystem.enemy.healthPoints = 10f;

        // ACT
        battleSystem.EndTurnAndMoveToNextTurn();

        // ASSERT
        Assert.AreEqual(BattleSystem.TurnState.PLAYER_TURN, battleSystem.turnState);
    }

    [Test]
    public void EndTurnAndMoveToNextTurn_IfEnemyHealthIsZeroAndDefeatedEnemyCountGetsIncreasedToMultipleOfThreeSetTurnStateTo_PLAYER_UPGRADE()
    {
        // ARRANGE
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_TURN;
        battleSystem.defeatedEnemyCount = 2;
        battleSystem.hero.healthPoints = 10f;
        battleSystem.enemy.healthPoints = 0;

        // ACT
        battleSystem.EndTurnAndMoveToNextTurn();

        // ASSERT
        Assert.AreEqual(BattleSystem.TurnState.PLAYER_UPGRADE, battleSystem.turnState);
    }

    [Test]
    public void EndGame_SetsEndScreenCanvasActive()
    {
        // ARRANGE
        endScreenCanvas.SetActive(false);

        // ACT
        battleSystem.EndGame();

        // ASSERT
        Assert.IsTrue(endScreenCanvas.activeSelf);
    }

    [Test]
    public void EndGame_SetsTurnStateTo_END()
    {
        // ARRANGE
        battleSystem.turnState = BattleSystem.TurnState.PLAYER_TURN;

        // ACT
        battleSystem.EndGame();

        // ASSERT
        Assert.AreEqual(BattleSystem.TurnState.END, battleSystem.turnState);
    }

    [Test]
    public void RestartGame_SetsTurnStateTo_PLAYER_TURN()
    {
        // ARRANGE
        battleSystem.turnState = BattleSystem.TurnState.END;

        // ACT
        battleSystem.RestartGame();

        // ASSERT
        Assert.AreEqual(BattleSystem.TurnState.PLAYER_TURN, battleSystem.turnState);
    }

    [Test]
    public void RestartGame_RespawnsEnemyAndHeroCharacters()
    {
        // ARRANGE
        battleSystem.hero.healthPoints = 0f;
        battleSystem.hero.startingHealthPoints = 10f;
        battleSystem.enemy.healthPoints = 1f;
        battleSystem.enemy.startingHealthPoints = 5f;

        // ACT
        battleSystem.RestartGame();

        // ASSERT
        Assert.AreEqual(battleSystem.hero.startingHealthPoints, battleSystem.hero.healthPoints);
        Assert.AreEqual(battleSystem.enemy.startingHealthPoints, battleSystem.enemy.healthPoints);
    }

    [Test]
    public void RestartGame_RespawnsSetsDefeatedEnemyCountToZero()
    {
        // ARRANGE
        battleSystem.defeatedEnemyCount = 5;

        // ACT
        battleSystem.RestartGame();

        // ASSERT
        Assert.AreEqual(0, battleSystem.defeatedEnemyCount);
    }

    [Test]
    public void RestartGame_SetsEndScreenCanvasInactive()
    {
        // ARRANGE
        endScreenCanvas.SetActive(true);

        // ACT
        battleSystem.RestartGame();

        // ASSERT
        Assert.IsFalse(endScreenCanvas.activeSelf);
    }

    [Test]
    public void RestartGame_SetsPlayerActionCanvasActive()
    {
        // ARRANGE
        playerActionCanvas.SetActive(false);

        // ACT
        battleSystem.RestartGame();

        // ASSERT
        Assert.IsTrue(playerActionCanvas.activeSelf);
    }
}

