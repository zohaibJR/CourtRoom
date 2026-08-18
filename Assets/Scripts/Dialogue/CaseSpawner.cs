using UnityEngine;
using System.Collections.Generic;

public class CaseSpawner : MonoBehaviour
{
    public static CaseSpawner Instance;

    public enum CharacterRole { Accused, Accuser, Witness }

    [Header("Case Data")]
    public CaseData caseData;

    [Header("Prefabs (your existing models)")]
    public GameObject accusedPrefab;
    public GameObject accuserPrefab;
    public GameObject witnessPrefab;

    [Header("Spawn Points (drag empty GameObjects here)")]
    public Transform[] accusedPoints;
    public Transform[] accuserPoints;
    public Transform[] witnessPoints;

    private List<GameObject> spawned = new List<GameObject>();

    private List<CharacterRole> flowOrder = new List<CharacterRole>()
    {
        CharacterRole.Accuser,
        CharacterRole.Accused,
        CharacterRole.Witness
    };
    private int currentFlowStep = 0;

    // ← NEW: exposed so VerdictManager can check this
    public bool IsFlowComplete => currentFlowStep >= flowOrder.Count;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCase();
        }
    }

    private void StartCase()
    {
        if (caseData == null)
        {
            Debug.LogWarning("No CaseData assigned!");
            return;
        }

        Debug.Log("Started Case: " + caseData.caseName);
        currentFlowStep = 0;
        SpawnCase(caseData);
        EvidenceSpawner.Instance.SpawnEvidence(caseData);
        AnnounceCurrentStep();
    }

    public void SpawnCase(CaseData data)
    {
        ClearSpawned();

        SpawnGroup(data.accusedList, accusedPrefab, accusedPoints, CharacterRole.Accused);
        SpawnGroup(data.accuserList, accuserPrefab, accuserPoints, CharacterRole.Accuser);
        SpawnGroup(data.witnessList, witnessPrefab, witnessPoints, CharacterRole.Witness);
    }

    private void SpawnGroup(List<CharacterInfo> characters, GameObject prefab, Transform[] points, CharacterRole role)
    {
        if (characters == null || prefab == null)
            return;

        for (int i = 0; i < characters.Count; i++)
        {
            if (i >= points.Length)
            {
                Debug.LogWarning($"Not enough spawn points for {role}. Need {characters.Count}, have {points.Length}.");
                break;
            }

            Transform point = points[i];
            GameObject npc = Instantiate(prefab, point.position, point.rotation);

            NPCDialogueTrigger trigger = npc.GetComponent<NPCDialogueTrigger>();
            if (trigger == null)
                trigger = npc.AddComponent<NPCDialogueTrigger>();

            trigger.SetCharacter(characters[i]);
            trigger.SetRole(role);

            spawned.Add(npc);
        }
    }

    private void ClearSpawned()
    {
        foreach (var npc in spawned)
            if (npc != null) Destroy(npc);

        spawned.Clear();
    }

    private void AnnounceCurrentStep()
    {
        if (currentFlowStep >= flowOrder.Count)
        {
            Debug.Log("All witnesses/parties questioned. Return to the judge's desk to give your verdict.");
            return;
        }

        Debug.Log("Go talk to the " + flowOrder[currentFlowStep]);
    }

    public void ReportInteraction(CharacterRole role)
    {
        if (currentFlowStep >= flowOrder.Count)
            return;

        if (role == flowOrder[currentFlowStep])
        {
            currentFlowStep++;
            AnnounceCurrentStep();
        }
        else
        {
            Debug.Log("You've already spoken to them, or it's not their turn yet. Go talk to the " + flowOrder[currentFlowStep]);
        }
    }
}