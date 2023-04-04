using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public Hero hero;
    public Enemy enemy;
    public enum TurnState
    {
        PLAYER_TURN,
        ENEMY_TURN,
        PLAYER_UPGRADE,
        END,
        PLAYER_UPGRADE_DONE
    }
    public TurnState turnState;
    public int defeatedEnemyCount = 0;

    // UI variables
    public GameObject playerActionCanvas;
    public GameObject playerUpgradeCanvas;
    public GameObject endScreenCanvas;

    public Text heroStatsText;
    public Text enemyStatsText;
    public Text endText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetStatTextsForBothCharacters();
    }

    public void EnemyAttacksHero()
    {
        // Enemy attcks hero
        enemy.Attack(hero);

        // End enemys turn
        EndTurnAndMoveToNextTurn();
    }

    public void HeroHealSelected()
    {
        // Hero heals
        hero.Heal();

        // Hide action buttons
        HideActionButtons();

        // Play heal animation if animator is found
        if (hero.animator != null) hero.animator.SetTrigger("Heal");

        // End players turn
        EndTurnAndMoveToNextTurn();
    }

    public void HeroAttackSelected()
    {
        // Hero attack enemy
        hero.Attack(enemy);

        // Hide action buttons
        HideActionButtons();

        // End players turn
        EndTurnAndMoveToNextTurn();
    }

    public void HeroArmorUpgradeSelected()
    {
        // Upgrade heros health
        hero.UpgradeHealth();

        // Hide upgrade selection buttons
        HideActionButtons();

        // End Upgrade turn phase
        turnState = TurnState.PLAYER_UPGRADE_DONE;
        EndTurnAndMoveToNextTurn();
    }

    public void HeroWeaponUpgradeSelected()
    {
        // Upgrade heros attack
        hero.UpgradeAttack();

        // Hide upgrade selection buttons
        HideActionButtons();

        // End Upgrade turn phase
        turnState = TurnState.PLAYER_UPGRADE_DONE;
        EndTurnAndMoveToNextTurn();
    }

    public void ShowActionButtons()
    {
        // Check what turnState game is at
        if (turnState == TurnState.PLAYER_TURN)
        {
            // Set PlayerActionCanvas active to show action options to player
            playerActionCanvas.SetActive(true);
        }
        else if (turnState == TurnState.PLAYER_UPGRADE)
        {
            // Set PlayerUpgradeCanvas active to show upgrade options to player
            playerUpgradeCanvas.SetActive(true);
        }
    }

    public void HideActionButtons()
    {
        // Set player action and upgrade canvases inactive
        playerActionCanvas.SetActive(false);
        playerUpgradeCanvas.SetActive(false);
    }

    public void EndTurnAndMoveToNextTurn()
    {
        
        if (hero.healthPoints == 0f)
        {
            // End game if hero is at zero health points
            EndGame();
        } 
        else if (enemy.healthPoints == 0f)
        {
            // Add counter to defeated enemies if enemy is at zero health points
            defeatedEnemyCount += 1;

            // Set turn state to player upgrade if defeatedEnemyCount is multiple of three
            if (defeatedEnemyCount % 3 == 0)
            {
                turnState = TurnState.PLAYER_UPGRADE;
            }

            // Respawn enemy with defeated enemy counter modifier
            enemy.RespawnEnemy(defeatedEnemyCount);

            // Play respawn animation if animator is found
            if (enemy.animator != null) enemy.animator.SetTrigger("Recover");
        }
        else
        {
            // Change turnState if both characters are alive
            if(turnState == TurnState.PLAYER_TURN)
            {
                turnState = TurnState.ENEMY_TURN;
            }
            else if (turnState == TurnState.ENEMY_TURN)
            {
                turnState = TurnState.PLAYER_TURN;
            }
            else if (turnState == TurnState.PLAYER_UPGRADE_DONE)
            {
                // Change to PLAYER_TURN after upgrade is done
                turnState = TurnState.PLAYER_TURN;
            }
        }

        // Check current turn state and define next turn
        switch (turnState)
        {
            case TurnState.PLAYER_TURN:
                // Show action options on players turn
                ShowActionButtons();
                break;

            case TurnState.PLAYER_UPGRADE:
                // Show upgrade options on player upgrade phase
                ShowActionButtons();
                break;
                    
            case TurnState.END:
                break;

            case TurnState.ENEMY_TURN:
                // Enemy attacks on its turn
                EnemyAttacksHero();
                break;

            default:
                break;
        }
    }

    public void EndGame()
    {
        // Set turn state to END
        turnState = TurnState.END;

        // Update EndText if it is assigned
        if (endText != null)
        {
            endText.text = "Game over! Defeated advesaries: " + defeatedEnemyCount;
        }

        // Activate end screen
        endScreenCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        // Set defeated enemy counter to zero
        defeatedEnemyCount = 0;

        // Respawn both characters
        hero.RespawnHero();
        enemy.RespawnEnemy(defeatedEnemyCount);

        // Play respawn animation if animator is found
        if (hero.animator != null) hero.animator.SetTrigger("Recover");

        // Deactivate end screen
        endScreenCanvas.SetActive(false);

        // Set turn state to player turn
        turnState = TurnState.PLAYER_TURN;

        // Show player action buttons
        ShowActionButtons();
    }

    void SetStatTextsForBothCharacters()
    {
        // Round stat numbers and update text element for enemy
        double roundedHP = Math.Round(enemy.healthPoints, 1);
        double roundedMaxHP = Math.Round(enemy.maxHealthPoints, 1);
        double roundedAttack = Math.Round(enemy.attackPoints, 1);
        enemyStatsText.text = "Enemy attack: " + roundedAttack + " Enemy HP: " + roundedHP + "/" + roundedMaxHP;

        // Round stat numbers and update text element for hero
        roundedHP = Math.Round(hero.healthPoints, 1);
        roundedMaxHP = Math.Round(hero.maxHealthPoints, 1);
        roundedAttack = Math.Round(hero.attackPoints, 1);
        heroStatsText.text = "Hero attack: " + roundedAttack + " Hero HP: " + roundedHP + "/" + roundedMaxHP;
    }
}
