using UnityEngine;

public class JudgeDeskInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        VerdictManager.Instance.OpenVerdictPanel();
    }
}