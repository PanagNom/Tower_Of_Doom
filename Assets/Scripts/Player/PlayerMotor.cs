using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private PlayerLook playerLook;

    private bool isGrounded;
    private bool isRunning;
    public bool isCrouching;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public float standingHeight = 2.0f;
    public float currentHeight = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerLook = GetComponent<PlayerLook>();

        characterController = GetComponent<CharacterController>();
        standingHeight = currentHeight = characterController.height;

        isCrouching = false;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        characterController.Move(transform.TransformDirection(moveDirection) * Time.deltaTime * speed);

        playerVelocity.y += gravity * Time.deltaTime;

        if(isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }
    public void OnJump()
    {
        if (characterController.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    public void OnRun()
    {
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
    public void OnCrouch()
    {
        if (characterController.isGrounded && isCrouching)
        {
            isCrouching = false;
            characterController.height = 2f;
            playerLook.ChangeCameraPosition(new Vector3(0f, 0f, 0f));
        }
        else if (characterController.isGrounded && !isCrouching)
        {
            isCrouching = true;
            characterController.height = .5f;

            playerLook.ChangeCameraPosition(new Vector3(0f,.5f,0f));
        } 
    }
}
