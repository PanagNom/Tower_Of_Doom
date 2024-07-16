using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private int searchTime = 5;
    private int moveTime = 1;
    private float s_timer = 0;
    private float m_timer = 0;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.transform.position);
    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new ApproachState());
        }

        m_timer += Time.deltaTime;

        if (m_timer > moveTime)
        {
            enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
            m_timer = 0;
        }

        s_timer += Time.deltaTime;

        if(s_timer > searchTime )
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }
}
