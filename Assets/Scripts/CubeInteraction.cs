using UnityEngine;

public class CubeInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("You interacted with the Cube.");
    }
}