using UnityEngine;
using UnityEngine.UI;

public class VerdictManager : MonoBehaviour
{
    public static VerdictManager Instance;

    [Header("UI References")]
    public GameObject verdictPanel;
    public Text caseNameText;
    public Text resultText;

    [Header("Result Panel")]
    public GameObject resultPanel;
    public float resultDisplayDuration = 5f;

    private void Awake()
    {
        Instance = this;

        verdictPanel.SetActive(false);

        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    private void Update()
    {
        if (!verdictPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChooseGuilty();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChooseNotGuilty();
    }

    public void OpenVerdictPanel()
    {
        CaseData current = CaseSpawner.Instance.caseData;

        if (current == null)
        {
            Debug.LogWarning("No active case to give a verdict on.");
            return;
        }

        // ← NEW: block until all NPCs have been talked to
        if (!CaseSpawner.Instance.IsFlowComplete)
        {
            Debug.Log("You still need to question everyone before giving a verdict.");
            return;
        }

        caseNameText.text = current.caseName;
        verdictPanel.SetActive(true);
    }

    public void ChooseGuilty()
    {
        SubmitVerdict(CaseData.Verdict.Guilty);
    }

    public void ChooseNotGuilty()
    {
        SubmitVerdict(CaseData.Verdict.NotGuilty);
    }

    private void SubmitVerdict(CaseData.Verdict chosen)
    {
        CaseData current = CaseSpawner.Instance.caseData;

        bool correct = chosen == current.correctVerdict;

        Debug.Log(correct
            ? "Correct verdict!"
            : $"Wrong verdict. The truth was: {current.correctVerdict}");

        verdictPanel.SetActive(false);

        if (resultPanel != null)
        {
            resultText.text = correct
                ? $"Correct Decision!\n\n{current.caseName}\nVerdict: {chosen}"
                : $"Incorrect Decision.\n\n{current.caseName}\nYou said: {chosen}\nTruth: {current.correctVerdict}";

            resultPanel.SetActive(true);

            if (CursorManager.Instance != null)
                CursorManager.Instance.EnableCursor();

            // ← NEW: auto-hide after X seconds
            CancelInvoke(nameof(HideResultPanel));
            Invoke(nameof(HideResultPanel), resultDisplayDuration);
        }
    }

    private void HideResultPanel()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);

        if (CursorManager.Instance != null)
            CursorManager.Instance.DisableCursor();
    }
}