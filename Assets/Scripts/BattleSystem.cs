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
        throw new NotImplementedException();
    }

    public void HeroHealSelected()
    {
        throw new NotImplementedException();
    }

    public void HeroAttackSelected()
    {
        throw new NotImplementedException();
    }

    public void HeroArmorUpgradeSelected()
    {
        throw new NotImplementedException();
    }

    public void HeroWeaponUpgradeSelected()
    {
        throw new NotImplementedException();
    }

    public void ShowActionButtons()
    {
        throw new NotImplementedException();
    }

    public void HideActionButtons()
    {
        throw new NotImplementedException();
    }

    public void EndTurnAndMoveToNextTurn()
    {
        throw new NotImplementedException();
    }

    public void EndGame()
    {
        throw new NotImplementedException();
    }

    public void RestartGame()
    {
        throw new NotImplementedException();
    }
}
