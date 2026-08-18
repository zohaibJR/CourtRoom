using UnityEngine;
using System.Collections.Generic;

public class EvidenceSpawner : MonoBehaviour
{
    public static EvidenceSpawner Instance;

    [Header("Spawn Points (place these near the Judge's Desk)")]
    public Transform[] evidencePoints;

    private List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnEvidence(CaseData caseData)
    {
        ClearSpawned();

        if (caseData.evidence == null) return;

        for (int i = 0; i < caseData.evidence.Count; i++)
        {
            EvidenceData data = caseData.evidence[i];

            if (data.evidencePrefab == null)
            {
                Debug.LogWarning($"Evidence '{data.evidenceName}' has no prefab assigned.");
                continue;
            }

            if (i >= evidencePoints.Length)
            {
                Debug.LogWarning($"Not enough evidence points. Need {caseData.evidence.Count}, have {evidencePoints.Length}.");
                break;
            }

            Transform point = evidencePoints[i];
            GameObject obj = Instantiate(data.evidencePrefab, point.position, point.rotation);

            EvidenceInteraction interaction = obj.GetComponent<EvidenceInteraction>();
            if (interaction == null)
                interaction = obj.AddComponent<EvidenceInteraction>();

            interaction.SetEvidence(data);

            spawned.Add(obj);
        }
    }

    public void ClearSpawned()
    {
        foreach (var obj in spawned)
            if (obj != null) Destroy(obj);

        spawned.Clear();
    }
}