using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Rigidbody enemyBody;

    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyBody.AddForce(-transform.forward * speed, ForceMode.Force);
    }
}