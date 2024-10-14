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
