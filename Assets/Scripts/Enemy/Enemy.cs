using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public Vector3 LastKnownPos;

    private NavMeshAgent m_Agent;
    private StateMachine m_StateMachine;
    public float attackRate;

    public Path path;
    public NavMeshAgent Agent { get { return m_Agent; } }

    [SerializeField]
    private string currentState;

    private float health;
    private float maxHealth = 10f;
    private float sightDistance = 10f;
    public float fieldOfView = 85f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        m_Agent = GetComponent<NavMeshAgent>();
        m_StateMachine = GetComponent<StateMachine>();
        Player = GameObject.FindGameObjectWithTag("Player");

        m_StateMachine.Initialise();
    }
    private void Update()
    {
        currentState = m_StateMachine.activeState.ToString();
    }
    public bool CanSeePlayer()
    {
        if(Player)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = Player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(transform.forward, targetDirection);

                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();

                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == Player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
