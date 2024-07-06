using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInputs playerInputs;

    private Vector3 move = Vector3.zero;
    private Vector3 playerVelocity;
    private bool isRunning = false;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    public Camera playerCamera;
    public float xSensitivity = 3f;
    public float ySensitivity = 3f;
    private Vector2 mouseInput;
    private float rotateTo = 0f;

    private void Awake()
    {
        // Confines the cursor
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = new PlayerInputs();

        characterController = GetComponent<CharacterController>();
    }
    public void FixedUpdate()
    {
        if(characterController.isGrounded)
        {
            characterController.Move(transform.TransformDirection(move) * Time.deltaTime * speed);
        }
        else
        {
            move = new Vector3(0, 0, move.z);
            characterController.Move(transform.TransformDirection(move) * Time.deltaTime * speed);
        }

        playerVelocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var movement = context.ReadValue<Vector2>();
            move = new Vector3(movement.x, 0, movement.y);
        }
        else if (context.canceled)
        {
            move = Vector3.zero;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if(context.performed)
        {
            if (characterController.isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
    }
    public void Run(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log(isRunning);
            if (isRunning == false)
            {
                speed *= 2;
                isRunning = true;
            }
            else
            {
                speed /= 2;
                isRunning = false;
            }
        }
    }
    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Camera");
            mouseInput = context.ReadValue<Vector2>();

            float mouseInputX = mouseInput.x;
            float mouseInputY = mouseInput.y;

            rotateTo -= (mouseInputY * Time.deltaTime) * ySensitivity;
            rotateTo = Mathf.Clamp(rotateTo, -80.0f, 80.0f);

            playerCamera.transform.localRotation = Quaternion.Euler(rotateTo, 0f, 0f);

            transform.Rotate(Vector3.up * (mouseInputX * Time.deltaTime) * ySensitivity);
        }
    }
}
