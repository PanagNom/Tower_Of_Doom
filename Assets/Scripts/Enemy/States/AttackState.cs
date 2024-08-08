using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private int confidenceTime = 5;
    private float c_timer = 0;
    private int attackTime = 3;
    private float a_timer = 0;

    public override void Enter()
    { 
        attackTime = (int)enemy.attackRate;
        enemy.animator.SetBool("IsMoving", false);
        enemy.animator.SetBool("JustStopped", false);
        enemy.animator.SetBool("WalkAgain", false);
        enemy.animator.SetBool("InRange", true);
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

        if (!enemy.CanSeePlayer())
        {
            c_timer += Time.deltaTime;
            if (c_timer > confidenceTime)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
        else
        {
            c_timer = 0;
            a_timer += Time.deltaTime;

            enemy.transform.LookAt(new Vector3 (enemy.Player.transform.position.x, 0f, enemy.Player.transform.position.z));
            //enemy.transform.LookAt(enemy.Player.transform.position);
            float playerDistance = Vector3.Distance(enemy.transform.position, enemy.Player.transform.position);

            if (playerDistance > 5)
            {
                stateMachine.ChangeState(new ApproachState());
                return;
            }

            if (a_timer > attackTime)
            {
                enemy.animator.SetBool("InRange", true);
                Attack();
                a_timer = 0;
            }
            enemy.LastKnownPos = enemy.Player.transform.position;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }
}
