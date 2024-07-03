using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Rigidbody enemyBody;

    public float speed = 20.0f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammunition"))
        {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Exploded");
            Destroy(this.gameObject);
        }
    }
}