using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCamera;

    public float xSensitivity = 3f;
    public float ySensitivity = 3f;
    public Vector3 initialCameraPosition;
    public Vector3 newCameraPosition = new Vector3(0f, 0.5f, 0f);

    private float verticalRotation = 0f;
    private void Start()
    {
        initialCameraPosition = newCameraPosition = playerCamera.transform.localPosition;
    }
    public void ProcessLook(Vector2 mouseInput)
    {
        float mouseInputX = mouseInput.x * xSensitivity * Time.deltaTime;
        float mouseInputY = mouseInput.y * ySensitivity * Time.deltaTime;

        verticalRotation -= mouseInputY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseInputX);
    }

    public void ChangeCameraPosition(Vector3 newCameraPositionOffset)
    {
        newCameraPosition = initialCameraPosition - newCameraPositionOffset;
        playerCamera.transform.localPosition = newCameraPosition;
    }
}
