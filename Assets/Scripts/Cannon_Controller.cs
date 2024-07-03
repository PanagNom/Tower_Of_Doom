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

    public float fireRate;
    public float ammunitionVelocity;

    private float timeCount  = 0;

    private bool rotateAllowedVert;
    private bool rotateAllowedHorz;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        fireRate = 1f;
        ammunitionVelocity = 20f;

        _inputs = new MyInputs();
        _inputs.MyInputMap.Enable();

        _inputs.MyInputMap.MoveCannon.performed += _ => { StartCoroutine(MoveCannonVertically()); };
        _inputs.MyInputMap.MoveCannon.canceled += _ => { rotateAllowedVert = false; };

        _inputs.MyInputMap.RotateCannon.performed += _ => { StartCoroutine(MoveCannonHorizontally()); };
        _inputs.MyInputMap.RotateCannon.canceled += _ => { rotateAllowedHorz = false; };
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

    IEnumerator MoveCannonVertically()
    {
        rotateAllowedVert = true;

        while (rotateAllowedVert)
        {
            var test = _inputs.MyInputMap.MoveCannon.ReadValue<float>();
            var rotationPreClamp = Vector3.right * test * Time.deltaTime * speed;

            rotationPreClamp.x = Mathf.Clamp((rotationPreClamp).x + transform.eulerAngles.x, 5, 90);

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
            var rotationPreClamp = 10 * speed * test * Time.deltaTime * Vector3.up;

            parentTransform.eulerAngles =
                new Vector3(parentTransform.eulerAngles.x, rotationPreClamp.y + parentTransform.eulerAngles.y, parentTransform.eulerAngles.z);
            
            yield return null;
        }
    }
}
