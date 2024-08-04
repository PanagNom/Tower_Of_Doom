using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : Interactable
{
    protected override void Interact()
    {
        promptMessage = "I want to interact";
    }
}
