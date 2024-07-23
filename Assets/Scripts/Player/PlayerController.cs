using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInputs playerInputs;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 jumpDirection = Vector3.zero;
    private bool isRunning = false;
    private bool isCrouching = false;
    private float playerCrouchHeight;
    private float playerHeight;

    public float crouchSpeed = 3f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    public Camera playerCamera;
    public float xSensitivity = 3f;
    public float ySensitivity = 3f;
    private float verticalRotation = 0f;

    private void Awake()
    {
        // Confines the cursor
        Cursor.lockState = CursorLockMode.Confined;
        characterController = GetComponent<CharacterController>();
        playerInputs = new PlayerInputs();

        playerInputs.PlayerMap.Walk.performed += ctx => OnMove(ctx);
        playerInputs.PlayerMap.Walk.canceled += ctx => OnMove(ctx);
        playerInputs.PlayerMap.Jump.performed += ctx => OnJump(ctx);
        playerInputs.PlayerMap.Run.performed += ctx => OnRun(ctx);
        playerInputs.PlayerMap.Crouch.performed += ctx => OnCrouch(ctx);
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }
    private void Update()
    {
        HandleMovement();
        HandleGravity();
        
    }
    private void LateUpdate()
    {
        HandleLook();
    }
    private void HandleMovement()
    {
        if (characterController.isGrounded && !isCrouching)
        {
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, 1, playerCamera.transform.position.z);
            characterController.Move(transform.TransformDirection(moveDirection) * 
                Time.deltaTime * (isRunning?runSpeed:walkSpeed));
        }
        else if(characterController.isGrounded && isCrouching)
        {
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, .5f, playerCamera.transform.position.z);
            characterController.Move(transform.TransformDirection(moveDirection) *
                Time.deltaTime * crouchSpeed);
        }
        else
        {
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, 1, playerCamera.transform.position.z);
            characterController.Move(transform.TransformDirection(jumpDirection) *
                Time.deltaTime * (isRunning ? runSpeed : walkSpeed));
        }
    }
    private void HandleGravity()
    {       
        playerVelocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
            playerVelocity.z = 0f;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }
    private void HandleLook()
    {
        Vector2 lookInput = playerInputs.PlayerMap.Look.ReadValue<Vector2>();
        float mouseX = lookInput.x * xSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * ySensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    private void OnMove(InputAction.CallbackContext context)
    {

        Vector2 input = context.ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0, input.y);
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && characterController.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            jumpDirection = moveDirection;
        }
    }
    private void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isRunning = !isRunning;
        }
    }
    private void OnCrouch(InputAction.CallbackContext context)
    {
        if(context.performed && characterController.isGrounded)
        {
            isCrouching = !isCrouching;
        }
    }
}
