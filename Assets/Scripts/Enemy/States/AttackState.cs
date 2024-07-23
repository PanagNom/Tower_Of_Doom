using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private int confidenceTime = 5;
    private float c_timer = 0;
    private int attackTime = 5;
    private float a_timer = 0;

    public override void Enter()
    {

        attackTime = (int)enemy.attackRate;
    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if(!enemy.CanSeePlayer())
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

            enemy.transform.LookAt(enemy.Player.transform);

            if(a_timer > attackTime)
            {
                enemy.animator.SetBool("InRange", true);
                Attack();
                enemy.animator.SetBool("InRange", false);
                a_timer = 0;
            }
            enemy.LastKnownPos = enemy.Player.transform.position;
        }
    }

    private void Attack()
    {

    }
}
