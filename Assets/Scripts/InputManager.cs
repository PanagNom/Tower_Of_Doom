using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        // Confines the cursor
        Cursor.lockState = CursorLockMode.Confined;

        playerInputs = new PlayerInputs();

        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        //playerAttack = GetComponent<PlayerAttack>();

        playerInputs.PlayerMap.Jump.performed += ctx => playerMotor.OnJump();
        playerInputs.PlayerMap.Run.performed += ctx => playerMotor.OnRun();
        playerInputs.PlayerMap.Crouch.performed += ctx => playerMotor.OnCrouch();
    }

    private void FixedUpdate()
    {
        playerMotor.ProcessMove(playerInputs.PlayerMap.Walk.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerLook.ProcessLook(playerInputs.PlayerMap.Look.ReadValue<Vector2>());
    }

    public PlayerInputs GetInputMap()
    {
        return playerInputs;
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
