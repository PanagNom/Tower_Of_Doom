using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret_Controller : MonoBehaviour
{
    // The target of our turret.
    public GameObject Player;

    // The turret body which rotate towards the player.
    public GameObject Turret;

    // Speed of turret rotation.
    private float rotationSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
    }


    void lookAtPlayer()
    {
        Vector3 alteredPlayerTransform = new Vector3(Player.transform.position.x, Turret.transform.position.y, Player.transform.position.z);
        
        // Determine which direction to rotate towards
        Vector3 targetDirection = alteredPlayerTransform - Turret.transform.position;

        // The step size is equal to speed times frame time.
        float speedNormalized = rotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step.
        Vector3 newDirection  = Vector3.RotateTowards(Turret.transform.forward, targetDirection, speedNormalized, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(Turret.transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        Turret.transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
