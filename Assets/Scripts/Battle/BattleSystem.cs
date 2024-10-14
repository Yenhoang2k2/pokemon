using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum BattleState {Start, PlayerAction, PlayerMove, EnemyMove, Busy}
public class BattleSystem : MonoBehaviour
{
    private BattleState state;
    [SerializeField] private BattleHub enemyHub;
    [SerializeField] private BattleHub playerHub;
    [SerializeField] private BattleUnit enemyUnit;
    [SerializeField] private BattleUnit playerUnit;
    [SerializeField] private BattleDialogBox dialogBox;
    private int currentAction;
    private int currentMove;

    private void Start()
    {
        StartCoroutine(SetUpBattle());
    }
    public IEnumerator SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHub.SetData(playerUnit.Pokemon);
        enemyHub.SetData(enemyUnit.Pokemon);
        
        dialogBox.SetNameMove(playerUnit.Pokemon.Moves);
        
        yield return dialogBox.TypeDialog($"A wild {playerUnit.Pokemon.Base.Name} appeared !");
        yield return new WaitForSeconds(1f);
        
        PlayerAction();
    }

    public void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action !"));
        dialogBox.EnableActionSelector(true);
    }

    public void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableDialogText(false);
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableMoveSelector(true);
    }

    public IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnit.Pokemon.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} use {move.Base.Name} !");
        yield return new WaitForSeconds(1f);
        DameDetails dameDetails = enemyUnit.Pokemon.TakeDame(move,playerUnit.Pokemon);
        yield return enemyHub.UpdateHp();
        yield return ShowDameDetails(dameDetails);
        if (dameDetails.fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} is Fainted !");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return EnemyMove();
        }
    }

    public IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        var move = enemyUnit.Pokemon.GetRandom();
        yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} use {move.Base.Name} !");
        yield return new WaitForSeconds(1f);
        DameDetails dameDetails = playerUnit.Pokemon.TakeDame(move,playerUnit.Pokemon);
        yield return playerHub.UpdateHp();
        yield return ShowDameDetails(dameDetails);
        if (dameDetails.fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} is Fainted !");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDameDetails(DameDetails dameDetails)
    {
        if (dameDetails.critical > 1)
        {
            yield return dialogBox.TypeDialog("A critical hit !");
            yield return new WaitForSeconds(1f);
        }

        if (dameDetails.type > 1)
        {
            yield return dialogBox.TypeDialog("It is super effective");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return dialogBox.TypeDialog("It is not super effective");
            yield return new WaitForSeconds(1f);
        }
    }
    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandheldActionSelector();
        }
        else if(state == BattleState.PlayerMove)
        {
            HandleMovesSelector();
        }
    }

    void HandleMovesSelector()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            currentMove =+ 2;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            currentMove =- 2;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            currentMove++;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentMove--;
        currentMove = Mathf.Clamp(currentMove, 0, playerUnit.Pokemon.Moves.Count-1);
        dialogBox.UpdateMovesSelector(currentMove,playerUnit.Pokemon.Moves[currentMove]);
        if(Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
    void HandheldActionSelector()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            currentAction++;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            currentAction--;
        currentAction = Mathf.Clamp(currentAction,0,1);
        dialogBox.UpdateActionSelector(currentAction);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                // run
            }
        }
    }
}
