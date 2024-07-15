using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float cofidenceTimer;
    private float attackTimer;

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if(!enemy.CanSeePlayer())
        {
            cofidenceTimer += Time.deltaTime;
            if (cofidenceTimer > 8)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
        else
        {
            cofidenceTimer = 0;
            attackTimer += Time.deltaTime;

            enemy.transform.LookAt(enemy.Player.transform);

            if(attackTimer>enemy.attackRate)
            {
                Attack();
            }
            enemy.LastKnownPos = enemy.Player.transform.position;
        }
    }

    private void Attack()
    {

    }
}
