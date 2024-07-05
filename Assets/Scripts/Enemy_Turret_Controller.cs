using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy_Turret_Controller : MonoBehaviour
{
    // The target of our turret.
    public GameObject Player;

    // The turret body which rotate towards the player.
    public GameObject Turret;

    // The cannon base which rotate towards the player.
    public GameObject CannonBase;

    // The position which will instantiate the ammunition.
    public GameObject GunPointBarrel;

    public GameObject Ammunition;

    public GameObject TowerPosition;

    // Speed of turret rotation.
    private float rotationSpeed = 1.0f;

    private float rate = 1.0f;
    private float time = 0.0f;

    private int MinDist = 40;
    private int MaxDist = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inView())
        {
            lookAtPlayer();
            gunLockAtPlayer();

            time += Time.deltaTime;
            if (time > rate)
            {
                shoot();
                time = 0.0f;
            }
        }
        
        
    }

    bool inView()
    {
        float distance = Vector3.Distance(Player.transform.position, TowerPosition.transform.position);
        
        Debug.Log(distance);

        if (distance >= MinDist && distance <= MaxDist)
        {
            return true;
        }

        return false;
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
        //Debug.DrawRay(Turret.transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        Turret.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void gunLockAtPlayer()
    {

        Vector3 targetDirection = Player.transform.position - CannonBase.transform.position;

        // The step size is equal to speed times frame time.
        float speedNormalized = rotationSpeed * Time.deltaTime;

        // Rotate the up vector towards the target direction by one step.
        Vector3 newDirection = Vector3.RotateTowards(CannonBase.transform.transform.forward, targetDirection, speedNormalized, 0.0f);

        Debug.Log(newDirection);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        CannonBase.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void shoot()
    {
        GameObject newObject = Instantiate(Ammunition, GunPointBarrel.transform.position, Quaternion.identity);

        var ammoRigid = newObject.GetComponent<Rigidbody>();

        ammoRigid.velocity = transform.TransformDirection(Vector3.up * 300);
    }
}
