using UnityEngine;

[System.Serializable]
public class EvidenceData
{
    [Tooltip("Must match the EvidenceId on the object in the scene")]
    public string evidenceId;

    public string evidenceName;
    [TextArea(2, 5)]
    public string description;
}