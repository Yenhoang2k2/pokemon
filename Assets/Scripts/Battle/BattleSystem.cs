using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleHub enemyHub;
    [SerializeField] private BattleHub playerHub;
    [SerializeField] private BattleUnit enemyUnit;
    [SerializeField] private BattleUnit playerUnit;

    private void Start()
    {
        SetUpBattle();
    }

    public void SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHub.SetData(playerUnit.Pokemon);
        enemyHub.SetData(enemyUnit.Pokemon);
    }
}
