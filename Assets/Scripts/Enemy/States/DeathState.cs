using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    private GameObject gameController;
    public override void Enter()
    {
        Debug.Log("Enter");
        enemy.animator.SetBool("IsDead", true);
    }

    public override void Exit()
    {
        Debug.Log("Exit");
        enemy.Death();
    }

    public override void Perform()
    {
        Debug.Log("Perform");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().UpdateScore(1);
        
        stateMachine.ChangeState(new DeathState());
    }
}
