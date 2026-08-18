using UnityEngine;

[System.Serializable]
public class EvidenceData
{
    public string evidenceId;
    public string evidenceName;

    [TextArea(2, 5)]
    public string description;

    [Tooltip("The 3D model representing this evidence in the courtroom")]
    public GameObject evidencePrefab;
}