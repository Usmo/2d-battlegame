using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public Hero hero;
    public Enemy enemy;
    public enum TurnState
    {
        PLAYER_TURN,
        ENEMY_TURN,
        PLAYER_UPGRADE,
        END
    }
    public TurnState turnState;
    public int defeatedEnemyCount = 0;

    public GameObject playerActionCanvas;
    public GameObject playerUpgradeCanvas;
    public GameObject endScreenCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        EndTurnAndMoveToNextTurn();
    }

    public void HeroWeaponUpgradeSelected()
    {
        // Upgrade heros attack
        hero.UpgradeAttack();

        // Hide upgrade selection buttons
        HideActionButtons();

        // End Upgrade turn phase
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

        // Deactivate end screen
        endScreenCanvas.SetActive(false);

        // Set turn state to player turn
        turnState = TurnState.PLAYER_TURN;

        // Show player action buttons
        ShowActionButtons();
    }
}
