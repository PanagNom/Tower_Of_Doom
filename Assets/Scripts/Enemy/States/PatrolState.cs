using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;

    // Function executed when we first enter the state.
    public override void Enter()
    {
        enemy.animator.SetBool("IsMoving", true);
        enemy.animator.SetBool("JustStopped", false);
        enemy.animator.SetBool("WalkAgain", false);
        enemy.animator.SetBool("InRange", false);

        enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
    }
    // Function executed when we exit the state.
    public override void Exit()
    {
        enemy.animator.SetBool("IsMoving", false);
    }

    // Function executed every frame as long as the state is active.
    public override void Perform()
    {
        // Move the enemy along the designated path.
        PatrolCycle();

        if (enemy.Health <= 0)
        {
            stateMachine.ChangeState(new DeathState());
        }

        // If the enemy can see the player, change to the attack state.
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState( new ApproachState());
        }
    }
    // Function to move the enemy along the designated path.
    public void PatrolCycle()
    {
        // If you have reached a paths node or this is the first run and the agent 
        // does not have a destination so the remainingDistance is 0 move to the next waypoint.
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            enemy.animator.SetBool("JustStopped", true);
            enemy.animator.SetBool("WalkAgain", false);
            // Wait for a small amount of time before moving forward.
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                enemy.animator.SetBool("JustStopped", false);
                enemy.animator.SetBool("WalkAgain", true);

                // Circle around the waypoints.
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }

                // Set the destination of the enemy for the next waypoint.
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }
    }
}
