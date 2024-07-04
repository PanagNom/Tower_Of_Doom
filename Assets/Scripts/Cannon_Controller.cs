using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Controller : MonoBehaviour
{
    public GameObject firePoint;
    public Rigidbody Ammunition;

    public float speed;
    public MyInputs _inputs;

    public Transform parentTransform;
    public Rigidbody parentRigid;

    public float fireRate;
    public float ammunitionVelocity;

    private float timeCount  = 0;

    private bool rotateAllowedVert;
    private bool rotateAllowedHorz;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        speed = 60f;
        fireRate = 1f;
        ammunitionVelocity = 300f;

        _inputs = new MyInputs();
        _inputs.MyInputMap.Enable();

        _inputs.MyInputMap.MoveCannon.performed += _ => { StartCoroutine(MoveCannonVertically()); };
        _inputs.MyInputMap.MoveCannon.canceled += _ => { rotateAllowedVert = false; };

        _inputs.MyInputMap.RotateCannon.performed += _ => { StartCoroutine(MoveCannonHorizontally()); };
        _inputs.MyInputMap.RotateCannon.canceled += _ => { rotateAllowedHorz = false; };

        _inputs.MyInputMap.MoveTank.performed += _ => { StartCoroutine(MoveTankInXY()); };
        _inputs.MyInputMap.MoveTank.canceled += _ => { moving = false; };
    }

    private void Update()
    {
        timeCount += Time.deltaTime;

        if (_inputs.MyInputMap.Fire.IsPressed() && fireRate<timeCount)
        {
            timeCount = 0;
            Debug.Log("Fire1");
            Rigidbody shotFired = Instantiate(Ammunition, firePoint.transform.position, firePoint.transform.rotation);

            shotFired.velocity = transform.TransformDirection(Vector3.up * ammunitionVelocity);
        }
    }

    IEnumerator MoveTankInXY()
    {
        moving = true;

        while (moving)
        {
            var readInput = _inputs.MyInputMap.MoveTank.ReadValue<float>();
            var movement = Vector3.right * readInput * Time.deltaTime * speed;
            parentRigid.velocity = parentTransform.forward * speed * readInput;

            //parentTransform.position = new Vector3(movement.x + parentTransform.position.x, parentTransform.position.y, parentTransform.position.z);
            yield return null;
        }

        if (!moving)
        {
            parentRigid.velocity = new Vector3(0,0,0);
        }
    }
    IEnumerator MoveCannonVertically()
    {
        rotateAllowedVert = true;

        while (rotateAllowedVert)
        {
            var test = _inputs.MyInputMap.MoveCannon.ReadValue<float>();
            var rotationPreClamp = Vector3.right * test * Time.deltaTime * speed;

            rotationPreClamp.x = Mathf.Clamp((rotationPreClamp).x + transform.eulerAngles.x, 55, 90);

            transform.eulerAngles =
                new Vector3(rotationPreClamp.x,
                    transform.eulerAngles.y,
                            transform.eulerAngles.z);
            yield return null;
        }
    }

    IEnumerator MoveCannonHorizontally()
    {
        rotateAllowedHorz = true;

        while (rotateAllowedHorz)
        {
            var test = _inputs.MyInputMap.RotateCannon.ReadValue<float>();
            var rotationPreClamp = speed/2 * test * Time.deltaTime * Vector3.up;

            parentTransform.eulerAngles =
                new Vector3(parentTransform.eulerAngles.x, rotationPreClamp.y + parentTransform.eulerAngles.y, parentTransform.eulerAngles.z);
            
            yield return null;
        }
    }
}
