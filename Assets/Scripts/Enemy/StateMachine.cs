using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Hold the current active state of the state-machine.
    public BaseState activeState;

    // Initialize the state-machine with the patrol state.
    public void Initialize()
    {
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        // In each update the state-machine performs the state function.
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    // Change the state of the state-machine.
    public void ChangeState(BaseState newState)
    {
        // Perform the exit of the previous state.
        if (activeState != null)
        {
            activeState.Exit();
        }

        // Update the activate state with the new one.
        activeState = newState;

        //Fail-sage null check to make sure the new state wasn't null
        if (activeState != null)
        {
            // Keep the reference to this state-machine.
            activeState.stateMachine = this;
            // Populate the news state enemy object.
            activeState.enemy = GetComponent<Enemy>();
            // Enter the new active state.
            activeState.Enter();
        }
    }

}
