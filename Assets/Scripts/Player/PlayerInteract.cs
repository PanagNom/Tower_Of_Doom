using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    private float distance = 3.0f;

    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().playerCamera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText("");
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * distance);

        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            Interactable hitObj = hit.collider.GetComponent<Interactable>();
            playerUI.UpdateText(hitObj.promptMessage);
            if (hitObj != null)
            {
                if (inputManager.GetInputMap().PlayerMap.Interact.IsPressed())
                {
                    hitObj.BaseInteract();
                }
            }
        }
    }
}
