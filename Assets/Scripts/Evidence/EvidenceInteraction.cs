using UnityEngine;

public class EvidenceInteraction : MonoBehaviour, IInteractable
{
    public EvidenceData data;

    public void SetEvidence(EvidenceData evidenceData)
    {
        data = evidenceData;
    }

    public void Interact()
    {
        if (data == null)
        {
            Debug.LogWarning($"{gameObject.name} has no evidence data assigned.");
            return;
        }

        EvidencePanelManager.Instance.ShowEvidence(data);
    }
}