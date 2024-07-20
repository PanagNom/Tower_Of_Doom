using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Vector3 lastKnownPos;
    private NavMeshAgent m_Agent;
    private StateMachine m_StateMachine;

    public Path path;
    public NavMeshAgent Agent { get { return m_Agent; } }
    public GameObject Player { get { return player; } }
    public Vector3 LastKnownPos { get { return lastKnownPos; } set { lastKnownPos = value; } }

    [SerializeField]
    private string currentState;

    private float health;
    private float maxHealth = 10f;
    private float sightDistance = 10f;
    public float fieldOfView = 85f;
    public float attackRate;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        m_Agent = GetComponent<NavMeshAgent>();
        m_StateMachine = GetComponent<StateMachine>();
        player = GameObject.FindGameObjectWithTag("Player");

        m_StateMachine.Initialize();
    }
    private void Update()
    {
        currentState = m_StateMachine.activeState.ToString();
    }
    public bool CanSeePlayer()
    {
        // If the player exists.
        if(Player)
        {
            // Check the distance between the player and the enemy.
            if (Vector3.Distance(transform.position, Player.transform.position) < sightDistance)
            {
                // Find the direction of the player and calculate the angle the enemy has with the player direction.
                Vector3 targetDirection = Player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(transform.forward, targetDirection);

                // If the angle is inside the fieldOfView of the enemy.
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    // Create a ray from the enemy with direction to the player
                    Ray ray = new Ray(transform.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();

                    // Perform a check to see if the ray hit anything the enemy can see.
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        // If it did hit and that object was the player return true that you can see the player.
                        if (hitInfo.transform.gameObject == Player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance*10);
                            return true;
                        }
                    }
                }
            }
        }
        // Return false because you can't see the player.
        return false;
    }

    private void Death()
    {
        Debug.Log("Enemy death.");
        Destroy(gameObject);
    }
}
