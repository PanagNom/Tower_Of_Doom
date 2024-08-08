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
        enemy.animator.SetBool("IsMoving", true);
        enemy.animator.SetBool("JustStopped", false);
        enemy.animator.SetBool("WalkAgain", false);
        enemy.animator.SetBool("InRange", false);
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if (enemy.Health <= 0)
        {
            stateMachine.ChangeState(new DeathState());
        }

        if (enemy.CanSeePlayer())
        {
            Approach();
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

    private void Approach()
    {
        playerDistance = Vector3.Distance(enemy.transform.position, enemy.Player.transform.position);
        enemy.Agent.stoppingDistance = 2.5f;

        if (enemy.Agent.remainingDistance > 3)
        {
            enemy.Agent.SetDestination(enemy.Player.transform.position);
        }
        else
        {
            enemy.animator.SetBool("IsMoving", false);

            stateMachine.ChangeState(new AttackState());
        }
    }

}
