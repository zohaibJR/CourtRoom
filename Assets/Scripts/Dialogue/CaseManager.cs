using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public static CaseManager Instance;

    public CaseData currentCase;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadCase(CaseData newCase)
    {
        currentCase = newCase;
        CaseSpawner.Instance.SpawnCase(newCase);
        Debug.Log("Loaded case: " + newCase.caseName);
    }

    private void Start()
    {
        if (currentCase != null)
            LoadCase(currentCase); // auto-load for testing
    }
}