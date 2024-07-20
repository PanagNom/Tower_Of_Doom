using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : BaseState
{
    private float playerDistance;
    private int engagmentTime = 5;
    private float timer = 0f;

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            playerDistance = Vector3.Distance(enemy.transform.position, enemy.Player.transform.position);
            if (playerDistance > 1)
            {
                enemy.Agent.SetDestination(enemy.Player.transform.position);
            }
            else
            {
                stateMachine.ChangeState(new AttackState());
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > engagmentTime)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

}
